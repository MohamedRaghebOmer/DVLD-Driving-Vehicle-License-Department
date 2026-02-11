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
        public static int AddNewTest(Test test)
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
                    cmd.Parameters.AddWithValue("@CreatedByUserId", test.CreatedByUserID);
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

        public static Test GetTestById(int testId)
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

        public static bool DoesTestAppointmentExists(int testAppointmentId)
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

        public static bool UpdateTestNotes(int testId, string newNotes)
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