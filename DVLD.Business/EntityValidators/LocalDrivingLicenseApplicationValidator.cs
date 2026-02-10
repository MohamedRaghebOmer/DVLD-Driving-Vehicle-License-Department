using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Business.EntityValidators
{
    internal class LocalDrivingLicenseApplicationValidator
    {
        public static void AddNewValidator(LocalDrivingLicenseApplication localDrivingLicenseApplication)
        {
            Core.Validators.LocalDrivingLicenseApplicationValidator.Validate(localDrivingLicenseApplication);
            
            if (!ApplicationData.Exists(localDrivingLicenseApplication.ApplicationId))
                throw new BusinessException("Associated application does not exist.");
            
            Application application = ApplicationData.GetApplication(localDrivingLicenseApplication.ApplicationId);
            if (application.ApplicationTypeId != ApplicationType.NewLocalDrivingLicenseService)
                throw new BusinessException("Associated application must be of type NewLocalDrivingLicenseService.");

            if (LocalDrivingLicenseApplicationData.Exists(localDrivingLicenseApplication.ApplicationId, -1))
                throw new BusinessException("The Application is already in use.");

            int driverId = DriverData.GetDriverIdByPersonId(application.ApplicantPersonId);
            if (driverId != -1) // Means: if it's an existing driver
            {
                if (LicenseData.DoesDriverHasLicense(driverId, localDrivingLicenseApplication.LicenseClassId))
                    throw new BusinessException("The driver already has this license class.");
            }
        }

        public static void UpdateValidator(LocalDrivingLicenseApplication localDrivingLicenseApplication)
        {
            Core.Validators.LocalDrivingLicenseApplicationValidator.Validate(localDrivingLicenseApplication);
            
            LocalDrivingLicenseApplication storedInfo = LocalDrivingLicenseApplicationData.GetById(localDrivingLicenseApplication.LocalDrivingLicenseApplicationId);
            Application application = ApplicationData.GetApplication(localDrivingLicenseApplication.ApplicationId);
            
            if (application.ApplicationStatus == ApplicationStatus.Completed)
                throw new BusinessException("Cannot update a Local Driving License Application associated with a completed application.");
            
            if (application.ApplicationStatus == ApplicationStatus.Cancelled)
                throw new BusinessException("Cannot update a Local Driving License Application associated with a cancelled application.");

            if (!ApplicationData.Exists(localDrivingLicenseApplication.ApplicationId))
                throw new BusinessException("Associated application does not exist.");

            if (application.ApplicationTypeId != ApplicationType.NewLocalDrivingLicenseService)
                throw new BusinessException("Associated application must be of type NewLocalDrivingLicenseService.");

            if (LocalDrivingLicenseApplicationData.Exists(localDrivingLicenseApplication.ApplicationId, localDrivingLicenseApplication.LocalDrivingLicenseApplicationId))
                throw new BusinessException("LocalDrivingLicenseApplication is already in use.");

            int driverId = DriverData.GetDriverIdByPersonId(application.ApplicantPersonId);
            if (driverId != -1) // Means: if it's an existing driver
            {
                if (LicenseData.DoesDriverHasLicense(driverId, localDrivingLicenseApplication.LicenseClassId))
                    throw new BusinessException("The driver already has this license class.");
            }
        }
    }
}
