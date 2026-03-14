using DVLD.Business.EntityValidators;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class InternationalLicenseService
    {
        public static int IssueByLocalClass3License(int localClass3LicenseId)
        {
            InternationalLicenseValidator.ValidateForAdd(localClass3LicenseId);

            try
            {
                return InternationalLicenseRepository.Add(localClass3LicenseId);
            }
            catch
            {
                AppLogger.LogError("BLL: Error while trying to add new International License.");
                throw;
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return InternationalLicenseRepository.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all International Licenses.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static InternationalLicense GetByLocalLicenseId(int localLicenseId)
        {
            if (localLicenseId <= 0)
                return null;

            try
            {
                return InternationalLicenseRepository.GetByLocalLicenseId(localLicenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get International License with local license Id {localLicenseId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetLicenseHistoryByPersonId(int personId)
        {
            if (personId <= 0)
                return null;

            try
            {
                return InternationalLicenseRepository.GetLicenseHistoryByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get International License history for person with Id {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static InternationalLicense GetById(int internationalLicenseId)
        {
            try
            {
                return InternationalLicenseRepository.GetById(internationalLicenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get International License with Id {internationalLicenseId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetLicenseHistoryByDriverId(int driverId)
        {
            if (driverId <= 0)
                return null;

            try
            {
                return InternationalLicenseRepository.GetLicenseHistory(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get International License history for driver with Id {driverId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetLicenseHistoryByNationalNo(string nationalNo)
        {
            if (string.IsNullOrEmpty(nationalNo))
                return null;

            try
            {
                return InternationalLicenseRepository.GetLicenseHistory(nationalNo);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get International License history for driver with Id {nationalNo}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool ExistsByLocalLicenseId(int localLicenseId, bool checkIsActive = false)
        {
            try
            {
                return InternationalLicenseRepository.ExistsByLocalLicenseId(localLicenseId, checkIsActive);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking existence of International License by local license Id {localLicenseId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
