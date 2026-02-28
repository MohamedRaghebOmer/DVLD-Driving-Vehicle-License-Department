using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class LocalDrivingLicenseApplicationService
    {
        // There is no Add method because the LocalDrivingLicenseApplication is created automatically
        // when the Application is created.

        public static LocalDrivingLicenseApplication GetById(int localDrivingLicenseApplicationId)
        {
            if (localDrivingLicenseApplicationId <= 0)
                return null;

            try
            {
                return LocalDrivingLicenseApplicationRepository.GetById(localDrivingLicenseApplicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Business: Error while retrieving local driving license application with ID {localDrivingLicenseApplicationId}.", ex);
                throw;
            }
        }

        public static LocalDrivingLicenseApplication GetByApplicationId(int applicationId)
        {
            if (applicationId <= 0)
                return null;

            try
            {
                return LocalDrivingLicenseApplicationRepository.GetByApplicationId(applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Business: Error while retrieving local driving license application with Application ID {applicationId}.", ex);
                throw;
            }
        }

        public static bool Exists(int localDrivingLicenseApplicationId, int excludedId = -1)
        {
            if (localDrivingLicenseApplicationId <= 0)
                return false;

            try
            {
                return LocalDrivingLicenseApplicationRepository.ExistsForApplication(localDrivingLicenseApplicationId, excludedId);
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
                return LocalDrivingLicenseApplicationRepository.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Business: Error while retrieving all local driving license applications.", ex);
                throw;
            }
        }
    }
}
