using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Business.EntityValidators
{
    internal class DetainedLicenseValidator
    {
        public static void DetainNewLicenseValidator(DetainedLicense detainedLicense)
        {
            Core.Validators.DetainedLicenseValidator.Validate(detainedLicense);
            
            if (!LicenseData.Exists(detainedLicense.LicenseID))
                throw new BusinessException("Associated license does not exist.");

            if (DetainedLicenseData.Exists(detainedLicense.LicenseID))
                throw new BusinessException("License is already detained.");

            if (!UserData.Exists(detainedLicense.CreatedByUserID))
                throw new BusinessException("User creating does not exist.");
        }

        public static void ReleaseDetainedLicenseValidator(Application application, int licenseID)
        {
            if (application == null || application.ApplicationID <= 0)
                throw new BusinessException("Invalid application.");

            Application storedApplication = ApplicationData.GetApplication(application.ApplicationID);
            if (storedApplication == null || storedApplication != application || 
                storedApplication.ApplicationTypeID != ApplicationType.ReleaseDetainedDrivingLicense || 
                storedApplication.ApplicationStatus != ApplicationStatus.New)
            throw new BusinessException("Invalid application.");

            decimal applicationTypeFees = ApplicationTypeData.GetFees(ApplicationType.ReleaseDetainedDrivingLicense);
            if (storedApplication.PaidFees < applicationTypeFees)
                throw new BusinessException($"Paid application fees are less than required. Required fees = {applicationTypeFees}.");

            if (!LicenseData.Exists(licenseID))
                throw new BusinessException("License does not exist.");

            if (!DetainedLicenseData.Exists(licenseID))
                throw new BusinessException("License is not detained.");

            if (LicenseData.IsExpired(licenseID))
                throw new BusinessException("Cannot release an expired license.");
        }
    }
}
