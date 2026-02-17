using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;

namespace DVLD.Data
{
    public static class TestData
    {
        public static int Add(Test test)
        {
            string query = @"INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID) 
                            VALUES (@TestAppointmentId, @TestResult, @Notes, @CreatedByUserId);
                            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection conn = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestAppointmentId", test.TestAppointmentID);
                    cmd.Parameters.AddWithValue("@TestResult", test.TestResult);
                    cmd.Parameters.AddWithValue("@Notes", test.Notes ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedByUserId", LoggedInUserInfo.UserId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int newTestId))
                        return newTestId;
                    return -1; // Indicate failure to retrieve new TestID
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into Test.", ex);
                throw;
            }
        }

        public static Test GetById(int testId)
        {
            string query = @"SELECT TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID 
                            FROM Tests WHERE TestID = @TestId";

            try
            {
                using (SqlConnection conn = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestId", testId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Test
                            (
                                testId: Convert.ToInt32(reader["TestID"]),
                                testAppointmentId: Convert.ToInt32(reader["TestAppointmentID"]),
                                testResult: Convert.ToBoolean(reader["TestResult"]),
                                notes: reader["Notes"] != DBNull.Value ? Convert.ToString(reader["Notes"]) : string.Empty,
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserID"])
                            );
                        }
                        return null; // No test found with the given ID
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving Test by ID.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = "SELECT * FROM Tests;";

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
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving all tests.", ex);
                throw;
            }
        }

        public static bool HasPassedThreeTests(int localDrivingLicenseApplicationId)
        {
            string query = @"SELECT CAST(CASE 
                            -- Condition 1: Check if the application exists
                            WHEN EXISTS (
                                SELECT 1 
                                FROM dbo.LocalDrivingLicenseApplications 
                                WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID
                            )
                            -- Condition 2: Check if there are exactly 3 locked TestAppointments
                            AND (
                                SELECT COUNT(*) 
                                FROM dbo.TestAppointments 
                                WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID
                                AND IsLocked = 1
                            ) = 3
                            -- Condition 3: Check if all 3 corresponding tests were passed (TestResult = 1)
                            -- We do this by ensuring the count of Passed Tests associated with Locked Appointments is also 3
                            AND (
                                SELECT COUNT(*) 
                                FROM dbo.Tests T
                                INNER JOIN dbo.TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
                                WHERE TA.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID
                                AND TA.IsLocked = 1 
                                AND T.TestResult = 1
                            ) = 3
                            THEN 1 
                            ELSE 0 
                        END AS BIT) AS HasPassed;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && bool.TryParse(result.ToString(), out bool hasPassed))
                        return hasPassed;
                    return false; // Return false if the application does not exist or if the value is invalid
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking if license for local driving license application ID {localDrivingLicenseApplicationId} has passed three tests.", ex);
                throw;
            }
        }

        public static bool ExistsForTestAppointment(int testAppointmentId)
        {
            string query = "SELECT 1 FROM Tests WHERE TestAppointmentID = @appointmentId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@appointmentId", testAppointmentId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"DAL: Error while chekcing TestAppointment existance with ID = {testAppointmentId}.", ex);
                throw;
            }
        }

        public static bool UpdateNotes(int testId, string newNotes)
        {
            string query = @"UPDATE Tests SET Notes = @newNotes WHERE TestID = @testId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newNotes", newNotes);
                    command.Parameters.AddWithValue("@testId", testId);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"DAL: Error while updating Test notes with id = {testId}.", ex);
                throw;
            }
        }
    }
}