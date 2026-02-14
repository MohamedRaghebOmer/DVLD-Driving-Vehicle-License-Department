using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;

namespace DVLD.Data
{
    public static class DetainedLicenseData
    {
        public static int DetainNewLicense(DetainedLicense detainedLicense)
        {
            string query = @"INSERT INTO DetainedLicenses (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID)
                            VALUES (@licenseId, @detainDate, @fineFees, @createdByUserId, @isReleased, @releaseDate, @releasedByUserId, @releaseApplicationId);
                            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@licenseId", detainedLicense.LicenseID);
                    command.Parameters.AddWithValue("@detainDate", detainedLicense.DetainDate);
                    command.Parameters.AddWithValue("@fineFees", detainedLicense.FineFees);
                    command.Parameters.AddWithValue("@createdByUserId", detainedLicense.CreatedByUserID);
                    command.Parameters.AddWithValue("@isReleased", detainedLicense.IsReleased);
                    command.Parameters.AddWithValue("@releaseDate", detainedLicense.ReleaseDate);
                    command.Parameters.AddWithValue("@releasedByUserId", detainedLicense.ReleasedByUserID);
                    command.Parameters.AddWithValue("@releaseApplicationId", detainedLicense.ReleaseApplicationID);
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

        public static DetainedLicense GetDetainedLicenseById(int detainedLicenseId)
        {
            string query = @"SELECT * FROM DetainedLicenses WHERE DetainedID = @detainedLicenseId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@detainedLicenseId", detainedLicenseId);
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
                                Convert.ToDateTime(reader["ReleaseDate"]),
                                Convert.ToInt32(reader["ReleasedByUserID"]),
                                Convert.ToInt32(reader["ReleaseApplicationID"])
                            );
                        }
                    }
                }
                
                return null;
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from DetainedLicenses", ex);
                throw;
            }
        }

        public static DetainedLicense GetDetainedLicenseByLicenseId(int licenseId)
        {
            string query = @"SELECT * FROM DetainedLicenses WHERE LicenseID = @licenseId;";

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
                                Convert.ToDateTime(reader["ReleaseDate"]),
                                Convert.ToInt32(reader["ReleasedByUserID"]),
                                Convert.ToInt32(reader["ReleaseApplicationID"])
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

        public static bool Exists(int detainedLicenseId, bool isReleased = false)
        {
            string query = @"SELECT 1 FROM DetainedLicenses WHERE DetainedID = @detainedLicenseId AND IsReleased = @isReleased;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@detainedLicenseId", detainedLicenseId);
                    command.Parameters.AddWithValue("@isReleased", isReleased);
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

        public static bool DoesLicenseExist(int licenseId, int excludedDetainedLicenseId = -1)
        {
            string query = @"SELECT 1 FROM DetainedLicenses WHERE LicenseID = @licenseId AND DetainID != @excludedDetainedLicenseId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@licenseId", licenseId);
                    command.Parameters.AddWithValue("@excludedDetainedLicenseId", excludedDetainedLicenseId);
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

        public static bool ReleaseDetainedLicense(int detainedLicenseId, int releasedByUserId, int releaseApplicationId)
        {
            string query = @"UPDATE DetainedLicenses SET IsReleased = 1, ReleaseDate = GETDATE(), ReleasedByUserID = @releasedByUserId, ReleaseApplicationID = @releaseApplicationId WHERE DetainedID = @detainedLicenseId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@detainedLicenseId", detainedLicenseId);
                    command.Parameters.AddWithValue("@releasedByUserId", releasedByUserId);
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