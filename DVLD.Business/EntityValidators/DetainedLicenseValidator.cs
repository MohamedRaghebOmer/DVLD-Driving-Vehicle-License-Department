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

            if (licenseId <= 0 || !LicenseRepository.Exists(licenseId))
                throw new BusinessException("Associated license does not exist.");

            if (DetainedLicenseRepository.IsDetained(licenseId))
                throw new BusinessException("License is already detained.");
        }
        
        public static void ReleaseDetainedLicenseValidator(int licenseID, Application application)
        {
            if (application == null || application.ApplicationID <= 0)
                throw new BusinessException("Associated application does not exist.");

            if (licenseID <= 0 || !LicenseRepository.Exists(licenseID))
                throw new BusinessException("Associated license does not exist.");

            if (LicenseRepository.GetPersonIdByLicenseId(licenseID) != application.ApplicantPersonID)
                throw new BusinessException("Associated license does not belong to the applicant.");

            if (application.ApplicationTypeID != ApplicationType.ReleaseDetainedDrivingLicense ||
                application.ApplicationStatus != ApplicationStatus.New)
            throw new BusinessException("Invalid application.");

            decimal applicationTypeFees = ApplicationTypeRepository.GetFees(ApplicationType.ReleaseDetainedDrivingLicense);
            if (application.PaidFees < applicationTypeFees)
                throw new BusinessException($"Paid application fees are less than required. Required fees = {applicationTypeFees}.");

            if (!DetainedLicenseRepository.IsDetained(licenseID))
                throw new BusinessException("License is not detained.");

            if (LicenseRepository.IsExpired(licenseID))
                throw new BusinessException("Cannot release an expired license.");
        }
    }
}
