using DVLD.Business.EntityValidators;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace DVLD.Business
{
    public static class CountryBusiness
    {        
        public static Country Save(Country country)
        {
            if (country == null)
                throw new ValidationException("Country cannot be empty.");

            // Add new country
            if (country.CountryID == -1)
            {
                CountryValidator.AddNewValidator(country);

                try
                {
                    return (CountryData.Add(country) != -1) ? CountryData.Get(country.CountryID) : null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"BLL: Error adding new country {country.CountryName}", ex);
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
            else // Update existing country
            {
                CountryValidator.UpdateValidator(country);

                try
                {
                    return (CountryData.Update(country)) ? CountryData.Get(country.CountryID) : null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"BLL: Error updating country {country.CountryName}", ex);
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static Country Get(int id)
        {
            if (id <= 0)
                return null;
             
            try
            {
                return CountryData.Get(id);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading country info with Id = {id}");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Country Get(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return null;

            try
            {
                return CountryData.Get(countryName);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading country info with name = {countryName}");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static string GetName(int countryId)
        {
            if (countryId <= 0)
                return null;

            try
            {
                return CountryData.GetName(countryId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading country name with Id = {countryId}");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static int GetIdByName(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return -1;

            try
            {
                return CountryData.GetIdByName(countryName);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading country Id with name = {countryName}");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAllNames()
        {
            try
            {
                return CountryData.GetAllNames();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading all countries.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exits(int countryId)
        {
            if (countryId < 1)
                return false;

            try
            {
                return CountryData.Exists(countryId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking country with id = {countryId} existance.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exists(string countryName, int excludedId = -1)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return false;
            
            try
            {
                return CountryData.Exists(countryName, excludedId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking country name = {countryName} existance for country with Id = {excludedId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
        
        public static bool Delete(int countryId)
        {
            if (countryId < 1)
                return false;

            try
            {
                return CountryData.Delete(countryId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting country with Id = {countryId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return false;

            try
            {
                return CountryData.Delete(countryName);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting country with name = {countryName}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
