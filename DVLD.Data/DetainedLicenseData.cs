using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Data.Settings;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Data
{
    public static class DetainedLicenseData
    {
        public static int Add(int licenseId, decimal fineFees)
        {
            string query = @"INSERT INTO DetainedLicenses (LicenseID, DetainDate, FineFees, CreatedByUserID)
                            VALUES (@licenseId, GETDATE(), @fineFees, @createdByUserId);
                            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@licenseId", licenseId);
                    command.Parameters.AddWithValue("@fineFees", fineFees);
                    command.Parameters.AddWithValue("@createdByUserId", LoggedInUserInfo.UserId);
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int detainedLicenseId))
                        return detainedLicenseId;
                    return -1; // Indicate failure
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into DetainedLicenses", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = @"SELECT * FROM DetainedLicenses;";

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
                                reader["ReleaseDate"] != null ? (DateTime?)Convert.ToDateTime(reader["ReleaseDate"]) : null,
                                reader["ReleasedByUserID"] != null ? (int?)Convert.ToInt32(reader["ReleasedByUserID"]) : null,
                                reader["ReleaseApplicationID"] != null ? (int?)Convert.ToInt32(reader["ReleaseApplicationID"]) : null
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
            string query = @"SELECT 1 FROM DetainedLicenses WHERE LicenseID = @licenseId AND IsReleased = @isReleased;";

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

        public static bool Release(int licenseId, int releaseApplicationId)
        {
            string query = @"UPDATE DetainedLicenses SET IsReleased = 1, ReleaseDate = GETDATE(), ReleasedByUserID = @releasedByUserId, ReleaseApplicationID = @releaseApplicationId WHERE DetainedID = @detainedLicenseId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@detainedLicenseId", licenseId);
                    command.Parameters.AddWithValue("@releasedByUserId", LoggedInUserInfo.UserId);
                    command.Parameters.AddWithValue("@releaseApplicationId", releaseApplicationId);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while releasing detained license.", ex);
                throw;
            }
        }
    }
}