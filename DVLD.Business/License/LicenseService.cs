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
        private static IssueReason DetermineIssueReason(ApplicationType applicationType)
        {
            IssueReason issueReason;

            switch (applicationType)
            {
                case ApplicationType.NewLocalDrivingLicenseService:
                    issueReason = IssueReason.FirstTime;
                    break;
                case ApplicationType.RenewDrivingLicenseService:
                    issueReason = IssueReason.Renew;
                    break;
                case ApplicationType.ReplacementForLostDrivingLicense:
                    issueReason = IssueReason.ReplacementLost;
                    break;
                case ApplicationType.ReplacementForDamagedDrivingLicense:
                    issueReason = IssueReason.ReplacementDamaged;
                    break;
                default:
                    throw new ValidationException("Invalid application type for issuing a license.");
            }

            return issueReason;
        }

        private static int GetDriverId(int personId)
        {
            // If the person is already a driver, return the driver id,
            // else add a new driver and return the new driver id

            int driverId = -1;
            driverId = DriverRepository.GetDriverIdByPersonId(personId);

            if (driverId == -1)
                return driverId;

            Driver driver = new Driver();
            driver.PersonId = personId;

            return DriverRepository.Add(driver);
        }

        private static int GetAndDeactivatePreviousLicense(int driverId, LicenseClass licenseClass)
        {
            int previousLicenseId = LicenseRepository.GetLicenseIdByDriverId(driverId, licenseClass);

            if (previousLicenseId == -1) // If there is no previous license, return -1
                return -1;

            LicenseRepository.DeactivateLicense(previousLicenseId);
            return previousLicenseId;
        }

        private static int AddNewLicense(License license, IssueReason issueReason, int applicantPersonId)
        {
            int driverId = GetDriverId(applicantPersonId); // Must return an existing driver
            int previousLicenseId = GetAndDeactivatePreviousLicense(driverId, license.LicenseClass);
            int newLicenseId = LicenseRepository.Add(license, issueReason, driverId, LicenseClassRepository.GetDefaultValidityLength(license.LicenseClass));

            if (license.LicenseClass == LicenseClass.Class3_OrdinaryDrivingLicense && previousLicenseId != -1 && InternationalLicenseRepository.ExistsForLocalLicenseId(previousLicenseId))
                InternationalLicenseRepository.UpdateLocalLicense(InternationalLicenseRepository.GetIdByLocalLicenseId(previousLicenseId), newLicenseId);

            return newLicenseId;
        }



        public static License Add(License license)
        {
            LicenseValidator.AddNewValidator(license);
            Application application = ApplicationRepository.GetById(license.ApplicationId);

            try
            {
                int newLicenseId = AddNewLicense(license, DetermineIssueReason(application.ApplicationTypeID), application.ApplicantPersonID);
                bool isUpdated = ApplicationRepository.UpdateApplicationStatus(license.ApplicationId, ApplicationStatus.Completed);
                return (newLicenseId != -1 && isUpdated) ? LicenseRepository.GetById(newLicenseId) : null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error adding new license.");
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
            catch(Exception ex)
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
