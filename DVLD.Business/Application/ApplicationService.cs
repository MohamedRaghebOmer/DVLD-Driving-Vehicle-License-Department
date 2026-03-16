using DVLD.Business.EntityValidators;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class ApplicationService
    {
        private static int NewLocalDrivingLicenseApplication(Application application, LicenseClass licenseClass)
        {
            ApplicationValidator.ValidateNewLocalDrivingLicenseApp(application, licenseClass);

            try
            {
                // Insert the application into the database and get the new ApplicationId
                int newApplicationId = ApplicationRepository.Add(application);

                var local = new LocalDrivingLicenseApplication
                {
                    ApplicationID = newApplicationId,
                    LicenseClassID = licenseClass
                };

                int newLocalId = LocalDrivingLicenseApplicationRepository.Add(local);
                if (newLocalId > 0 && newApplicationId > 0)
                    return newApplicationId;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating new local driving license");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static int NewRetakeTestApplication(Application application)
        {
            ApplicationValidator.ValidateNewRetakeTestApplication(application);

            try
            {
                // Insert the application into the database and get the new ApplicationId
                return ApplicationRepository.Add(application);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating new retake test application");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static int Add(Application application, LicenseClass licenseClass)
        {
            switch (application.ApplicationTypeID)
            {
                case ApplicationType.NewLocalDrivingLicenseService:
                    return NewLocalDrivingLicenseApplication(application, licenseClass);

            }

            return -1;
        }

        //public static Application NewInternationalLicenseApplication(Application application)
        //{
        //    ApplicationValidator.NewInternationalLicenseValidator(application);

        //    try
        //    {
        //        int newApplicationId = ApplicationRepository.Add(application);
        //        if (newApplicationId != -1)
        //            return ApplicationRepository.GetById(newApplicationId);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        AppLogger.LogError("BLL: Error while creating new international driving license application", ex);
        //        throw new Exception("We encountered a technical issue. Please try again later.", ex);
        //    }
        //}

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

        public static Application GetByLocalAppId(int localAppId)
        {
            if (localAppId <= 0)
                return null;

            try
            {
                return ApplicationRepository.GetByLocalAppId(localAppId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while retrieving application by local application id = {localAppId}.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static Application GetByLicenseId(int licenseId)
        {
            if (licenseId <= 0)
                return null;

            try
            {
                return ApplicationRepository.GetByLicenseId(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while retrieving application by license id = {licenseId}.", ex);
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

        public static bool Cancel(int applicationId)
        {
            if (applicationId <= 0)
                return false;

            try
            {
                return ApplicationRepository.Cancel(applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while canceling application", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(int applicationId)
        {
            if (applicationId <= 0)
                return false;

            try
            {
                return ApplicationRepository.Delete(applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while deleting application", ex);
                throw new Exception("The application could not be deleted because there is related data associated with it.", ex);
            }
        }
    }
}
