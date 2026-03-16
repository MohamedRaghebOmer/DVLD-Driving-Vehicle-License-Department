using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Data;
using System;

namespace DVLD.Business.EntityValidators
{
    internal static class LicenseValidator
    {
        public static License AddNewValidator(int localAppId)
        {
            if (localAppId <= 0)
                throw new ValidationException($"Local Driving License Application with Id = {localAppId} does not exist.");

            var localApp = LocalDrivingLicenseApplicationRepository.GetById(localAppId);

            if (localApp == null)
                throw new ValidationException($"Local Driving License Application with Id = {localAppId} does not exist.");


            Application application = ApplicationRepository.GetById(localApp.ApplicationID);
            int driverId = DriverRepository.GetDriverIdByPersonId(application.ApplicantPersonID);
            bool isDriver = (driverId > 0);

            if (isDriver)
            {
                // Check if the person already hold a license with the same class type
                if (LicenseRepository.ExistsByDriver(driverId, localApp.LicenseClassID))
                    throw new BusinessException($"The applicant already hold {LicenseClassService.GetLicenseClassName(localApp.LicenseClassID)} license class.");
            }

            // Check if the application status is not new
            if (application.ApplicationStatus != ApplicationStatus.New)
                throw new BusinessException("The status of the associated application must be new for issuing a new license.");

            // Check if the application type is not valid.
            if (application.ApplicationTypeID != ApplicationType.NewLocalDrivingLicenseService)
                throw new BusinessException("Invalid application type.");

            if (!TestRepository.HasPassedThreeTests(localAppId))
                throw new BusinessException("The Applicant must pass the three tests to issue the license.");

            // If the applicant is not a driver then make him a driver.
            if (!isDriver)
            {
                Driver driver = new Driver();

                driver.PersonId = application.ApplicantPersonID;

                driverId = DriverRepository.Add(driver);
            }

            License license = new License();

            license.ApplicationId = application.ApplicationID;
            license.DriverId = driverId;
            license.LicenseClass = localApp.LicenseClassID;

            return license;
        }

        public static void ValidateForRenew(int licenseId)
        {
            // Check if the license is not exists
            if (!LicenseRepository.Exists(licenseId))
                throw new BusinessException("License does not exist.");

            // Check if the license is not active
            if (!LicenseRepository.IsActive(licenseId))
                throw new BusinessException("Cannot renew license. License is not active.");

            // Check if the license is not expired
            if (!LicenseRepository.IsExpired(licenseId))
                throw new BusinessException("License is not expired.");
        }

        public static void ValidateForReplace(int licenseId)
        {
            License license = null;

            if (licenseId <= 0 || (license = LicenseRepository.GetById(licenseId)) == null)
                throw new BusinessException("License does not exist.");

            if (license.IsActive == false)
                throw new BusinessException("License must be active to renew it.");

            if (license.ExpirationDate < DateTime.Now)
                throw new BusinessException("License is expired.");

            if (DetainedLicenseRepository.IsDetained(licenseId))
                throw new BusinessException("License is detained.");
        }
    }
}
