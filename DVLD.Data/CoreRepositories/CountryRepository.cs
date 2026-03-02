using DVLD.Core.Logging;
using DVLD.Data.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.Data
{
    public static class CountryRepository
    {
        public static string GetName(int countryId)
        {
            string query = @"SELECT CountryName
                            FROM Countries
                            WHERE CountryId = @countryId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryId", countryId);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    return result?.ToString(); // Return null if not found
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from Countries table.", ex);
                throw;
            }
        }

        public static bool Exists(int countryId)
        {
            string query = @"SELECT 1
                            FROM Countries
                            WHERE CountryId = @countryId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryId", countryId);

                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from Countries.", ex);
                throw;
            }
        }

        public static bool Exists(string countryName, int excludedId = -1)
        {
            string query = @"SELECT 1 
                            FROM Countries 
                            WHERE LOEWR(CountryName) = LOWER(@countryName) AND CountryId != @excludedId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@excludedId", excludedId);
                    command.Parameters.AddWithValue("@countryName", countryName);

                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from Countries.", ex);
                throw;
            }

        }

        public static DataTable GetAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand("SELECT * FROM Countries ORDER BY CountryName;", connection))
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

                    return null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting all from Countries.", ex);
                throw;
            }
        }
    }
}
