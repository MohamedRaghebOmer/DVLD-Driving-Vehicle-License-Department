using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Business.EntityValidators;
using DVLD.Core.Logging;

namespace DVLD.Business
{
    public static class InternationalLicenseBusiness
    {
        public static InternationalLicense AddNewInternationalLicense(InternationalLicense internationalLicense)
        {
            InternationalLicenseValidator.AddNewValidator(internationalLicense);

            try
            {
                // Check if the driver has active and expired International License
                if (InternationalLicenseData.DoesDriverIdExist(internationalLicense.DriverID, true, false))
                    InternationalLicenseData.DeactiveInternationalLicenseByDriverId(internationalLicense.DriverID);

                int newInternationalLicenseId = InternationalLicenseData.AddNewInternationalLicense(internationalLicense);
                bool isApplicationCompleted = ApplicationData.UpdateApplicationStatus(internationalLicense.ApplicationID, ApplicationStatus.Completed);
                
                if (newInternationalLicenseId != -1 && isApplicationCompleted)
                    return InternationalLicenseData.GetInternationalLicenseById(newInternationalLicenseId);
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
                return InternationalLicenseData.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all International Licenses.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static InternationalLicense GetInternationalLicenseById(int internationalLicenseId)
        {
            try
            {
                return InternationalLicenseData.GetInternationalLicenseById(internationalLicenseId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get International License with Id {internationalLicenseId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeactiveInternationalLicense(int internationalLicenseId)
        {
            try
            {
                return InternationalLicenseData.DeactiveInternationalLicenseByLicenseId(internationalLicenseId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deactivate International License with Id = {internationalLicenseId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool ActivateInternationalLicense(int internationalLicenseId)
        {
            try
            {
                return InternationalLicenseData.ActivateInternationalLicense(internationalLicenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while activate International License with Id = {internationalLicenseId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
