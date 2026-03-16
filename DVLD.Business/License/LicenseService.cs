using DVLD.Business.EntityValidators;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class LicenseService
    {
        public static int Issue(int localDrivingLicenseApplicationId, string notes)
        {
            // Validate and return a trusted license to issue
            License license = LicenseValidator.AddNewValidator(localDrivingLicenseApplicationId);
            license.Notes = notes;

            try
            {
                int newLicenseId = LicenseRepository.Add(license);
                bool isUpdated = ApplicationRepository.UpdateAppStatusByLocalAppId(localDrivingLicenseApplicationId, ApplicationStatus.Completed);

                if (newLicenseId != -1 && isUpdated)
                    return newLicenseId;

                return -1;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error adding issuing a license for first time.");
                throw new Exception("An error occurred while adding the license. Please try again later.", ex);
            }
        }

        public static int Renew(int licenseId)
        {
            LicenseValidator.ValidateForRenew(licenseId);

            try
            {
                return LicenseRepository.Renew(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error adding renew a license.");
                throw new Exception("An error occurred while adding the license. Please try again later.", ex);
            }
        }

        public static int Replace(int licenseId, bool isLost)
        {
            LicenseValidator.ValidateForReplace(licenseId);

            try
            {
                return LicenseRepository.Replace(licenseId, isLost);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while replacing license with id " + licenseId + ".");
                throw new Exception("Error replacing the license.", ex);
            }
        }

        public static int Detain(int licenseId)
        {
            LicenseValidator.ValidateForDetain(licenseId);

            try
            {
                return LicenseRepository.Detain(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while detaining license with id " + licenseId + ".");
                throw new Exception("Error detaining the license.", ex);
            }
        }

        public static int GetOldLicenseIdAfterReplacement(int replaceTypeApplicationId)
        {
            Application application = null;

            if (replaceTypeApplicationId <= 0 ||
                (application = ApplicationRepository.GetById(replaceTypeApplicationId)) == null)
            {
                throw new BusinessException("Replace Application does not exist.");
            }

            if (application.ApplicationTypeID != ApplicationType.ReplacementForLostDrivingLicense
                && application.ApplicationTypeID != ApplicationType.ReplacementForDamagedDrivingLicense)
            {
                throw new BusinessException("Application Type is not replace for license.");
            }

            // Replacement application status is always completed because it done automatically 
            if (application.ApplicationStatus != ApplicationStatus.Completed)
            {
                throw new BusinessException("License with given application id does not exist.");
            }

            try
            {
                return LicenseRepository.GetOldLicenseIdAfterReplacement(replaceTypeApplicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get new license id after replacement.");
                throw new Exception("An error occurred while adding the license. Please try again later.", ex);
            }
        }

        public static int GetNewLicenseIdAfterReplacement(int replaceTypeApplicationId)
        {
            Application application = null;

            if (replaceTypeApplicationId <= 0 ||
                (application = ApplicationRepository.GetById(replaceTypeApplicationId)) == null)
            {
                throw new BusinessException("Replace Application does not exist.");
            }

            if (application.ApplicationTypeID != ApplicationType.ReplacementForDamagedDrivingLicense
                && application.ApplicationTypeID != ApplicationType.ReplacementForLostDrivingLicense)
            {
                throw new BusinessException("Application Type is not replace for license.");
            }

            // Replacement application status is always completed because it done automatically 
            if (application.ApplicationStatus != ApplicationStatus.Completed)
            {
                throw new BusinessException("License with given application id does not exist.");
            }

            try
            {
                return LicenseRepository.GetNewLicenseIdAfterReplacement(replaceTypeApplicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get new license id after replacement.");
                throw new Exception("An error occurred while adding the license. Please try again later.", ex);
            }

        }

        public static DataTable GetAll()
        {
            try
            {
                return LicenseRepository.GetAll();

            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting all licenses.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static License GetById(int licenseId)
        {
            try
            {
                return LicenseRepository.GetById(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static DataTable GetDriverLicenses(int driverId)
        {
            if (driverId <= 0)
                throw new ValidationException("Invalid Driver ID. It must be a positive integer.");

            try
            {
                return LicenseRepository.GetDriverLicenses(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting licenses for DriverID {driverId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static DataTable GetLicenseHistoryByPersonId(int personId)
        {
            if (personId <= 0)
                return null;

            try
            {
                return LicenseRepository.GetLicenseHistoryByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting license history for PersonID {personId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static DataTable GetLicenseHistoryByDriverId(int driverId)
        {
            if (driverId <= 0)
                return null;

            try
            {
                return LicenseRepository.GetLicenseHistoryByDriverId(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting license history for DriverID {driverId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static DataTable GetLicenseHistoryByNationalNo(string nationalNo)
        {
            if (string.IsNullOrEmpty(nationalNo))
                return null;

            try
            {
                return LicenseRepository.GetLicenseHistoryByNationalNo(nationalNo);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting license history for NationalNo {nationalNo}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static License GetByNationalNo(string nationalNo)
        {
            if (string.IsNullOrEmpty(nationalNo))
                return null;

            try
            {
                return LicenseRepository.GetByNationalNo(nationalNo);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting license with NationalNo {nationalNo}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static License GetByApplicationId(int applicationId)
        {
            if (applicationId <= 0)
                return null;

            try
            {
                return LicenseRepository.GetByApplicationId(applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting license by application id = {applicationId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static License GetByLocalApplicationId(int localApplicationId)
        {
            if (localApplicationId <= 0)
                return null;

            try
            {
                return LicenseRepository.GetByLocalApplicationId(localApplicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting license by local application id = {localApplicationId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }

        }

        public static bool DeactivateLicense(int licenseId)
        {
            if (licenseId <= 0)
                throw new ValidationException("Invalid License ID. It must be a positive integer.");
            try
            {
                return LicenseRepository.DeactivateLicense(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deactivating license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool ReactivateLicense(int licenseId)
        {
            if (licenseId <= 0)
                throw new ValidationException("Invalid License ID. It must be a positive integer.");
            try
            {
                return LicenseRepository.ReactivateLicense(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reactivating license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool IsActive(int licenseId)
        {
            if (licenseId <= 0)
                throw new ValidationException("Invalid license id, it must be a positive integer.");

            try
            {
                return LicenseRepository.IsActive(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if license with ID {licenseId} is active.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool UpdateNotes(int licenseId, string newNotes)
        {
            if (licenseId <= 0)
                return false; // Invalid license ID, cannot update notes

            try
            {
                return LicenseRepository.UpdateNotes(licenseId, newNotes);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating notes for license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }
    }
}
