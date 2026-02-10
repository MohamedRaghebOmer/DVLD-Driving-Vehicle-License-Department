using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;


namespace DVLD.Business.EntityValidators
{
    internal class ApplicationValidator
    {
        public static void AddNewValidator(Application application)
        {
            Core.Validators.ApplicationValidator.Validate(application);

            if (!PersonData.Exists(application.ApplicantPersonId))
                throw new BusinessException("Applicant person does not exist.");

            if (!UserData.Exists(application.CreatedByUserId))
                throw new BusinessException("User creating does not exist.");

            bool isDriver = DriverData.IsPersonUsed(application.ApplicantPersonId, -1);

            int driverId = isDriver
                ? DriverData.GetByPersonId(application.ApplicantPersonId).DriverId
                : -1;

            switch (application.ApplicationTypeId)
            {
                case ApplicationType.NewLocalDrivingLicenseService:
                    if (!isDriver)
                        return;

                    if (LicenseData.DoesDriverHasAllLicenseClasses(driverId))
                        throw new BusinessException("Applicant already has all license classes.");
                    break;

                case ApplicationType.RenewDrivingLicenseService:
                    if (!isDriver)
                        throw new BusinessException("Applicant person is not a local driver.");

                    if (!LicenseData.DoesPersonHasExpiredLicense(application.ApplicantPersonId))
                        throw new BusinessException("No expired license to renew.");
                    break;

                case ApplicationType.ReleaseDetainedDrivingLicense:
                    if (!isDriver)
                        throw new BusinessException("Applicant person is not a local driver.");

                    if (!DetainedLicenseData.DoesPersonHasDetainedLicense(application.ApplicantPersonId))
                        throw new BusinessException("No detained license to release.");
                    break;

                case ApplicationType.NewInternationalLicense:
                    if (!isDriver)
                        throw new BusinessException("Applicant person is not a local driver.");

                    if (!LicenseData.DoesDriverHasLicense(driverId, LicenseClass.Class3_OrdinaryDrivingLicense, true))
                        throw new BusinessException("Active class 3 license required.");
                    break;

                case ApplicationType.ReplacementForLostDrivingLicense:
                case ApplicationType.ReplacementForDamagedDrivingLicense:
                    if (!isDriver)
                        throw new BusinessException("Applicant person is not a local driver.");
                    break;
            }
        }

        public static void UpdateValidator(Application application)
        {
            Core.Validators.ApplicationValidator.Validate(application);
            Application storedInfo = ApplicationData.GetApplication(application.ApplicationId);

            if (application.ApplicantPersonId != storedInfo.ApplicantPersonId)
                throw new BusinessException("Applicant person cannot be changed.");

            if (application.ApplicationDate != storedInfo.ApplicationDate)
                throw new BusinessException("Application date is automatically set, you cannot change it.");

            if (application.ApplicationTypeId != storedInfo.ApplicationTypeId)
                throw new BusinessException("Application type cannot be changed.");

            if (application.LastStatusDate != storedInfo.LastStatusDate)
                throw new BusinessException("Last status date is automatically set, you cannot change it.");

            if (application.CreatedByUserId != storedInfo.CreatedByUserId)
                throw new BusinessException("User creating cannot be changed.");

            if (storedInfo.ApplicationStatus == ApplicationStatus.Completed)
            {
                if (application.ApplicationStatus != ApplicationStatus.Completed)
                    throw new BusinessException("Completed application status cannot be changed.");
                
                if (application.LastStatusDate != storedInfo.LastStatusDate)
                    throw new BusinessException("Last status date cannot be changed.");
            }

            if (storedInfo.ApplicationStatus == ApplicationStatus.Cancelled)
            {
                if (application.ApplicationStatus != ApplicationStatus.Cancelled)
                    throw new BusinessException("Cancelled application status cannot be changed.");
                
                if (application.LastStatusDate != storedInfo.LastStatusDate)
                    throw new BusinessException("Last status date cannot be changed, because the application is cancelled.");
            }

            if (application.PaidFees < 0)
                throw new BusinessException("Paid fees cannot be negative.");

            if (application.PaidFees < storedInfo.PaidFees)
                throw new BusinessException("Paid fees cannot be decreased.");
        }
    }
}
