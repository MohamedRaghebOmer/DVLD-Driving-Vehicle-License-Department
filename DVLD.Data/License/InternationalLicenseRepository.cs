using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.Data
{
    public static class InternationalLicenseRepository
    {
        public static int Add(int localClass3LicenseId)
        {
            string query = @"SET XACT_ABORT ON;

                            BEGIN TRY
                                BEGIN TRANSACTION;

                                DECLARE 
                                    @DriverID INT,
                                    @ApplicantPersonID INT,
                                    @LicenseClass INT,
                                    @LicenseIsActive BIT,
                                    @LicenseExpiration DATETIME,
                                    @PaidFees SMALLMONEY,
                                    @ApplicationID INT,
                                    @NewInternationalLicenseID INT;

                                SELECT 
                                    @DriverID = L.DriverID,
                                    @LicenseClass = L.LicenseClass,
                                    @LicenseIsActive = L.IsActive,
                                    @LicenseExpiration = L.ExpirationDate
                                FROM Licenses L
                                WHERE L.LicenseID = @LocalLicenseID;

                                IF @DriverID IS NULL
                                BEGIN
                                    THROW 51000, 'The local license does not exist.', 1;
                                END

                                IF EXISTS (SELECT 1 FROM InternationalLicenses IL WHERE IL.IssuedUsingLocalLicenseID = @LocalLicenseID)
                                BEGIN
                                    THROW 51001, 'The local license is already assigned to an international license.', 1;
                                END

                                IF EXISTS (SELECT 1 FROM InternationalLicenses IL WHERE IL.DriverID = @DriverID)
                                BEGIN
                                    THROW 51002, 'The driver is already assigned to an international license.', 1;
                                END

                                IF @LicenseClass <> 3
                                BEGIN
                                    THROW 51003, 'The local license must be a class 3 license.', 1;
                                END

                                IF @LicenseIsActive <> 1
                                BEGIN
                                    THROW 51004, 'Local license is not active.', 1;
                                END

                                IF @LicenseExpiration IS NULL OR @LicenseExpiration < GETDATE()
                                BEGIN
                                    THROW 51005, 'Local license is expired.', 1;
                                END

                                IF EXISTS (
                                    SELECT 1 FROM DetainedLicenses D
                                    WHERE D.LicenseID = @LocalLicenseID
                                      AND (D.IsReleased = 0 OR D.IsReleased IS NULL)
                                )
                                BEGIN
                                    THROW 51006, 'The local license is detained.', 1;
                                END

                                SELECT @ApplicantPersonID = D.PersonID
                                FROM Drivers D
                                WHERE D.DriverID = @DriverID;

                                IF @ApplicantPersonID IS NULL
                                BEGIN
                                    THROW 51007, 'Driver record not found for this license.', 1;
                                END

                                SELECT @PaidFees = AT.ApplicationFees
                                FROM ApplicationTypes AT
                                WHERE AT.ApplicationTypeID = 6;

                                IF @PaidFees IS NULL
                                BEGIN
                                    THROW 51008, 'Application type 6 (New International License) not found or has no fee configured.', 1;
                                END

                                INSERT INTO Applications
                                    (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                                VALUES
                                    (@ApplicantPersonID, GETDATE(), 6, 3, GETDATE(), @PaidFees, @CreatedByUserID);

                                SET @ApplicationID = CAST(SCOPE_IDENTITY() AS INT);

                                INSERT INTO InternationalLicenses
                                    (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID)
                                VALUES
                                    (@ApplicationID, @DriverID, @LocalLicenseID, GETDATE(), DATEADD(year, 1, GETDATE()), 1, @CreatedByUserID);

                                SET @NewInternationalLicenseID = CAST(SCOPE_IDENTITY() AS INT);

                                COMMIT TRANSACTION;

                                SELECT @NewInternationalLicenseID AS NewInternationalLicenseID;
                            END TRY
                            BEGIN CATCH
                                IF XACT_STATE() <> 0 AND @@TRANCOUNT > 0
                                    ROLLBACK TRANSACTION;

                                THROW;
                            END CATCH;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalLicenseID", localClass3LicenseId);
                    command.Parameters.AddWithValue("@CreatedByUserID", LoggedInUserInfo.UserId);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int newLicenseId))
                        return newLicenseId;

                    return -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into InternationalLicenses", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = @"SELECT * FROM InternationalLicenses;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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
                AppLogger.LogError("DAL: Error while selecting from InternationalLicenses", ex);
                throw;
            }
        }

        public static DataTable GetLicenseHistoryByPersonId(int personId)
        {
            string query = @"SELECT 
                            il.InternationalLicenseID,
                            il.ApplicationID,
                            il.IssuedUsingLocalLicenseID,
                            il.IssueDate,
                            il.ExpirationDate,
                            il.IsActive
                        FROM InternationalLicenses il
                        INNER JOIN Drivers d
                            ON il.DriverID = d.DriverID
                        WHERE d.PersonID = @PersonID;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
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
                AppLogger.LogError($"DAL: Error while fetching license history for PersonID {personId}.", ex);
                throw;
            }
        }

        public static InternationalLicense GetById(int licenseID)
        {
            string query = @"SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @LicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", licenseID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new InternationalLicense
                            (
                                licenseID,
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("DriverID")),
                                reader.GetInt32(reader.GetOrdinal("IssuedUsingLocalLicenseID")),
                                reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                                reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                                reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                            );
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from InternationalLicenses", ex);
                throw;
            }
        }

        public static int GetIdByLocalLicenseId(int localLicenseId)
        {
            string query = @"SELECT InternationalLicenseID FROM InternationalLicenses WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", localLicenseId);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int licenseId))
                        return licenseId;

                    return -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from InternationalLicenses", ex);
                throw;
            }
        }

        public static DataTable GetLicenseHistory(int driverId)
        {
            string query = @"SELECT  il.InternationalLicenseID,
                    		    il.ApplicationID,
                    		    il.IssuedUsingLocalLicenseID,
                    		    il.IssueDate,
                    		    il.ExpirationDate,
                    		    il.IsActive
                            FROM InternationalLicenses il
                            WHERE il.DriverID = @DriverID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverId);
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
                AppLogger.LogError("DAL: Error while selecting from InternationalLicenses", ex);
                throw;
            }
        }

        public static DataTable GetLicenseHistory(string nationalNo)
        {
            string query = @"SELECT  il.InternationalLicenseID,
                        		il.ApplicationID,
                        		il.IssuedUsingLocalLicenseID,
                        		il.IssueDate,
                        		il.ExpirationDate,
                        		il.IsActive
                        FROM InternationalLicenses il
                        INNER JOIN Drivers d
                        	ON il.DriverID = d.DriverID
                        INNER JOIN People p
                        	ON d.PersonID = p.PersonID
                        WHERE p.NationalNo = @NationalNo;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", nationalNo);
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
                AppLogger.LogError("DAL: Error while selecting from InternationalLicenses", ex);
                throw;
            }
        }

        public static InternationalLicense GetByLocalLicenseId(int localLicenseId)
        {
            string query = @"SELECT * FROM InternationalLicenses 
                     WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", localLicenseId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new InternationalLicense
                            (
                                reader.GetInt32(reader.GetOrdinal("InternationalLicenseID")),
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("DriverID")),
                                reader.GetInt32(reader.GetOrdinal("IssuedUsingLocalLicenseID")),
                                reader.GetDateTime(reader.GetOrdinal("IssueDate")),        // ensure column name matches DB
                                reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                                reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                            );
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from InternationalLicenses", ex);
                throw;
            }
        }

        public static bool ExistsByLocalLicenseId(int localLicenseId, bool checkIsActive = false)
        {
            string query = @"SELECT 1 FROM InternationalLicenses WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID";

            if (checkIsActive)
                query += " AND IsActive = 1;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", localLicenseId);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from InternationalLicenses", ex);
                throw;
            }
        }

        public static bool ExistsForApplication(int applicationId, int excludedInternationalLicenseId = -1)
        {
            string query = @"SELECT 1 FROM InternationalLicenses WHERE ApplicationID = @ApplicationID AND InternationalLicenseID != @InternationalLicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", applicationId);
                    command.Parameters.AddWithValue("@InternationalLicenseID", excludedInternationalLicenseId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from InternationalLicenses", ex);
                throw;
            }
        }

        public static bool ExistsByDriver(int driverId, bool isActive = false, bool notExpired = false)
        {
            string query = @"SELECT 1 FROM InternationalLicenses WHERE DriverID = @DriverID";

            if (isActive)
                query += " AND IsActive = 1;";

            if (notExpired)
                query += " AND ExpirationDate > GETDATE();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from InternationalLicenses", ex);
                throw;
            }
        }

        public static bool DeactivateById(int internationalLicenseId)
        {
            string query = @"UPDATE InternationalLicenses SET IsActive = 0 WHERE InternationalLicenseID = @InternationalLicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseId);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating InternationalLicenses", ex);
                throw;
            }
        }

        public static bool DeactivateByDriverId(int driverId)
        {
            string query = @"UPDATE InternationalLicenses SET IsActive = 0 WHERE DriverID = @DriverID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating InternationalLicenses", ex);
                throw;
            }
        }

        public static bool Activate(int internationalLicenseId)
        {
            string query = @"UPDATE InternationalLicenses SET IsActive = 1 WHERE InternationalLicenseID = @InternationalLicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseId);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while activating InternationalLicenses.", ex);
                throw;
            }

        }

        public static bool UpdateLocalLicense(int internationalLicenseId, int issuedUsingLocalLicenseId)
        {
            string query = @"UPDATE InternationalLicenses SET IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID WHERE InternationalLicenseID = @InternationalLicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", issuedUsingLocalLicenseId);
                    command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating IssuedUsingLocalLicenseID for InternationalLicenses.", ex);
                throw;
            }
        }
    }
}
