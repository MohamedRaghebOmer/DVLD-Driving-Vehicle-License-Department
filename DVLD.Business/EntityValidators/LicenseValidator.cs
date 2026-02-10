using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Data;
using System;

namespace DVLD.Business.EntityValidators
{
    internal class LicenseValidator
    {
        public static void AddNewValidator(License license)
        {
            Core.Validators.LicenseValidator.Validate(license);
            
            ApplicationStatus applicationStatus = ApplicationData.GetApplicationStatus(license.ApplicationId);
            ApplicationType applicationType = ApplicationData.GetApplicationType(license.ApplicationId);

            if (!ApplicationData.Exists(license.ApplicationId))
                throw new BusinessException("Application does not exist.");

            if (applicationStatus == ApplicationStatus.Cancelled)
                throw new BusinessException("Can't add license for a canceled application.");

            if (applicationStatus == ApplicationStatus.Completed)
                throw new BusinessException("Can't add license for a completed application.");

            if (applicationType == ApplicationType.ReleaseDetainedDrivingLicense)
                throw new BusinessException("Invalid application type.");

            if (applicationType == ApplicationType.NewInternationalLicense)
                throw new BusinessException("Can't add license for an international application.");

            if (!DriverData.Exists(license.DriverId))
                throw new BusinessException("Driver does not exist.");

            if (LicenseData.DoesDriverHasActiveLicense(license.DriverId, license.LicenseClass, false))
                throw new BusinessException("Driver already has an license with the same type.");
        }

        public static void UpdateValidator(License license)
        {
            License storedInfo = LicenseData.GetLicenseById(license.LicenseId);

            Core.Validators.LicenseValidator.Validate(license);
            
            if (!LicenseData.Exists(license.LicenseId))
                throw new BusinessException("License does not exist.");

            if (storedInfo.ApplicationId != license.ApplicationId)
                throw new BusinessException("Can't change the application associated with the license.");

            if (storedInfo.DriverId != license.DriverId)
                throw new BusinessException("Can't change the driver associated with the license.");

            if (storedInfo.LicenseClass != license.LicenseClass)
                throw new BusinessException("Can't change the license class of an existing license.");

            if (storedInfo.IssueDate != license.IssueDate)
                throw new BusinessException("Can't change license issue date.");

            if (storedInfo.ExpirationDate != license.ExpirationDate)
                throw new BusinessException("Can't change license expiration date.");

            if (storedInfo.IssueReason != license.IssueReason)
                throw new BusinessException("Can't change license issue reason.");

            if (storedInfo.CreatedByUserId != license.CreatedByUserId)
                throw new BusinessException("Can't change user created the license.");
        }
    }
}
