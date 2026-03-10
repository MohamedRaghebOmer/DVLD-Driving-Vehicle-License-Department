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
            if (!ApplicationRepository.Exists(license.ApplicationId))
                throw new BusinessException("Application does not exist.");

            // Check if driver exists.
            if (!DriverRepository.Exists(license.DriverId))
                throw new BusinessException("Driver does not exist.");



            // Check if driver already has license with the same type.
            if (LicenseRepository.ExistsByDriver(license.DriverId, license.LicenseClass))
                throw new BusinessException("Driver already has an license with the same type.");

            // Check if the application already associated with another license.
            if (LicenseRepository.ExistsForApplication(license.ApplicationId))
                throw new BusinessException("License for the application already exists.");


            // Check if the application is completed.
            ApplicationStatus applicationStatus = ApplicationRepository.GetById(license.ApplicationId).ApplicationStatus;

            if (applicationStatus == ApplicationStatus.Completed)
                throw new BusinessException("License already exists for the completed application.");

            if (applicationStatus == ApplicationStatus.Canceled)
                throw new BusinessException("Can't add license for a canceled application.");


            // Check if the application type is valid.
            ApplicationType applicationType = ApplicationRepository.GetById(license.ApplicationId).ApplicationTypeID;

            if (applicationType == ApplicationType.ReleaseDetainedDrivingLicense)
                throw new BusinessException("Invalid application type.");

            if (applicationType == ApplicationType.NewInternationalLicense)
                throw new BusinessException("Can't add license for an international application.");


            // Check if the driver has passed all three tests.
            LocalDrivingLicenseApplication localDrivingLicenseApplication = LocalDrivingLicenseApplicationRepository.GetByApplicationId(license.ApplicationId);
            _ = localDrivingLicenseApplication
                ?? throw new BusinessException("Local driving license application does not exist.");

            if (!TestRepository.HasPassedThreeTests(localDrivingLicenseApplication.LocalDrivingLicenseApplicationID))
                throw new BusinessException("Driver has not passed all three tests.");


            // Check if the license class fees are paid.
            decimal licenseClassFees = LicenseClassRepository.GetFees(license.LicenseClass);
            if (license.PaidFees != licenseClassFees)
                throw new BusinessException($"Paid fees must be {licenseClassFees} for this license class.");
        }
    }
}
