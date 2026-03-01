using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.Data
{
    public static class TestTypeRepository
    {
        public static DataTable Get(TestType testType)
        {
            string query = @"SELECT * FROM TestTypes
                            WHERE TestTypeID = @id;";

            try
            {
                using (var con = new SqlConnection(DataSettings.connectionString))
                using (var com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@id", (int)testType);
                    con.Open();
                    using (var reader = com.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            return dt;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while retrieving TestType for TestTypeId = {(int)testType}.", ex);
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

        public static bool Update(TestType testType, string newTitle,
            string newDescription, decimal newFees)
        {
            string query = @"UPDATE TestTypes
                            SET
                            TestTypeTitle = @title, TestTypeDescription = @desc,
                            TestTypeFees = @fees
                            WHERE TestTypeID = @id;";

            try
            {
                using (var con = new SqlConnection(DataSettings.connectionString))
                using (var com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@title", newTitle);
                    com.Parameters.AddWithValue("@desc", newDescription);
                    com.Parameters.AddWithValue("@fees", newFees);
                    com.Parameters.AddWithValue("@id", (int)testType);
                    con.Open();
                    return com.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DLL: Error while updating TestTypes where TestTypeId = {(int)testType}.", ex);
                throw;
            }
        }
    }
}
