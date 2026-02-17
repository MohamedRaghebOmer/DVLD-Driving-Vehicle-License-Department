using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class ApplicationBusiness
    {
        public static Application NewLocalDrivingLicenseApplication(Application application, LicenseClass licenseClass)
        {
            ApplicationValidator.NewLocalDrivingLicenseValidator(application, licenseClass);

            try
            {
                // Insert the application into the database and get the new ApplicationId
                int newApplicationId = ApplicationData.Add(application);
                int newLocalDrivingLicenseApplicationId = LocalDrivingLicenseApplicationData.Add(new LocalDrivingLicenseApplication(newApplicationId, licenseClass));
                if (newApplicationId != -1 && newLocalDrivingLicenseApplicationId != -1)
                    return ApplicationData.GetById(newApplicationId);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating new local driving license", ex);
                throw;
            }
        }

        public static Application NewInternationalLicenseApplication(Application application)
        {
            ApplicationValidator.NewInternationalLicenseValidator(application);

            try
            {
                int newApplicationId = ApplicationData.Add(application);
                if (newApplicationId != -1)
                    return ApplicationData.GetById(newApplicationId);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating new international driving license application", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return ApplicationData.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while retrieving all applications", ex);
                throw;
            }
        }

        public static Application GetById(int applicationId)
        {
            if (applicationId <= 0)
                return null;

            try
            {
                return ApplicationData.GetById(applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while retrieving application by id", ex);
                throw;
            }
        }

        public static bool Exists(int applicationId)
        {
            try
            {
                return ApplicationData.Exists(applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if application exists", ex);
                throw;
            }
        }
    }
}
