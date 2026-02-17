using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Entities;
using DVLD.Business.EntityValidators;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public static class CountryBusiness
    {        
        public static Country Save(Country country)
        {
            if (country == null)
                throw new ValidationException("Country cannot be empty.");

            // Add new country
            if (country.CountryId == -1)
            {
                CountryValidator.AddNewValidator(country);

                try
                {
                    return (CountryData.Add(country) != -1) ? CountryData.Get(country.CountryId) : null;
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
                    return (CountryData.Update(country)) ? CountryData.Get(country.CountryId) : null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"BLL: Error updating country {country.CountryName}", ex);
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static Country GetById(int id)
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

        public static Country GetByName(string countryName)
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

        public static DataTable GetAll()
        {
            try
            {
                return CountryData.GetAll();
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
