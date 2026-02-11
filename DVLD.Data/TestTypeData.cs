using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Data.Settings;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Data
{
    public static class TestTypeData
    {
        public static decimal GetTestTypeFees(TestType testType)
        {
            string query = "SELECT TestTypeFees FROM TestTypes WHERE TestTypeId = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", (int)testType);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && decimal.TryParse(result.ToString(), out decimal fees))
                        return fees;
                    return -1; // Return -1 if fees not found or parsing fails
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while retrieving TestTypeFees for TestTypeId = {(int)testType}.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = "SELECT * FROM TestTypes;";

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
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            return dt;
                        }
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving all TestTypes.", ex);
                throw;
            }
        }

        public static bool UpdateFees(TestType testType, decimal newFees)
        {
            string query = @"UPDATE TestTypes SET TestTypeFees = @fees WHERE TestTypeId = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fees", newFees);
                    command.Parameters.AddWithValue("@id", (int)testType);

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DLL: Error while updating Drivers where driver id = {(int)testType}.", ex);
                throw;
            }

        }
    }
}
