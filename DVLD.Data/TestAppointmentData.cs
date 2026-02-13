using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Data
{
    public static class TestAppointmentData
    {
        public static int AddNewTestAppointment(TestAppointment testAppointment)
        {
            string query = @"INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserId)
                VALUES (@TestTypeId, @LocalDrivingLicenseApplicationId, @AppointmentDate, @PaidFees, @CreatedByUserId);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestTypeId", testAppointment.TestTypeId);
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationId", testAppointment.LocalDrivingLicenseApplicationId);
                        command.Parameters.AddWithValue("@AppointmentDate", testAppointment.AppointmentDate);
                        command.Parameters.AddWithValue("@PaidFees", testAppointment.PaidFees);
                        command.Parameters.AddWithValue("@CreatedByUserId", testAppointment.CreatedByUserId);
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int newId))
                            return newId;

                        return -1; // Indicate failure to retrieve new ID
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while inserting into TestAppointments.", ex);
                throw;
            }
        }

        public static TestAppointment GetById(int testAppointmentId)
        {
            string query = @"SELECT TestAppointmentId, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserId, IsLocked
                FROM TestAppointments WHERE TestAppointmentId = @TestAppointmentId";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestAppointmentId", testAppointmentId);
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new TestAppointment
                                (
                                    testAppointmentId: Convert.ToInt32(reader["TestAppointmentId"]),
                                    testTypeId: (TestType)Convert.ToInt32(reader["TestTypeID"]),
                                    localDrivingLicenseApplicationId: Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]),
                                    appointmentDate: Convert.ToDateTime(reader["AppointmentDate"]),
                                    paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                    createdByUserId: Convert.ToInt32(reader["CreatedByUserId"]),
                                    isLocked: Convert.ToBoolean(reader["IsLocked"])
                                );
                            }
                        }
                    }
                }

                return null; // Not found
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while retrieving TestAppointment with ID {testAppointmentId}.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = @"SELECT TestAppointmentId, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserId, IsLocked
                FROM TestAppointments";
            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while retrieving all TestAppointments.", ex);
                throw;
            }
        }

        public static bool Exists(int testAppointmentId)
        {
            string query = "SELECT 1 FROM TestAppointments WHERE TestAppointmentID = @id;";
            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", testAppointmentId);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking existence of TestAppointment with ID {testAppointmentId}.", ex);
                throw;
            }
        }

        public static bool IsLocked(int testAppointmentId)
        {
            string query = "SELECT 1 FROM TestAppointments WHERE TestAppointmentID = @id AND IsLocked = 1;";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", testAppointmentId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"DAL: Error while check TestAppointment with ID{testAppointmentId} is locked.", ex);
                throw;
            }
        }

        public static bool DoesApplicationExist(int localDrivingLicenseApplicationId, int excludedId = -1)
        {
            string query = @"SELECT 1 FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId AND TestAppointmentID != @excludedId;";
            
            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                        command.Parameters.AddWithValue("@excludedId", excludedId);
                        connection.Open();
                        return command.ExecuteScalar() != null; // Returns true if at least one record exists
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking existence of TestAppointment for Application ID {localDrivingLicenseApplicationId}.", ex);
                throw;
            }
        }

        public static bool DoesApplicationExist(TestType testType, int localDrivingLicenseApplicationId)
        {
            string query = @"SELECT 1 FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId AND TestTypeID = @testTypeId;";
            
            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                        command.Parameters.AddWithValue("@testTypeId", (int)testType);
                        connection.Open();
                        return command.ExecuteScalar() != null; // Returns true if at least one record exists
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking existence of TestAppointment for Application ID {localDrivingLicenseApplicationId} and Test Type {testType}.", ex);
                throw;
            }
        }

        public static bool UpdateTestAppointment(TestAppointment testAppointment)
        {
            string query = @"UPDATE TestAppointments SET TestTypeID = @TestTypeId, LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationId,
                            AppointmentDate = @AppointmentDate, PaidFees = @PaidFees, CreatedByUserId = @CreatedByUserId, IsLocked = @IsLocked
                            WHERE TestAppointmentId = @TestAppointmentId";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestTypeId", testAppointment.TestTypeId);
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationId", testAppointment.LocalDrivingLicenseApplicationId);
                        command.Parameters.AddWithValue("@AppointmentDate", testAppointment.AppointmentDate);
                        command.Parameters.AddWithValue("@PaidFees", testAppointment.PaidFees);
                        command.Parameters.AddWithValue("@CreatedByUserId", testAppointment.CreatedByUserId);
                        command.Parameters.AddWithValue("@IsLocked", testAppointment.IsLocked);
                        command.Parameters.AddWithValue("@TestAppointmentId", testAppointment.TestAppointmentId);
                        connection.Open();

                        return command.ExecuteNonQuery() > 0; // Returns true if at least one row was updated
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while updating TestAppointment with ID {testAppointment.TestAppointmentId}.", ex);
                throw;
            }
        }

        public static bool LockTestAppointment(int testAppointmentId)
        {
            string query = @"UPDATE TestAppointments SET IsLocked = 1 WHERE TestAppointmentId = @TestAppointmentId";
            
            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestAppointmentId", testAppointmentId);
                        connection.Open();
                        return command.ExecuteNonQuery() > 0; // Returns true if at least one row was updated
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while locking TestAppointment with ID {testAppointmentId}.", ex);
                throw;
            }
        }

        public static bool DeleteTestAppointment(int testAppointmentId)
        {
            string query = @"DELETE FROM TestAppointments WHERE TestAppointmentId = @TestAppointmentId";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestAppointmentId", testAppointmentId);
                        connection.Open();
                        return command.ExecuteNonQuery() > 0; // Returns true if at least one row was deleted
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting TestAppointment with ID {testAppointmentId}.", ex);
                throw;
            }
        }
    }
}
