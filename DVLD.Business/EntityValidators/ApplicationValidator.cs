using System;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Business.EntityValidators
{
    internal class ApplicationValidator
    {
        public static void NewLocalDrivingLicenseValidator(Application application, LicenseClass licenseClass)
        {
            Core.Validators.ApplicationValidator.Validate(application);

            if (!PersonData.Exists(application.ApplicantPersonId))
                throw new BusinessException("Applicant person does not exist.");

            if (!UserData.Exists(application.CreatedByUserId))
                throw new BusinessException("User creating does not exist.");

            /* 
                * 1. Check if the driver already has the required license class.
                * 2. Minimum age check.
                * 3. Check if the applicant doesnot have uncompleted application from the same type.
            */

            int driverId = DriverData.GetDriverIdByPersonId(application.ApplicantPersonId);

            // 1. Check if the driver already has the required license class.
            if (LicenseData.DoesDriverHasLicense(driverId, licenseClass))
                throw new BusinessException("Driver already holds the required license class.");


            // Minimum age check.
            DateTime dateOfBirth = PersonData.Get(application.ApplicationId).DateOfBirth;
            int personAge = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now < dateOfBirth.AddYears(personAge))
            {
                personAge--;
            }

            if (LicenseClassData.GetMinimumAllowedAge(licenseClass) > personAge)
                throw new BusinessException("The applicant is under the minimum age required to obtain a driving license from this class.");



            // Check if the applicant doesnot have uncompleted application from the same type.
            if (LocalDrivingLicenseApplicationData.DoesPersonHaveApplication(application.ApplicantPersonId, licenseClass, ApplicationType.NewLocalDrivingLicenseService, ApplicationStatus.New))
                throw new BusinessException("There is already an uncompleted application of the same type.");

            if (LocalDrivingLicenseApplicationData.DoesPersonHaveApplication(application.ApplicantPersonId, licenseClass, ApplicationType.NewLocalDrivingLicenseService, ApplicationStatus.Completed))
                throw new BusinessException("There is already a completed application of the same type.");
        }
        
        public static void RenewDrivingLicenseValidator(Application application, LicenseClass licenseClass)
        {
            Core.Validators.ApplicationValidator.Validate(application);

            if (!PersonData.Exists(application.ApplicantPersonId))
                throw new BusinessException("Applicant person does not exist.");

            if (!UserData.Exists(application.CreatedByUserId))
                throw new BusinessException("User creating does not exist.");

            if (!LicenseData.DoesDriverHasLicense(DriverData.GetDriverIdByPersonId(application.ApplicantPersonId), licenseClass, true))
                throw new BusinessException("Applicant does not have the required license class.");

            if (!LicenseData.IsExpired(application.ApplicantPersonId, licenseClass))
                throw new BusinessException("Only expired licenses can be renewed.");

            if (LocalDrivingLicenseApplicationData.DoesPersonHaveApplication(application.ApplicantPersonId, licenseClass, ApplicationType.RenewDrivingLicenseService, ApplicationStatus.New))
                throw new BusinessException("There is already an uncompleted application of the same type.");
        }

        public static void ReplacementForLostDrivingLicenseValidator(Application application, LicenseClass licenseClass)
        {
            Core.Validators.ApplicationValidator.Validate(application);

            if (!PersonData.Exists(application.ApplicantPersonId))
                throw new BusinessException("Applicant person does not exist.");

            if (!UserData.Exists(application.CreatedByUserId))
                throw new BusinessException("User creating does not exist.");

            if (!LicenseData.DoesDriverHasLicense(DriverData.GetDriverIdByPersonId(application.ApplicantPersonId), licenseClass, true))
                throw new BusinessException("License to replace does not exist.");

            if (LicenseData.IsExpired(application.ApplicantPersonId, licenseClass))
                throw new BusinessException("Cannot replace an expired license.");

            if (LocalDrivingLicenseApplicationData.DoesPersonHaveApplication(application.ApplicantPersonId, licenseClass, ApplicationType.ReplacementForLostDrivingLicense, ApplicationStatus.New))
                throw new BusinessException("There is already an uncompleted application of the same type.");
        }

        public static void ReplacementForDamagedDrivingLicenseValidator(Application application, LicenseClass licenseClass)
        {
            Core.Validators.ApplicationValidator.Validate(application);

            if (!PersonData.Exists(application.ApplicantPersonId))
                throw new BusinessException("Applicant person does not exist.");

            if (!UserData.Exists(application.CreatedByUserId))
                throw new BusinessException("User creating does not exist.");

            if (!LicenseData.DoesDriverHasLicense(DriverData.GetDriverIdByPersonId(application.ApplicantPersonId), licenseClass, true))
                throw new BusinessException("License to replace does not exist.");

            if (LicenseData.IsExpired(application.ApplicantPersonId, licenseClass))
                throw new BusinessException("Cannot replace an expired license.");

            if (LocalDrivingLicenseApplicationData.DoesPersonHaveApplication(application.ApplicantPersonId, licenseClass, ApplicationType.ReplacementForDamagedDrivingLicense, ApplicationStatus.New))
                throw new BusinessException("There is already an uncompleted application of the same type.");
        }

        public static void ReleaseDetainedDrivingLicenseValidator(Application application, LicenseClass licenseClass)
        {
            Core.Validators.ApplicationValidator.Validate(application);

            if (!PersonData.Exists(application.ApplicantPersonId))
                throw new BusinessException("Applicant person does not exist.");

            if (!UserData.Exists(application.CreatedByUserId))
                throw new BusinessException("User creating does not exist.");

            if (!LicenseData.DoesDriverHasLicense(DriverData.GetDriverIdByPersonId(application.ApplicantPersonId), licenseClass, true))
                throw new BusinessException("License to release does not exist.");

            if (LicenseData.IsExpired(application.ApplicantPersonId, licenseClass))
                throw new BusinessException("Cannot release an expired license.");

            if (!DetainedLicenseData.Exists(licenseId))
                throw new BusinessException("The license is not detained.");

            if (LocalDrivingLicenseApplicationData.DoesPersonHaveApplication(application.ApplicantPersonId, licenseClass, ApplicationType.ReleaseDetainedDrivingLicense, ApplicationStatus.New))
                throw new BusinessException("There is already an uncompleted application of the same type.");
        }

        public static void NewInternationalLicenseValidator(Application application)
        {
            Core.Validators.ApplicationValidator.Validate(application);

            if (!PersonData.Exists(application.ApplicantPersonId))
                throw new BusinessException("Applicant person does not exist.");

            if (!UserData.Exists(application.CreatedByUserId))
                throw new BusinessException("User creating does not exist.");
            /* 
               * 1. Check if the driver already has an international license.
               * 2. Check if the driver has an active class 3 license.
               * 3. Check if the applicant doesnot have uncompleted application from the same type.
           */

            int driverId = DriverData.GetDriverIdByPersonId(application.ApplicantPersonId);

            // 1. Check if the driver already has an international license.
            if (driverId == -1)
                throw new BusinessException("The applicant does not have a driving license, so cannot apply for an international license.");

            if (InternationalLicenseData.DoesDriverHaveInternationalLicense(driverId))
                throw new BusinessException("The applicant already has an international license.");

            if (!LicenseData.DoesDriverHasLicense(driverId, LicenseClass.Class3_OrdinaryDrivingLicense, true))
                throw new BusinessException("The applicant does not have an active class 3 driving license.");

            if (ApplicationData.DoesPersonHaveApplication(application.ApplicantPersonId, ApplicationType.NewInternationalLicense, ApplicationStatus.New))
                throw new BusinessException("There is already an uncompleted application of the same type.");

            if (ApplicationData.DoesPersonHaveApplication(application.ApplicantPersonId, ApplicationType.NewInternationalLicense, ApplicationStatus.Completed))
                throw new BusinessException("There is already a completed application of the same type.");
        }


        public static void UpdateValidator(Application updatedVersion)
        {
            Core.Validators.ApplicationValidator.Validate(updatedVersion);
            Application storedInfo = ApplicationData.GetApplication(updatedVersion.ApplicationId);

            if (updatedVersion.ApplicantPersonId != storedInfo.ApplicantPersonId)
                throw new BusinessException("Applicant person cannot be changed.");

            if (updatedVersion.ApplicationDate != storedInfo.ApplicationDate)
                throw new BusinessException("Application date is automatically set, you cannot change it.");

            if (updatedVersion.ApplicationTypeId != storedInfo.ApplicationTypeId)
                throw new BusinessException("Application type cannot be changed.");

            if (updatedVersion.LastStatusDate != storedInfo.LastStatusDate)
                throw new BusinessException("Last status date is automatically set, you cannot change it.");

            if (updatedVersion.CreatedByUserId != storedInfo.CreatedByUserId)
                throw new BusinessException("User creating cannot be changed.");

            if (updatedVersion.PaidFees < 0)
                throw new BusinessException("Paid fees cannot be negative.");

            if (updatedVersion.PaidFees < storedInfo.PaidFees)
                throw new BusinessException("Paid fees cannot be decreased.");

            if (storedInfo.ApplicationStatus == ApplicationStatus.Completed)
            {
                if (updatedVersion.ApplicationStatus != ApplicationStatus.Completed)
                    throw new BusinessException("Completed application status cannot be changed.");
                
                if (updatedVersion.LastStatusDate != storedInfo.LastStatusDate)
                    throw new BusinessException("Last status date cannot be changed.");
            }

            if (storedInfo.ApplicationStatus == ApplicationStatus.Cancelled)
            {
                if (updatedVersion.ApplicationStatus != ApplicationStatus.Cancelled)
                    throw new BusinessException("Cancelled application status cannot be changed.");
                
                if (updatedVersion.LastStatusDate != storedInfo.LastStatusDate)
                    throw new BusinessException("Last status date cannot be changed, because the application is cancelled.");
            }

            if (storedInfo.ApplicationStatus != ApplicationStatus.New && updatedVersion.ApplicationStatus != storedInfo.ApplicationStatus)
                throw new BusinessException("Application status can only be changed from New.");

        }
    }
}
