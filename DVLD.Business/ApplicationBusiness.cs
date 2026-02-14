using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public static class ApplicationBusiness
    {
        public static int NewLocalDrivingLicenseApplication(Application application, LicenseClass licenseClass)
        {
            if (application == null)
                throw new ValidationException("Application cannot be empty.");

            if (application.ApplicationTypeID != ApplicationType.NewLocalDrivingLicenseService)
                throw new ValidationException("Application type must be New Local Driving License.");

            try
            {
                // Validate the application entity
                ApplicationValidator.NewLocalDrivingLicenseValidator(application, licenseClass);

                // Insert the application into the database and get the new ApplicationId
                Application DBVersion = application;
                DBVersion.ApplicationDate = DateTime.Now;
                DBVersion.ApplicationStatus = ApplicationStatus.New;
                DBVersion.LastStatusDate = DateTime.Now;

                int newApplicationId = ApplicationData.AddApplication(DBVersion);
                LocalDrivingLicenseApplicationData.Add(new LocalDrivingLicenseApplication(newApplicationId, licenseClass));
                return newApplicationId;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating new local driving license", ex);
                throw;
            }
        }

        public static int RenewDrivingLicenseApplication(Application application, LicenseClass licenseClass)
        {
            if (application == null)
                throw new ValidationException("Application cannot be empty.");

            if (application.ApplicationTypeID != ApplicationType.RenewDrivingLicenseService)
                throw new ValidationException("Application type must be Renew Driving License.");

            try
            {
                // Validate the application entity
                ApplicationValidator.RenewDrivingLicenseValidator(application, licenseClass);

                // Insert the application into the database and get the new ApplicationId
                Application DBVersion = application;
                DBVersion.ApplicationDate = DateTime.Now;
                DBVersion.ApplicationStatus = ApplicationStatus.New;
                DBVersion.LastStatusDate = DateTime.Now;

                int newApplicationId = ApplicationData.AddApplication(DBVersion);
                LocalDrivingLicenseApplicationData.Add(new LocalDrivingLicenseApplication(newApplicationId, licenseClass));
                return newApplicationId;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating renew driving license application", ex);
                throw;
            }
        }

        public static int ReplacementForLostDrivingLicenseApplication(Application application, LicenseClass licenseClass)
        {
            if (application == null)
                throw new ValidationException("Application cannot be empty.");

            if (application.ApplicationTypeID != ApplicationType.ReplacementForLostDrivingLicense)
                throw new ValidationException("Application type must be Replacement for Lost Driving License.");

            try
            {
                // Validate the application entity
                ApplicationValidator.ReplacementForLostDrivingLicenseValidator(application, licenseClass);

                // Insert the application into the database and get the new ApplicationId
                Application DBVersion = application;
                DBVersion.ApplicationDate = DateTime.Now;
                DBVersion.ApplicationStatus = ApplicationStatus.New;
                DBVersion.LastStatusDate = DateTime.Now;

                int newApplicationId = ApplicationData.AddApplication(DBVersion);
                LocalDrivingLicenseApplicationData.Add(new LocalDrivingLicenseApplication(newApplicationId, licenseClass));
                return newApplicationId;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating replacement for lost driving license application", ex);
                throw;
            }
        }

        public static int ReplacementForDamagedDrivingLicenseApplication(Application application, LicenseClass licenseClass)
        {
            if (application == null)
                throw new ValidationException("Application cannot be empty.");

            if (application.ApplicationTypeID != ApplicationType.ReplacementForDamagedDrivingLicense)
                throw new ValidationException("Application type must be Replacement for Damaged Driving License.");

            try
            {
                // Validate the application entity
                ApplicationValidator.ReplacementForDamagedDrivingLicenseValidator(application, licenseClass);

                // Insert the application into the database and get the new ApplicationId
                Application DBVersion = application;
                DBVersion.ApplicationDate = DateTime.Now;
                DBVersion.ApplicationStatus = ApplicationStatus.New;
                DBVersion.LastStatusDate = DateTime.Now;

                int newApplicationId = ApplicationData.AddApplication(DBVersion);
                LocalDrivingLicenseApplicationData.Add(new LocalDrivingLicenseApplication(newApplicationId, licenseClass));
                return newApplicationId;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating replacement for damaged driving license application", ex);
                throw;
            }
        }

        public static int ReleaseDetainedDrivingLicenseApplication(Application application, LicenseClass licenseClass)
        {
            if (application == null)
                throw new ValidationException("Application cannot be empty.");

            if (application.ApplicationTypeID != ApplicationType.ReleaseDetainedDrivingLicense)
                throw new ValidationException("Application type must be Release of Detained Driving License.");

            try
            {
                // Validate the application entity
                ApplicationValidator.ReleaseDetainedDrivingLicenseValidator(application, licenseClass);

                // Insert the application into the database and get the new ApplicationId
                Application DBVersion = application;
                DBVersion.ApplicationDate = DateTime.Now;
                DBVersion.ApplicationStatus = ApplicationStatus.New;
                DBVersion.LastStatusDate = DateTime.Now;

                int newApplicationId = ApplicationData.AddApplication(DBVersion);
                LocalDrivingLicenseApplicationData.Add(new LocalDrivingLicenseApplication(newApplicationId, licenseClass));
                return newApplicationId;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating change of personal information application", ex);
                throw;
            }
        }

        public static int NewInternationalLicenseApplication(Application application)
        {
            if (application == null)
                throw new ValidationException("Application cannot be empty.");

            if (application.ApplicationTypeID != ApplicationType.NewInternationalLicense)
                throw new ValidationException("Application type must be New International Driving License.");

            try
            {
                // Validate the application entity
                ApplicationValidator.NewInternationalLicenseValidator(application);

                // Insert the application into the database and get the new ApplicationId
                Application DBVersion = application;
                DBVersion.ApplicationDate = DateTime.Now;
                DBVersion.ApplicationStatus = ApplicationStatus.New;
                DBVersion.LastStatusDate = DateTime.Now;

                return ApplicationData.AddApplication(DBVersion);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating new international driving license application", ex);
                throw;
            }
        }

        public static DataTable GetAllApplications()
        {
            try
            {
                return ApplicationData.GetAllApplications();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while retrieving all applications", ex);
                throw;
            }
        }

        public static Application GetApplicationById(int applicationId)
        {
            if (applicationId <= 0)
                return null;

            try
            {
                return ApplicationData.GetApplication(applicationId);
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

        public static Application Update(Application application)
        {
            try
            {
                // Validate the application entity
                ApplicationValidator.UpdateValidator(application);

                // Update the application in the database
                if (ApplicationData.Update(application))
                    return ApplicationData.GetApplication(application.ApplicationID);
                else
                    return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while updating application", ex);
                throw;
            }
        }


    }
}
