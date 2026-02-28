using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Collections.Generic;
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
            catch(Exception ex)
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
                AppLogger.LogError($"BLL: Error while getting fees for application type {applicationType}.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool UpdateFees(ApplicationType applicationType, decimal newFees)
        {
            if (newFees < 0)
                throw new ValidationException("Fees cannot be negative.");

            try
            {
                return ApplicationTypeRepository.UpdateFees(applicationType, newFees);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating fees for application type {applicationType}.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
