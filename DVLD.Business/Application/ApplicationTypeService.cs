using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class ApplicationTypeService
    {
        public static bool Update(ApplicationType applicationType,
            string newTitle, decimal newFees)
        {
            if (string.IsNullOrWhiteSpace(newTitle) || newFees < 0)
                return false;

            try
            {
                return ApplicationTypeRepository.Update(applicationType, newTitle, newFees);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get all application types.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetTitleAndFees(ApplicationType applicationType)
        {
            try
            {
                return ApplicationTypeRepository.GetTitleAndFees(applicationType);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get all application types.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return ApplicationTypeRepository.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get all application types.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static decimal GetFees(ApplicationType applicationType)
        {
            try
            {
                return ApplicationTypeRepository.GetFees(applicationType);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get all application types.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
