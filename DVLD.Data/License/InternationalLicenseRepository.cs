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
        public static int Add(InternationalLicense internationalLicense)
        {
            string query = @"INSERT INTO InternationalLicenses(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssuedDate, ExpirationDate, IsActive, CreatedByUserID)
                            VALUES (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, GETDATE(), @ExpirationDate, @IsActive, @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", internationalLicense.ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", internationalLicense.DriverID);
                    command.Parameters.AddWithValue("@IssuedUsingLocalDrivingLicenseID", internationalLicense.IssuedUsingLocalLicenseID);
                    command.Parameters.AddWithValue("@ExpirationDate", internationalLicense.ExpirationDate);
                    command.Parameters.AddWithValue("@IsActive", internationalLicense.IsActive);
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

        public static InternationalLicense GetById(int licenseID)
        {
            string query = @"SELECT * FROM InternationalLicenses WHERE LicenseID = @LicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", licenseID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return new InternationalLicense
                                (
                                    licenseID,
                                    (int)reader["ApplicationID"],
                                    (int)reader["DriverID"],
                                    (int)reader["IssuedUsingLocalLicenseID"],
                                    (DateTime)reader["IssuedDate"],
                                    (DateTime)reader["ExpirationDate"],
                                    (bool)reader["IsActive"],
                                    (int)reader["CreatedByUserID"]
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

        public static bool ExistsForLocalLicenseId(int localLicenseId)
        {
            string query = @"SELECT 1 FROM InternationalLicenses WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID;";

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

        public static bool ExistsForDriver(int driverId, bool isActive = false, bool notExpired = false)
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
