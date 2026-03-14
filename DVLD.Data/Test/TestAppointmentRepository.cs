using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.Data
{
    public static class TestAppointmentRepository
    {
        public static int Add(TestAppointment testAppointment)
        {
            string query = @"INSERT INTO TestAppointments
                            (
                                TestTypeID,
                                LocalDrivingLicenseApplicationID,
                                AppointmentDate,
                                PaidFees,
                                CreatedByUserID
                            )
                            SELECT
                                @TestTypeId,
                                @LocalDrivingLicenseApplicationId,
                                @AppointmentDate,
                                t.TestTypeFees
                                  + CASE WHEN a.ApplicationTypeID = 7 THEN COALESCE(at.ApplicationFees, 0) ELSE 0 END AS PaidFees,
                                @CreatedByUserId
                            FROM TestTypes t
                            LEFT JOIN LocalDrivingLicenseApplications ldla
                                ON ldla.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationId
                            LEFT JOIN Applications a
                                ON a.ApplicationID = ldla.ApplicationID
                            LEFT JOIN ApplicationTypes at
                                ON at.ApplicationTypeID = a.ApplicationTypeID
                            WHERE t.TestTypeID = @TestTypeId;
                            
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
                        command.Parameters.AddWithValue("@CreatedByUserId", LoggedInUserInfo.UserId);
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
            string query = @"SELECT * FROM TestAppointments 
                            WHERE TestAppointmentID = @TestAppointmentId";

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
                                    isLocked: Convert.ToBoolean(reader["IsLocked"]),
                                    retakeTestApplicationID: reader["RetakeTestApplicationID"] != DBNull.Value ? (int?)reader["RetakeTestApplicationID"] : null
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

        public static int GetIdByLocalAppId(int localDrivingLicenseApplicationId)
        {
            string query = @"SELECT TestAppointmentID FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId;";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while retrieving TestAppointments for LocalDrivingLicenseApplication with ID {localDrivingLicenseApplicationId}.", ex);
                throw;
            }
        }

        public static TestAppointment GetByLocalAppId(int localDrivingLicenseApplicationId)
        {
            string query = @"SELECT * FROM TestAppointments
                            WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TestAppointment
                            (
                                testAppointmentId: Convert.ToInt32(reader["TestAppointmentID"]),
                                testTypeId: (TestType)Convert.ToInt32(reader["TestTypeID"]),
                                localDrivingLicenseApplicationId: Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]),
                                appointmentDate: Convert.ToDateTime(reader["AppointmentDate"]),
                                paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserID"]),
                                isLocked: Convert.ToBoolean(reader["IsLocked"]),
                                retakeTestApplicationID: reader["RetakeTestApplicationID"] != DBNull.Value ? (int?)reader["RetakeTestApplicationID"] : null
                            );
                        }
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while retrieving TestAppointments for LocalDrivingLicenseApplication with ID {localDrivingLicenseApplicationId}.", ex);
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

        public static bool ExistsByLocalApplicationId(int localDrivingLicenseApplicationId, bool isLocked = false)
        {
            string query = @"SELECT 1 
                            FROM TestAppointments 
                            WHERE LocalDrivingLicenseApplicationID = 
                            @localDrivingLicenseApplicationId 
                            AND IsLocked = @isLocked;";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                    command.Parameters.AddWithValue("@isLocked", isLocked);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking existence of TestAppointments for LocalDrivingLicenseApplication with ID {localDrivingLicenseApplicationId}.", ex);
                throw;
            }
        }

        public static bool ExistsByRetakeTestAppId(int retakeTestAppId)
        {
            string query = @"SELECT 1 
                            FROM TestAppointments 
                            WHERE RetakeTestApplicationID = 
                            @retakeTestAppId;";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@retakeTestAppId", retakeTestAppId);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking existence of TestAppointments for RetakeTestApplication with ID {retakeTestAppId}.", ex);
                throw;
            }
        }

        public static DataTable GetShortAppointmentInfo(int localDrivingLicenseApplicationId, TestType testType)
        {
            string query = @"SELECT TestAppointmentID,
                                    AppointmentDate,
                                    PaidFees,
                                    IsLocked
                            FROM TestAppointments 
                            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            AND TestTypeID = @TestTypeID;";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationId);
                        command.Parameters.AddWithValue("@TestTypeID", (int)testType);
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
                AppLogger.LogError($"DAL: Error while retrieving TestAppointments for LocalDrivingLicenseApplication with ID {localDrivingLicenseApplicationId}.", ex);
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while check TestAppointment with ID{testAppointmentId} is locked.", ex);
                throw;
            }
        }

        public static bool ExistsByApplication(TestType testType, int localDrivingLicenseApplicationId)
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

        public static bool Update(TestAppointment testAppointment)
        {
            string query = @"UPDATE TestAppointments 
                            SET AppointmentDate = @newDate 
                            WHERE TestAppointmentID = @TestAppointmentId";

            try
            {
                using (var connection = new SqlConnection(DataSettings.connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newDate", testAppointment.AppointmentDate);
                        command.Parameters.AddWithValue("@TestAppointmentId", testAppointment.TestAppointmentId);
                        connection.Open();
                        return command.ExecuteNonQuery() > 0; // Returns true if at least one row was updated
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while updating TestAppointment with ID {testAppointment.TestAppointmentId} date.", ex);
                throw;
            }
        }

        public static bool Lock(int testAppointmentId)
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

        public static bool Delete(int testAppointmentId)
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
