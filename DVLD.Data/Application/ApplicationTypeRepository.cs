using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.Data
{
    public static class ApplicationTypeRepository
    {
        public static DataTable GetAll()
        {
            string query = @"SELECT ApplicationTypes.* FROM ApplicationTypes;";

            try
            {
                using (var con = new SqlConnection(DataSettings.connectionString))
                using (var com = new SqlCommand(query, con))
                {
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
                AppLogger.LogError($"DAL: Error while fetching all application types.", ex);
                throw;
            }
        }

        public static DataTable GetTitleAndFees(ApplicationType applicationType)
        {
            string query = @"SELECT ApplicationTypeTitle, ApplicationFees
                            FROM ApplicationTypes WHERE ApplicationTypeID = @id";

            try
            {
                using (var con = new SqlConnection(DataSettings.connectionString))
                using (var com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@id", (int)applicationType);
                    con.Open();

                    using (var reader = com.ExecuteReader())
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
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching application type and fees.", ex);
                throw;
            }
        }

        public static decimal GetFees(ApplicationType applicationType)
        {
            string query = "SELECT Fees FROM ApplicationTypes WHERE ApplicationTypeId = @ApplicationTypeId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationTypeId", (int)applicationType);
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && decimal.TryParse(result.ToString(), out decimal fees))
                        return fees;
                }
                return -1; // Return -1 if the application type is not found or if the value is invalid
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching fees for application type {applicationType}.", ex);
                throw;
            }
        }

        public static bool Update(ApplicationType applicationType, string newTitle,
            decimal newFees)
        {
            string query = @"UPDATE ApplicationTypes
                            SET 
                            ApplicationTypeTitle = @newTitle,
                            ApplicationFees = @newFees
                            WHERE ApplicationTypeID = @id;";

            try
            {
                using (var con = new SqlConnection(DataSettings.connectionString))
                using (var com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@newTitle", newTitle);
                    com.Parameters.AddWithValue("@newFees", newFees);
                    com.Parameters.AddWithValue("@id", (int)applicationType);
                    con.Open();
                    return com.ExecuteNonQuery() > 0;
                }

            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while updating fees for application type {applicationType}.", ex);
                throw;
            }
        }
    }
}