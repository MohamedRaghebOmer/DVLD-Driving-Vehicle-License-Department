using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data.Settings;

namespace DVLD.Data
{
    public static class LocalDrivingLicenseApplicationData
    {
        public static int Add(LocalDrivingLicenseApplication localDrivingLicenseApplication)
        {
            string query = @"INSERT INTO LocalDrivingLicenseApplications (ApplicationId, LicenseClassId)
                            VALUES (@ApplicationId, @LicenseClassId);
                            SELECT SCOPE_IDENTITY();";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationId", localDrivingLicenseApplication.ApplicationId);
                    command.Parameters.AddWithValue("@LicenseClassId", (int)localDrivingLicenseApplication.LicenseClassId);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int newLocalDrivingLicenseApplicationId))
                        return newLocalDrivingLicenseApplicationId;
                }
                return -1; // Indicate failure to insert
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into LocalDrivingLicenseApplications table.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = @"SELECT * FROM LocalDrivingLicenseApplications";
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
                AppLogger.LogError("DAL: Error while retrieving data from LocalDrivingLicenseApplications table.", ex);
                throw;
            }
        }

        public static LocalDrivingLicenseApplication GetById(int localDrivingLicenseApplicationId)
        {
            string query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationId = @LocalDrivingLicenseApplicationId";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LocalDrivingLicenseApplication
                            (
                                localDrivingLicenseApplicationId: Convert.ToInt32(reader["LocalDrivingLicenseApplicationId"]),
                                applicationId: Convert.ToInt32(reader["ApplicationId"]),
                                licenseClassId: (LicenseClass)Convert.ToInt32(reader["LicenseClassId"])
                            );
                        }
                    }
                }
                return null; // Not found
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving a specific record from LocalDrivingLicenseApplications table.", ex);
                throw;
            }
        }

        public static LocalDrivingLicenseApplication GetByApplicationId(int applicationId)
        {
            string query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationId = @ApplicationId";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationId", applicationId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LocalDrivingLicenseApplication
                            (
                                localDrivingLicenseApplicationId: Convert.ToInt32(reader["LocalDrivingLicenseApplicationId"]),
                                applicationId: Convert.ToInt32(reader["ApplicationId"]),
                                licenseClassId: (LicenseClass)Convert.ToInt32(reader["LicenseClassId"])
                            );
                        }
                    }
                }
                return null; // Not found
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving a specific record from LocalDrivingLicenseApplications table by ApplicationId.", ex);
                throw;
            }
        }

        public static bool Exists(int applicationId, int excludedId)
        {
            string query = @"SELECT 1 FROM LocalDrivingLicenseApplications WHERE ApplicationId = @ApplicationId AND LocalDrivingLicenseApplicationId != @ExcludedId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationId", applicationId);
                    command.Parameters.AddWithValue("@ExcludedId", excludedId);
                    connection.Open();

                    return command.ExecuteScalar() != null; // If a record exists, it means the ApplicationId is used
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking if ApplicationId is used in LocalDrivingLicenseApplications table.", ex);
                throw;
            }
        }

        public static bool DoesPersonHaveApplication(int personId, LicenseClass licenseClass, ApplicationType applicationType, ApplicationStatus status)
        {
            string query = @"SELECT 1 FROM LocalDrivingLicenseApplications ldl
                            INNER JOIN Applications a ON ldl.ApplicationId = a.ApplicationId
                            WHERE a.PersonId = @PersonId AND ldl.LicenseClassId = @LicenseClassId AND a.ApplicationStatus = @Status AND a.ApplicationTypeID = @applicationType;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonId", personId);
                    command.Parameters.AddWithValue("@LicenseClassId", (int)licenseClass);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@applicationType", (int)applicationType);
                    connection.Open();
                    return command.ExecuteScalar() != null; // If a record exists, it means the person has a new application for the specified license class
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking if a person has a new application for a specific license class in LocalDrivingLicenseApplications table.", ex);
                throw;
            }
        }

        public static bool Update(LocalDrivingLicenseApplication localDrivingLicenseApplication)
        {
            string query = @"UPDATE LocalDrivingLicenseApplications
                            SET LicenseClassId = @LicenseClassId
                            WHERE LocalDrivingLicenseApplicationId = @LocalDrivingLicenseApplicationId";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClassId", (int)localDrivingLicenseApplication.LicenseClassId);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationId", localDrivingLicenseApplication.LocalDrivingLicenseApplicationId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0; // Returns true if at least one record was updated
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating a record in LocalDrivingLicenseApplications table.", ex);
                throw;
            }
        }
    }
}