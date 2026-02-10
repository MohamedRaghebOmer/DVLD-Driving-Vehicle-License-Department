using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data.Settings;

namespace DVLD.Data
{
    public static class ApplicationData
    {
        public static int AddApplication(Application application)
        {
            string query = @"INSERT INTO Applications (ApplicantPersonId, ApplicationDate, ApplicationTypeId, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserId)
                            VALUES (@ApplicantPersonId, @ApplicationDate, @ApplicationTypeId, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserId);
                            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicantPersonId", application.ApplicantPersonId);
                    command.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                    command.Parameters.AddWithValue("@ApplicationTypeId", (int)application.ApplicationTypeId);
                    command.Parameters.AddWithValue("@ApplicationStatus", (int)application.ApplicationStatus);
                    command.Parameters.AddWithValue("@LastStatusDate", application.LastStatusDate);
                    command.Parameters.AddWithValue("@PaidFees", application.PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserId", application.CreatedByUserId);
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int newApplicationId))
                        return newApplicationId;
                }

                return -1; // Indicate failure to insert
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while insertin into Applications table.", ex);
                throw;
            }
        }

        public static DataTable GetAllApplications()
        {
            string query = @"SELECT * FROM Applications";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable applicationsTable = new DataTable();
                        applicationsTable.Load(reader);
                        return applicationsTable;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving all applications from Applications table.", ex);
                throw;
            }
        }

        public static Application GetApplication(int applicationId)
        {
            string query = @"SELECT * FROM Applications WHERE ApplicationId = @appId";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@appId", applicationId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Application
                            (
                                applicationId: Convert.ToInt32(reader["ApplicationID"]),
                                applicantPersonId: Convert.ToInt32(reader["ApplicantPersonId"]),
                                applicationDate: Convert.ToDateTime(reader["ApplicationDate"]),
                                applicationTypeId: (ApplicationType)Convert.ToInt32(reader["ApplicationTypeId"]),
                                applicationStatus: (ApplicationStatus)Convert.ToInt32(reader["ApplicationStatus"]),
                                lastStatusDate: Convert.ToDateTime(reader["LastStatusDate"]),
                                paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserId"])
                            );
                        }
                    }
                }
                return null; // Not found
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving application from Applications table.", ex);
                throw;
            }
        }

        public static bool Exists(int applicationId)
        {
            string query = @"SELECT 1 FROM Applications WHERE ApplicationID = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", applicationId);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence of application in Applications table.", ex);
                throw;

            }
        }

        public static ApplicationStatus GetApplicationStatus(int applicationId)
        {
            string query = @"SELECT ApplicationStatus FROM Applications WHERE ApplicationID = @id;";
           
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", applicationId);
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && int.TryParse(result.ToString(), out int statusValue))
                        return (ApplicationStatus)statusValue;
                }
                return 0; // Default value if not found (Unreachable in normal flow since application should exist)
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving application status from Applications table.", ex);
                throw;
            }
        }

        public static ApplicationType GetApplicationType(int applicationId)
        {
            string query = @"SELECT ApplicationTypeId FROM Applications WHERE ApplicationID = @id;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", applicationId);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int typeValue))
                        return (ApplicationType)typeValue;
                }
                return 0; // Default value if not found (Unreachable in normal flow since application should exist)
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving application type from Applications table.", ex);
                throw;
            }
        }

        public static bool Update(Application application)
        {
            string query = @"UPDATE Applications SET ApplicationStatus = @ApplicationStatus, 
                            LastStatusDate = @LastStatusDate, PaidFees = @PaidFees 
                            WHERE ApplicationId = @ApplicationId";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationStatus", (int)application.ApplicationStatus);
                    command.Parameters.AddWithValue("@LastStatusDate", application.LastStatusDate);
                    command.Parameters.AddWithValue("@PaidFees", application.PaidFees);
                    command.Parameters.AddWithValue("@ApplicationId", application.ApplicationId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }

            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating application in Applications table.", ex);
                throw;
            }

        }
    }
}