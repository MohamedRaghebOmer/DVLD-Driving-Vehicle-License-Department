using DVLD.Business.EntityValidators;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class InternationalLicenseService
    {
        public static InternationalLicense Add(InternationalLicense internationalLicense)
        {
            InternationalLicenseValidator.AddNewValidator(internationalLicense);

            try
            {
                // Check if the driver has active and expired International License
                if (InternationalLicenseRepository.ExistsForDriver(internationalLicense.DriverID, true, false))
                    InternationalLicenseRepository.DeactivateByDriverId(internationalLicense.DriverID);

                int newInternationalLicenseId = InternationalLicenseRepository.Add(internationalLicense);
                bool isApplicationCompleted = ApplicationRepository.UpdateAppStatusByAppId(internationalLicense.ApplicationID, ApplicationStatus.Completed);

                if (newInternationalLicenseId != -1 && isApplicationCompleted)
                    return InternationalLicenseRepository.GetById(newInternationalLicenseId);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to add new International License.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
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

        public static DataTable GetLicenseHistoryByPersonId(int personId)
        {
            if (personId <= 0)
                return null;

            try
            {
                return InternationalLicenseRepository.GetLicenseHistory(personId);
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

        public static bool InActivate(int internationalLicenseId)
        {
            try
            {
                return InternationalLicenseRepository.DeactivateById(internationalLicenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deactivate International License with Id = {internationalLicenseId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Activate(int internationalLicenseId)
        {
            try
            {
                return InternationalLicenseRepository.Activate(internationalLicenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while activate International License with Id = {internationalLicenseId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
