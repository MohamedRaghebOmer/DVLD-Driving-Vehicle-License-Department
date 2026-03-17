using DVLD.Business.EntityValidators;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class DetainedLicenseService
    {
        public static int Detain(int licenseId)
        {
            DetainedLicenseValidator.ValidateForDetain(licenseId);

            try
            {
                return DetainedLicenseRepository.Detain(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while detaining license with ID " + licenseId + ".");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
            }
        }

        public static int Release(int licenseId)
        {
            DetainedLicenseValidator.ValidateForRelease(licenseId);

            try
            {
                return DetainedLicenseRepository.Release(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while releasing license with ID " + licenseId + ".");
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

        public static DataTable GetAllWithDetails()
        {
            try
            {
                DataTable dataTable = DetainedLicenseRepository.GetAllWithDetails();

                if (dataTable.Rows.Count > 0)
                    return dataTable;
                else
                    return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting all detained licenses.");
                throw new Exception("We encountered a technical issue, please try again later.", ex);
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
    }
}
