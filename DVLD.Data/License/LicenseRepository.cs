using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.Data
{
    public static class LicenseRepository
    {
        public static int Add(License license)
        {
            string query = @"DECLARE @ValidityLength INT;
                            DECLARE @ClassFees MONEY;
                            
                            SELECT 
                                @ValidityLength = DefaultValidityLength,
                                @ClassFees = ClassFees
                            FROM LicenseClasses
                            WHERE LicenseClassID = @licenseClass;
                            
                            INSERT INTO Licenses
                            (
                                ApplicationID,
                                DriverID,
                                LicenseClass,
                                IssueDate,
                                ExpirationDate,
                                Notes,
                                PaidFees,
                                IsActive,
                                IssueReason,
                                CreatedByUserID
                            )
                            VALUES
                            (
                                @applicationID,
                                @driverID,
                                @licenseClass,
                                GETDATE(),
                                DATEADD(YEAR, @ValidityLength, GETDATE()),
                                @notes,
                                @ClassFees,
                                1,
                                @issueReason,
                                @createdByUserID
                            );
                            
                            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@applicationID", license.ApplicationId);
                    command.Parameters.AddWithValue("@driverID", license.DriverId);
                    command.Parameters.AddWithValue("@licenseClass", (int)license.LicenseClass);
                    command.Parameters.AddWithValue("@notes", license.Notes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@issueReason", (int)license.IssueReason);
                    command.Parameters.AddWithValue("@createdByUserID", LoggedInUserInfo.UserId);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int newLicenseId))
                        return newLicenseId;
                }

                return -1; // Return -1 if the insert operation fails
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while adding a new license.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = "SELECT * FROM Licenses;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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
                AppLogger.LogError("DAL: Error while fetching all licenses.", ex);
                throw;
            }
        }

        public static DataTable GetDriverLicenses(int driverId)
        {
            string query = "SELECT * FROM Licenses WHERE DriverID = @DriverID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverId);
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
                AppLogger.LogError($"DAL: Error while fetching licenses for DriverID {driverId}.", ex);
                throw;
            }
        }

        public static int GetPersonIdByLicenseId(int licenseId)
        {
            string query = @"SELECT D.PersonID
                            FROM Licenses L
                            INNER JOIN Drivers D ON L.DriverID = D.DriverID
                            WHERE L.LicenseID = @LicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", licenseId);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int personId))
                        return personId;
                }
                return -1; // Return -1 if the insert operation fails
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching PersonID for LicenseID {licenseId}.", ex);
                throw;
            }
        }

        public static DataTable GetLicenseHistoryByPersonId(int personId)
        {
            string query = "SELECT * FROM Licenses WHERE DriverID IN (SELECT DriverID FROM Drivers WHERE PersonID = @PersonID);";

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

        public static License GetById(int licenseId)
        {
            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID;";

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
                            return new License
                            (
                                licenseId: Convert.ToInt32(reader["LicenseID"]),
                                applicationId: Convert.ToInt32(reader["ApplicationID"]),
                                driverId: Convert.ToInt32(reader["DriverID"]),
                                licenseClass: (LicenseClass)Convert.ToInt32(reader["LicenseClass"]),
                                issueDate: Convert.ToDateTime(reader["IssueDate"]),
                                expirationDate: Convert.ToDateTime(reader["ExpirationDate"]),
                                notes: reader["Notes"] != null ? reader["Notes"].ToString() : string.Empty,
                                paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                isActive: Convert.ToBoolean(reader["IsActive"]),
                                issueReason: (IssueReason)Convert.ToInt32(reader["IssueReason"]),
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserID"])
                            );
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching license with ID {licenseId}.", ex);
                throw;
            }
        }

        public static int GetLicenseIdByDriverId(int driverId, LicenseClass licenseClass, bool getActiveOnly = true)
        {
            string query = "SELECT LicenseID FROM Licenses WHERE DriverID = @DriverID AND LicenseClass = @LicenseClass";

            if (getActiveOnly)
                query += " AND IsActive = 1;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverId);
                    command.Parameters.AddWithValue("@LicenseClass", (int)licenseClass);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int licenseId))
                        return licenseId;
                    return -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching license for DriverID {driverId} and LicenseClass {licenseClass}.", ex);
                throw;
            }
        }

        public static DataTable GetLicenseHistoryByDriverId(int driverId)
        {
            string query = @"SELECT  l.LicenseID,
                    		l.ApplicationID,
                    		lc.ClassName,
                    		l.IssueDate,
                    		l.IssueDate + lc.DefaultValidityLength AS ExpirationDate,
                    		l.IsActive
                    FROM Licenses l
                    INNER JOIN LicenseClasses lc
                    	ON l.LicenseClass = lc.LicenseClassID
                    WHERE l.DriverID =  @DriverID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverId);
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
                AppLogger.LogError($"DAL: Error while fetching license history for DriverID {driverId}.", ex);
                throw;
            }
        }

        public static DataTable GetLicenseHistoryByNationalNo(string nationalNo)
        {
            string query = @"SELECT  l.LicenseID,
                    		l.ApplicationID,
                    		lc.ClassName,
                    		l.IssueDate,
                    		l.IssueDate + lc.DefaultValidityLength AS ExpirationDate,
                    		l.IsActive
                    FROM Licenses l
                    INNER JOIN LicenseClasses lc
                    	ON l.LicenseClass = lc.LicenseClassID
                    INNER JOIN Drivers d
                    	ON l.DriverID = d.DriverID
                    INNER JOIN People p
                    	ON d.PersonID = p.PersonID
                    WHERE p.NationalNo =  @NationalNo;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", nationalNo);
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
                AppLogger.LogError($"DAL: Error while fetching license history for NationalNo {nationalNo}.", ex);
                throw;
            }
        }

        public static License GetByNationalNo(string nationalNo)
        {
            string query = @"SELECT * FROM Licenses l
                            INNER JOIN Drivers d
                            ON l.DriverID = d.DriverID
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
                        if (reader.Read())
                        {
                            return new License(
                                licenseId: Convert.ToInt32(reader["LicenseID"]),
                                applicationId: Convert.ToInt32(reader["ApplicationID"]),
                                driverId: Convert.ToInt32(reader["DriverID"]),
                                licenseClass: (LicenseClass)Convert.ToInt32(reader["LicenseClass"]),
                                issueDate: Convert.ToDateTime(reader["IssueDate"]),
                                expirationDate: Convert.ToDateTime(reader["ExpirationDate"]),
                                notes: reader["Notes"] != null ? reader["Notes"].ToString() : string.Empty,
                                paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                isActive: Convert.ToBoolean(reader["IsActive"]),
                                issueReason: (IssueReason)Convert.ToInt32(reader["IssueReason"]),
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserID"])
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching license for NationalNo {nationalNo}.", ex);
                throw;
            }
            return null;
        }

        public static License GetByApplicationId(int applicationId)
        {
            const string query = @"SELECT * FROM Licenses 
                                WHERE ApplicationID = @ApplicationID;";

            try
            {
                using (var con = new SqlConnection(DataSettings.connectionString))
                using (var com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@ApplicationID", applicationId);
                    con.Open();

                    using (var reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new License
                            (
                                licenseId: Convert.ToInt32(reader["LicenseID"]),
                                applicationId: Convert.ToInt32(reader["ApplicationID"]),
                                driverId: Convert.ToInt32(reader["DriverID"]),
                                licenseClass: (LicenseClass)Convert.ToInt32(reader["LicenseClass"]),
                                issueDate: Convert.ToDateTime(reader["IssueDate"]),
                                expirationDate: Convert.ToDateTime(reader["ExpirationDate"]),
                                notes: reader["Notes"] != null ? reader["Notes"].ToString() : string.Empty,
                                paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                isActive: Convert.ToBoolean(reader["IsActive"]),
                                issueReason: (IssueReason)Convert.ToInt32(reader["IssueReason"]),
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserID"])
                            );
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching license for ApplicationId {applicationId}.", ex);
                throw;
            }
        }

        public static License GetByLocalApplicationId(int localApplicationId)
        {
            const string query = @"SELECT * FROM Licenses l
                            INNER JOIN Applications a
                                ON l.ApplicationID = a.ApplicationID
                            INNER JOIN LocalDrivingLicenseApplications ld
                                ON a.ApplicationID = ld.ApplicationID
                            WHERE ld.LocalDrivingLicenseApplicationID =
                            @LocalDrivingLicenseApplicationID;";

            try
            {
                using (var con = new SqlConnection(DataSettings.connectionString))
                using (var com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",
                        localApplicationId);
                    con.Open();

                    using (var reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new License
                            (
                                licenseId: Convert.ToInt32(reader["LicenseID"]),
                                applicationId: Convert.ToInt32(reader["ApplicationID"]),
                                driverId: Convert.ToInt32(reader["DriverID"]),
                                licenseClass: (LicenseClass)Convert.ToInt32(reader["LicenseClass"]),
                                issueDate: Convert.ToDateTime(reader["IssueDate"]),
                                expirationDate: Convert.ToDateTime(reader["ExpirationDate"]),
                                notes: reader["Notes"] != null ? reader["Notes"].ToString() : string.Empty,
                                paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                isActive: Convert.ToBoolean(reader["IsActive"]),
                                issueReason: (IssueReason)Convert.ToInt32(reader["IssueReason"]),
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserID"])
                            );
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching license for LocalApplicationId {localApplicationId}.", ex);
                throw;
            }

        }

        public static bool Exists(int licenseId)
        {
            string query = @"SELECT 1 FROM Licenses WHERE LicenseID = @licenseId;";

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
                AppLogger.LogError($"DAL: Error while checking existence of license with ID {licenseId}.", ex);
                throw;
            }
        }

        public static bool ExistsByDriver(int driverId, LicenseClass licenseClass, bool checkActive = false)
        {
            string query = @"SELECT 1 FROM Licenses WHERE DriverID = @Id AND LicenseClass = @class";

            if (checkActive)
                query += " AND IsActive = 1;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", driverId);
                    command.Parameters.AddWithValue("@class", (int)licenseClass);
                    command.Parameters.AddWithValue("@todayDate", DateTime.Now);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while license with driver id = {driverId}.", ex);
                throw;
            }
        }

        public static bool ExistsForApplication(int applicationId)
        {
            string query = @"SELECT 1 FROM Licenses WHERE ApplicationID = @applicationId;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@applicationId", applicationId);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking if license for application ID {applicationId} exists.", ex);
                throw;
            }
        }

        public static bool IsActive(int licenseId)
        {
            string query = @"SELECT 1 FROM Licenses WHERE LicenseID = @licenseId AND IsActive = 1;";

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
                AppLogger.LogError($"DAL: Error while checking if license with ID {licenseId} is active.", ex);
                throw;
            }
        }

        public static bool IsActive(int personId, LicenseClass licenseClass)
        {
            string query = @"SELECT 1 FROM Licenses L
                            INNER JOIN Drivers D ON L.DriverID = D.DriverID
                            WHERE D.PersonID = @personId AND L.LicenseClass = @licenseClass AND L.IsActive = 1;";

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
                AppLogger.LogError("DAL: Error while checking if license is active.", ex);
                throw;
            }
        }

        public static bool IsExpired(int personId, LicenseClass licenseClass)
        {
            string query = @"SELECT 1 FROM Licenses L
                            INNER JOIN Drivers D ON L.DriverID = D.DriverID
                            WHERE D.PersonID = @applicantPersonId AND L.LicenseClass = @licenseClass AND L.ExpirationDate < GETDATE();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@applicantPersonId", personId);
                    command.Parameters.AddWithValue("@licenseClass", (int)licenseClass);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking if license is expired.", ex);
                throw;
            }
        }

        public static bool IsExpired(int licenseId)
        {
            string query = @"SELECT 1 FROM Licenses WHERE LicenseID = @licenseId AND ExpirationDate < GETDATE();";
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
                AppLogger.LogError($"DAL: Error while checking if license with ID {licenseId} is expired.", ex);
                throw;
            }
        }

        public static bool UpdateNotes(int licenseId, string notes)
        {
            string query = @"UPDATE Licenses SET Notes = @notes WHERE LicenseID = @licenseId;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@notes", notes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@licenseId", licenseId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while updating notes for license with ID {licenseId}.", ex);
                throw;
            }
        }

        public static bool DeactivateLicense(int licenseId)
        {
            string query = "UPDATE Licenses SET IsActive = 0 WHERE LicenseId = @LicenseId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseId", licenseId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deactivating license with ID {licenseId}.", ex);
                throw;
            }
        }

        public static bool ReactivateLicense(int licenseId)
        {
            string query = "UPDATE Licenses SET IsActive = 1 WHERE LicenseId = @LicenseId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseId", licenseId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while reactivating license with ID {licenseId}.", ex);
                throw;
            }
        }
    }
}
