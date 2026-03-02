using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class CountryService
    {
        public static string GetName(int countryId)
        {
            if (countryId <= 0)
                return null;

            try
            {
                return CountryRepository.GetName(countryId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading country name with Id = {countryId}");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return CountryRepository.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading all countries.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
