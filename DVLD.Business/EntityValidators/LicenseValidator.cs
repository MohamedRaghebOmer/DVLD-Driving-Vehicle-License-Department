using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal class LicenseValidator
    {
        public static void AddNewValidator(License license)
        {
            Core.Validators.LicenseValidator.Validate(license);
            

            // Check if application exists.
            if (!ApplicationData.Exists(license.ApplicationId))
                throw new BusinessException("Application does not exist.");
            
            // Check if driver exists.
            if (!DriverData.Exists(license.DriverId))
                throw new BusinessException("Driver does not exist.");



            // Check if driver already has license with the same type.
            if (LicenseData.DoesDriverHasLicense(license.DriverId, license.LicenseClass))
                throw new BusinessException("Driver already has an license with the same type.");

            // Check if the application already associated with another license.
            if (LicenseData.DoesApplicationExist(license.ApplicationId))
                throw new BusinessException("License for the application already exists.");



            ApplicationStatus applicationStatus = ApplicationData.GetApplication(license.ApplicationId).ApplicationStatus;

            if (applicationStatus == ApplicationStatus.Completed)
                throw new BusinessException("License already exists for the completed application.");

            if (applicationStatus == ApplicationStatus.Cancelled)
                throw new BusinessException("Can't add license for a canceled application.");



            ApplicationType applicationType = ApplicationData.GetApplication(license.ApplicationId).ApplicationTypeID;

            if (applicationType == ApplicationType.ReleaseDetainedDrivingLicense)
                throw new BusinessException("Invalid application type.");

            if (applicationType == ApplicationType.NewInternationalLicense)
                throw new BusinessException("Can't add license for an international application.");



            LocalDrivingLicenseApplication localDrivingLicenseApplication = LocalDrivingLicenseApplicationData.GetByApplicationId(license.ApplicationId);
            _ = localDrivingLicenseApplication
                ?? throw new BusinessException("Local driving license application does not exist.");
            
            if (!TestData.HasPassedThreeTests(localDrivingLicenseApplication.LocalDrivingLicenseApplicationId))
                throw new BusinessException("Driver has not passed all three tests.");
        }

        public static void UpdateValidator(License license)
        {
            Core.Validators.LicenseValidator.Validate(license);
            License storedInfo = LicenseData.GetLicenseById(license.LicenseId);
            
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
