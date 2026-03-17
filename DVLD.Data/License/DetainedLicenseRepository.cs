using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.Data
{
    public static class DetainedLicenseRepository
    {
        public static int Detain(int licenseId)
        {
            string query = @"INSERT INTO [dbo].[DetainedLicenses]
                            ([LicenseID]
                            ,[DetainDate]
                            ,[FineFees]
                            ,[CreatedByUserID]
                            ,[IsReleased]
                            ,[ReleaseDate]
                            ,[ReleasedByUserID]
                            ,[ReleaseApplicationID])
                        VALUES
                            (@LicenseID,
                            GETDATE(),
                            150,
                            @CreatedByUserID,
                            0,
                            NULL,
                            NULL,
                            NULL);
                        SELECT SCOPE_IDENTITY();";

            try
            {
                using (var con = new SqlConnection(DataSettings.connectionString))
                using (var com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@LicenseID", licenseId);
                    com.Parameters.AddWithValue("@CreatedByUserID", LoggedInUserInfo.UserId);
                    con.Open();

                    object result = com.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int detainId))
                        return detainId;
                    else
                        return -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into DetainedLicenses.", ex);
                throw;
            }
        }

        public static int Release(int licenseId)
        {
            string query = @"-- T-SQL: Release detained license (validate then create release application + local driving application + mark detained released)
                            -- EXPECTS PARAMETERS: @LicenseID INT, @ReleasedByUserID INT

                            SET NOCOUNT ON;
                            SET XACT_ABORT ON;

                            BEGIN TRY
                                BEGIN TRAN;

                                ----------------------------------------------------------------
                                -- 1) Validation: get license info and related driver/person
                                ----------------------------------------------------------------
                                DECLARE 
                                    @DriverID INT,
                                    @LicenseClass INT,
                                    @IsActive BIT,
                                    @ExpirationDate DATETIME,
                                    @PersonID INT,
                                    @NewApplicationID INT,
                                    @AppPaidFees SMALLMONEY;

                                SELECT 
                                    @DriverID    = L.DriverID,
                                    @LicenseClass = L.LicenseClass,
                                    @IsActive    = L.IsActive,
                                    @ExpirationDate = L.ExpirationDate
                                FROM Licenses L
                                WHERE L.LicenseID = @LicenseID;

                                -- License exists?
                                IF @DriverID IS NULL
                                BEGIN
                                    ROLLBACK TRAN;
                                    THROW 50001, 'License does not exist.', 1;
                                END

                                -- Is active?
                                IF @IsActive = 0
                                BEGIN
                                    ROLLBACK TRAN;
                                    THROW 50002, 'License is not active', 1;
                                END

                                -- Not expired?
                                IF @ExpirationDate <= GETDATE()
                                BEGIN
                                    ROLLBACK TRAN;
                                    THROW 50003, 'License is expired.', 1;
                                END

                                -- Is detained?
                                IF NOT EXISTS (
                                    SELECT 1 FROM DetainedLicenses DL
                                    WHERE DL.LicenseID = @LicenseID
                                        AND DL.IsReleased = 0
                                )
                                BEGIN
                                    ROLLBACK TRAN;
                                    THROW 50004, 'License is not detained', 1;
                                END

                                -- get PersonID from Drivers
                                SELECT @PersonID = D.PersonID
                                FROM Drivers D
                                WHERE D.DriverID = @DriverID;

                                IF @PersonID IS NULL
                                BEGIN
                                    ROLLBACK TRAN;
                                    THROW 50005, 'License does not exist.', 1; -- defensive: driver without person
                                END

                                ----------------------------------------------------------------
                                -- 2) Create new Application (ApplicationTypeID = 5)
                                ----------------------------------------------------------------
                                SELECT @AppPaidFees = ISNULL(ApplicationFees, 0)
                                FROM ApplicationTypes
                                WHERE ApplicationTypeID = 5;

                                INSERT INTO Applications
                                    (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                                VALUES
                                    (@PersonID, GETDATE(), 5, 3, GETDATE(), @AppPaidFees, @ReleasedByUserID);

                                SET @NewApplicationID = SCOPE_IDENTITY();

                                ----------------------------------------------------------------
                                -- 3) Insert LocalDrivingLicenseApplications for this application
                                ----------------------------------------------------------------
                                INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
                                VALUES (@NewApplicationID, @LicenseClass);

                                ----------------------------------------------------------------
                                -- 4) Mark detained record(s) as released
                                --    update rows where LicenseID matches and IsReleased = 0
                                ----------------------------------------------------------------
                                UPDATE DetainedLicenses
                                SET 
                                    IsReleased = 1,
                                    ReleaseDate = GETDATE(),
                                    ReleasedByUserID = @ReleasedByUserID,
                                    ReleaseApplicationID = @NewApplicationID
                                WHERE LicenseID = @LicenseID
                                    AND IsReleased = 0;

                                ----------------------------------------------------------------
                                -- 5) Commit and return the ApplicationID (as confirmation)
                                ----------------------------------------------------------------
                                COMMIT TRAN;

                                SELECT @NewApplicationID AS NewReleaseApplicationID;

                            END TRY
                            BEGIN CATCH
                                IF XACT_STATE() <> 0
                                    ROLLBACK TRAN;

                                -- Re-throw the error so caller sees it (keeps original message if any)
                                DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
                                THROW 50000, @ErrMsg, 1;
                            END CATCH;";

            try
            {
                using (var con = new SqlConnection(DataSettings.connectionString))
                using (var com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@LicenseID", licenseId);
                    com.Parameters.AddWithValue("@ReleasedByUserID", LoggedInUserInfo.UserId);
                    con.Open();

                    object result = com.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int releaseId))
                        return releaseId;
                    else
                        return -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while release a detained license with id {licenseId}.", ex);
                throw;
            }
        }

        public static DataTable GetAllWithDetails()
        {
            string query = @"SELECT * FROM DetainedLicenses_View;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // return true, but data table is empty fix this bug
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            return dataTable;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from DetainedLicenses", ex);
                throw;
            }
        }

        public static DataTable GetLicenseRecords(int licenseId, bool isReleasedOnly = false)
        {
            string query = @"SELECT * FROM DetainedLicenses WHERE LicenseID = @licenseId;";

            if (isReleasedOnly)
                query += " AND IsReleased = 1;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@licenseId", licenseId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from DetainedLicenses", ex);
                throw;
            }
        }

        public static DetainedLicense GetByLicenseId(int licenseId)
        {
            string query = @"SELECT * FROM DetainedLicenses WHERE LicenseID = @licenseId AND IsReleased = 0;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", licenseId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DetainedLicense
                            (
                                Convert.ToInt32(reader["DetainID"]),
                                Convert.ToInt32(reader["LicenseID"]),
                                Convert.ToDateTime(reader["DetainDate"]),
                                Convert.ToDecimal(reader["FineFees"]),
                                Convert.ToInt32(reader["CreatedByUserID"]),
                                Convert.ToBoolean(reader["IsReleased"]),
                                reader["ReleaseDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ReleaseDate"]) : null,
                                reader["ReleasedByUserID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ReleasedByUserID"]) : null,
                                reader["ReleaseApplicationID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ReleaseApplicationID"]) : null
                            );
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from DetainedLicenses", ex);
                throw;
            }
        }

        public static bool Exists(int detainId)
        {
            string query = @"SELECT 1 FROM DetainedLicenses WHERE DetainID = @detainId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@detainId", detainId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in DetainedLicenses.", ex);
                throw;
            }
        }

        public static bool IsDetained(int licenseId)
        {
            string query = @"SELECT 1 FROM DetainedLicenses WHERE LicenseID = @licenseId AND IsReleased = 0;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@licenseId", licenseId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in DetainedLicenses.", ex);
                throw;
            }
        }

        public static bool IsDetained(int personId, LicenseClass licenseClass)
        {
            string query = @"SELECT 1 FROM DetainedLicenses DL
                            JOIN Licenses L ON DL.LicenseID = L.LicenseID
                            JOIN Drivers D ON L.DriverID = D.DriverID
                            WHERE D.PersonID = @personId AND L.LicenseClass = @licenseClass AND IsReleased = 0;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personId", personId);
                    command.Parameters.AddWithValue("@licenseClass", (int)licenseClass);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in DetainedLicenses.", ex);
                throw;
            }
        }
    }
}