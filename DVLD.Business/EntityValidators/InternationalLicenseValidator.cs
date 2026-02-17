using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Business.EntityValidators
{
    internal class InternationalLicenseValidator
    {
        // This method is used to validate data integrity of the entity
        public static void EnsureForeignKeysExist(InternationalLicense license)
        {
            if (!ApplicationData.Exists(license.ApplicationID))
                throw new BusinessException("The application does not exist.");

            if (!DriverData.Exists(license.DriverID))
                throw new BusinessException("The driver does not exist.");

            if (!LicenseData.Exists(license.IssuedUsingLocalLicenseID))
                throw new BusinessException("The local license used does not exist.");
        }

        public static void AddNewValidator(InternationalLicense license)
        {
            Core.Validators.InternationalLicenseValidator.Validate(license);
            EnsureForeignKeysExist(license);
            Application application = ApplicationData.GetById(license.ApplicationID);
            License localLicense = LicenseData.GetById(license.IssuedUsingLocalLicenseID);

            // Check if the driver already has an international license.
            if (InternationalLicenseData.DoesDriverIdExist(localLicense.DriverId, true, true))
                throw new BusinessException("The driver already has an active international license.");

            // Check if the application already associated with another international license.
            if (InternationalLicenseData.DoesApplicationIdExist(license.ApplicationID))
                throw new BusinessException("The application is already associated with existing international license.");

            // Check application type and status.
            if (application.ApplicationTypeID != ApplicationType.NewInternationalLicense || application.ApplicationStatus != ApplicationStatus.New)
                throw new BusinessException("Invalid application.");

            // Check if the application fees are not completely paid.
            decimal applicationTypeFees = ApplicationTypeData.GetFees(ApplicationType.NewInternationalLicense);
            if (application.PaidFees < applicationTypeFees)
                throw new BusinessException($"Paid application fees are less than required. Required fees = {applicationTypeFees}.");

            // Check if the local license belongs to the applicant.
            if (DriverData.GetByDriverId(DriverData.GetDriverIdByPersonId(localLicense.DriverId)).PersonId != application.ApplicantPersonID)
                throw new BusinessException("The local license does not belong to the applicant.");

            // Check if the local license is a class 3 license.
            if (localLicense.LicenseClass != LicenseClass.Class3_OrdinaryDrivingLicense)
                throw new BusinessException("The local license must be a class 3 license.");

            // LocalLicense must be Active and not expired
            if (localLicense.IsActive != true || LicenseData.IsExpired(localLicense.LicenseID))
                throw new BusinessException("The local license must be active and not expired.");

            // LocalLicense must belong to the Driver.
            if (localLicense.DriverId != license.DriverID)
                throw new BusinessException("The local license must belong to the driver.");

            // Check if the local license is not detained.
            if (DetainedLicenseData.IsDetained(localLicense.LicenseID))
                throw new BusinessException("The local license is detained.");
        }
    }
}
