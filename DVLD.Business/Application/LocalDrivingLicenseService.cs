using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;
using System.Diagnostics;

namespace DVLD.Business
{
    public static class LocalDrivingLicenseService
    {
        public static Application NewLocalDrivingLicenseApplication(Application application, LicenseClass licenseClass)
        {
            ApplicationValidator.NewLocalDrivingLicenseValidator(application, licenseClass);

            try
            {
                // Insert the application into the database and get the new ApplicationId
                int newApplicationId = ApplicationRepository.Add(application);
                int newLocalDrivingLicenseApplicationId = LocalDrivingLicenseApplicationRepository.Add(new LocalDrivingLicenseApplication(newApplicationId, licenseClass));
                if (newApplicationId != -1 && newLocalDrivingLicenseApplicationId != -1)
                    return ApplicationRepository.GetById(newApplicationId);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating new local driving license");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Application NewInternationalLicenseApplication(Application application)
        {
            ApplicationValidator.NewInternationalLicenseValidator(application);

            try
            {
                int newApplicationId = ApplicationRepository.Add(application);
                if (newApplicationId != -1)
                    return ApplicationRepository.GetById(newApplicationId);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating new international driving license application", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return ApplicationRepository.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while retrieving all applications", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Application GetById(int applicationId)
        {
            if (applicationId <= 0)
                return null;

            try
            {
                return ApplicationRepository.GetById(applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while retrieving application by id", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exists(int applicationId)
        {
            try
            {
                return ApplicationRepository.Exists(applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if application exists", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
