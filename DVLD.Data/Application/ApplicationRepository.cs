using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.Data
{
    public static class ApplicationRepository
    {
        public static int Add(Application application)
        {
            string query = @"INSERT INTO [dbo].[Applications]
                            (
                                [ApplicantPersonID],
                                [ApplicationDate],
                                [ApplicationTypeID],
                                [ApplicationStatus],
                                [LastStatusDate],
                                [PaidFees],
                                [CreatedByUserID]
                            )
                            VALUES
                            (
                                @ApplicantPersonID,
                                GETDATE(),
                                @ApplicationTypeID,
                                1,
                                GETDATE(),
                                (SELECT ApplicationFees 
                                 FROM ApplicationTypes 
                                 WHERE ApplicationTypeID = @ApplicationTypeID),
                                @CreatedByUserID
                            );
                            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicantPersonID", application.ApplicantPersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", (int)application.ApplicationTypeID);
                    command.Parameters.AddWithValue("@CreatedByUserID", LoggedInUserInfo.UserId);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int newApplicationId))
                        return newApplicationId;
                }

                return -1; // Indicate failure to insert
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting application into Applications table.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
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

        public static Application GetById(int applicationId)
        {
            string query = @"SELECT * FROM Applications WHERE ApplicationID = @appId";

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
                                applicantPersonId: Convert.ToInt32(reader["ApplicantPersonID"]),
                                applicationDate: Convert.ToDateTime(reader["ApplicationDate"]),
                                applicationTypeId: (ApplicationType)Convert.ToInt32(reader["ApplicationTypeID"]),
                                applicationStatus: (ApplicationStatus)Convert.ToInt32(reader["ApplicationStatus"]),
                                lastStatusDate: Convert.ToDateTime(reader["LastStatusDate"]),
                                paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserID"])
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

        public static bool ExistsForPerson(int personId, ApplicationType applicationType, ApplicationStatus applicationStatus)
        {
            string query = @"SELECT 1 FROM Applications 
                            WHERE ApplicantPersonId = @personId AND ApplicationTypeId = @applicationType AND ApplicationStatus = @applicationStatus";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personId", personId);
                    command.Parameters.AddWithValue("@applicationType", (int)applicationType);
                    command.Parameters.AddWithValue("@applicationStatus", (int)applicationStatus);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking if person has application of specific type in Applications table.", ex);
                throw;
            }
        }

        public static bool UpdateApplicationStatus(int applicationId, ApplicationStatus newStatus, bool autoUpdateLastStatusDate = true)
        {
            string query = @"UPDATE Applications SET ApplicationStatus = @applicationStatus";

            if (autoUpdateLastStatusDate)
                query += ", LastStatusDate = GETDATE()";

            query += " WHERE ApplicationID = @applicationId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@applicationStatus", (int)newStatus);
                    command.Parameters.AddWithValue("@applicationId", applicationId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating application status in Applications table.", ex);
                throw;
            }
        }

        public static bool Cancel(int localDrivingLicenseApplicationId)
        {
            string query = @"UPDATE A
                            SET
                                A.ApplicationStatus = 2,
                                A.LastStatusDate = GETDATE()
                            FROM Applications A
                            INNER JOIN LocalDrivingLicenseApplications L
                                ON L.ApplicationID = A.ApplicationID
                            WHERE L.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while cancelling application in Applications table.", ex);
                throw;
            }
        }

        public static bool Delete(int localDrivingLicenseApplicationId)
        {
            string query = @"BEGIN TRANSACTION;
                            DECLARE @ApplicationID INT;

                            SELECT @ApplicationID = ApplicationID
                            FROM LocalDrivingLicenseApplications
                            WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId;

                            DELETE FROM LocalDrivingLicenseApplications
                            WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId;

                            DELETE FROM Applications
                            WHERE ApplicationID = @ApplicationID;

                            COMMIT;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while deleting application from Applications table.", ex);
                throw;
            }
        }
    }
}