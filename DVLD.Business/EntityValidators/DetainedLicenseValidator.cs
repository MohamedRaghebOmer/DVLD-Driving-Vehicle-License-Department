using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Business.EntityValidators
{
    internal class DetainedLicenseValidator
    {
        public static void DetainNewLicenseValidator(int licenseId, decimal fineFees)
        {          
            if (fineFees <= 0)
                throw new BusinessException("Fine fees must be greater than 0.");

            if (licenseId <= 0 || !LicenseData.Exists(licenseId))
                throw new BusinessException("Associated license does not exist.");

            if (DetainedLicenseData.IsDetained(licenseId))
                throw new BusinessException("License is already detained.");
        }
        
        public static void ReleaseDetainedLicenseValidator(int licenseID, Application application)
        {
            if (application == null || application.ApplicationID <= 0)
                throw new BusinessException("Associated application does not exist.");

            if (licenseID <= 0 || !LicenseData.Exists(licenseID))
                throw new BusinessException("Associated license does not exist.");

            if (LicenseData.GetPersonIdByLicenseId(licenseID) != application.ApplicantPersonID)
                throw new BusinessException("Associated license does not belong to the applicant.");

            if (application.ApplicationTypeID != ApplicationType.ReleaseDetainedDrivingLicense ||
                application.ApplicationStatus != ApplicationStatus.New)
            throw new BusinessException("Invalid application.");

            decimal applicationTypeFees = ApplicationTypeData.GetFees(ApplicationType.ReleaseDetainedDrivingLicense);
            if (application.PaidFees < applicationTypeFees)
                throw new BusinessException($"Paid application fees are less than required. Required fees = {applicationTypeFees}.");

            if (!DetainedLicenseData.IsDetained(licenseID))
                throw new BusinessException("License is not detained.");

            if (LicenseData.IsExpired(licenseID))
                throw new BusinessException("Cannot release an expired license.");
        }
    }
}
