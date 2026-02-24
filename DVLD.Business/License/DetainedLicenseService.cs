using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class DetainedLicenseService
    {                    
        public static DetainedLicense Add(int licenseId, decimal fineFees)
        {
            DetainedLicenseValidator.DetainNewLicenseValidator(licenseId, fineFees);
            
            try
            {
                int detainedLicenseId = DetainedLicenseRepository.Add(licenseId, fineFees);
                return DetainedLicenseRepository.GetByLicenseId(detainedLicenseId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while detaining license with ID " + licenseId + ".");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }

        public static DetainedLicense GetById(int detainedLicenseId)
        {
            if (detainedLicenseId <= 0)
                return null;

            try
            {
                return DetainedLicenseRepository.GetByLicenseId(detainedLicenseId);
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
                return DetainedLicenseRepository.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting all detained licenses.");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }

        public static bool Exists(int detainId)
        {
            if (detainId <= 0)
                return false;

            try
            {
                return DetainedLicenseRepository.Exists(detainId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking existence of detain by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsDetained(int licenseId)
        {
            if (licenseId <= 0)
                return false;

            try
            {
                return DetainedLicenseRepository.IsDetained(licenseId);
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
                return DetainedLicenseRepository.GetLicenseRecords(licenseId, isReleasedOnly);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting all license records for license with ID " + licenseId + ".");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }

        public static bool Release(int licenseId, int applicationId)
        {
            DetainedLicenseValidator.ReleaseDetainedLicenseValidator
                (licenseId, ApplicationRepository.GetById(applicationId));

            try
            {
                return DetainedLicenseRepository.Release(licenseId,  applicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while releasing license with ID " + licenseId + ".");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }
    }
}
