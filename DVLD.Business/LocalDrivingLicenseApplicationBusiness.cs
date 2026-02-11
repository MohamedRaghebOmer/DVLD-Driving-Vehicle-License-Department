using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class LocalDrivingLicenseApplicationBusiness
    {
        public static int Add(LocalDrivingLicenseApplication localDrivingLicenseApplication)
        {
            try
            {
                LocalDrivingLicenseApplicationValidator.AddNewValidator(localDrivingLicenseApplication);
                return LocalDrivingLicenseApplicationData.Add(localDrivingLicenseApplication);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Business: Error while adding a new local driving license application.", ex);
                throw;
            }
        }

        public static LocalDrivingLicenseApplication GetById(int localDrivingLicenseApplicationId)
        {
            try
            {
                return LocalDrivingLicenseApplicationData.GetById(localDrivingLicenseApplicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Business: Error while retrieving local driving license application with ID {localDrivingLicenseApplicationId}.", ex);
                throw;
            }
        }

        public static LocalDrivingLicenseApplication GetByApplicationId(int applicationId)
        {
            try
            {
                return LocalDrivingLicenseApplicationData.GetByApplicationId(applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Business: Error while retrieving local driving license application with Application ID {applicationId}.", ex);
                throw;
            }
        }

        public static bool Exists(int localDrivingLicenseApplicationId, int excludedId)
        {
            try
            {
                return LocalDrivingLicenseApplicationData.Exists(localDrivingLicenseApplicationId, excludedId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Business: Error while checking existence of local driving license application with ID {localDrivingLicenseApplicationId}.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return LocalDrivingLicenseApplicationData.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Business: Error while retrieving all local driving license applications.", ex);
                throw;
            }
        }

        public static bool DoesPersonHaveApplication(int personId, LicenseClass licenseClass, ApplicationType applicationType, ApplicationStatus status)
        {
            try
            {
                return LocalDrivingLicenseApplicationData.DoesPersonHaveApplication(personId, licenseClass, applicationType, status);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Business: Error while checking if person with ID {personId} has an application for license class {licenseClass}.", ex);
                throw;
            }
        }

        public static bool Update(LocalDrivingLicenseApplication localDrivingLicenseApplication)
        {
            LocalDrivingLicenseApplicationValidator.UpdateValidator(localDrivingLicenseApplication);
            
            try
            {
                return LocalDrivingLicenseApplicationData.Update(localDrivingLicenseApplication);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Business: Error while updating local driving license application with ID {localDrivingLicenseApplication.LocalDrivingLicenseApplicationId}.", ex);
                throw;
            }
        }
    }
}
