using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Business.EntityValidators;
using DVLD.Core.Logging;

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
                if (InternationalLicenseRepository.ExistsForDirver(internationalLicense.DriverID, true, false))
                    InternationalLicenseRepository.DeactivateByDriverId(internationalLicense.DriverID);

                int newInternationalLicenseId = InternationalLicenseRepository.Add(internationalLicense);
                bool isApplicationCompleted = ApplicationRepository.UpdateApplicationStatus(internationalLicense.ApplicationID, ApplicationStatus.Completed);
                
                if (newInternationalLicenseId != -1 && isApplicationCompleted)
                    return InternationalLicenseRepository.GetById(newInternationalLicenseId);
                return null;
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all International Licenses.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static InternationalLicense GetById(int internationalLicenseId)
        {
            try
            {
                return InternationalLicenseRepository.GetById(internationalLicenseId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get International License with Id {internationalLicenseId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Deactive(int internationalLicenseId)
        {
            try
            {
                return InternationalLicenseRepository.DeactivateById(internationalLicenseId);
            }
            catch(Exception ex)
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
