using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class DetainedLicenseBusiness
    {                     // International licenses cannot be detained
        public static DetainedLicense DetainNewLicense(int licenseId, decimal fineFees)
        {
            DetainedLicenseValidator.DetainNewLicenseValidator(licenseId, fineFees);
            
            try
            {
                int detainedLicenseId = DetainedLicenseData.DetainNewLicense(licenseId, fineFees);
                return DetainedLicenseData.GetDetainedLicenseById(detainedLicenseId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while detaining license with ID " + licenseId + ".");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }

        public static DetainedLicense GetDetainedLicenseById(int detainedLicenseId)
        {
            try
            {
                return DetainedLicenseData.GetDetainedLicenseById(detainedLicenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting detained license with ID " + detainedLicenseId + ".");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return DetainedLicenseData.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting all detained licenses.");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }

        public static bool DoesDetainIdExist(int detainId)
        {
            if (detainId <= 0)
                return false;

            try
            {
                return DetainedLicenseData.DoesDetainIdExist(detainId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking existence of detain by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DoesLicenseExist(int licenseId, bool isReleased = false)
        {
            if (licenseId <= 0)
                return false;

            try
            {
                return DetainedLicenseData.DoesLicenseExist(licenseId, isReleased);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if license with ID " + licenseId + " is detained.");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }

        public static DataTable GetLicenseRecords(int licenseId, bool isReleasedOnly = false)
        {
            if (licenseId <= 0)
                return null;

            try
            {
                return DetainedLicenseData.GetLicenseRecords(licenseId, isReleasedOnly);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting all license records for license with ID " + licenseId + ".");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }

        public static bool ReleaseLicense(int licenseId, int applicationId)
        {
            Application application = ApplicationData.GetApplication(applicationId);
            DetainedLicenseValidator.ReleaseDetainedLicenseValidator(licenseId, application);

            try
            {
                                                           // Update 1 below to actual UserId
                return DetainedLicenseData.ReleaseLicense(licenseId, 1, applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while releasing license with ID " + licenseId + ".");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }
    }
}
