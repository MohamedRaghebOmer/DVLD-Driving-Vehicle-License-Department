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
            
            if (LocalDrivingLicenseApplicationData.Exists(localDrivingLicenseApplication.ApplicationId, -1))
                throw new BusinessException("The Application is already in use.");
        }

        public static void UpdateValidator(LocalDrivingLicenseApplication localDrivingLicenseApplication)
        {
            Core.Validators.LocalDrivingLicenseApplicationValidator.Validate(localDrivingLicenseApplication);
            
            LocalDrivingLicenseApplication storedInfo = LocalDrivingLicenseApplicationData.GetById(localDrivingLicenseApplication.LocalDrivingLicenseApplicationId);
            Application application = ApplicationData.GetApplication(localDrivingLicenseApplication.ApplicationId);

            if (storedInfo.ApplicationId != localDrivingLicenseApplication.ApplicationId)
                throw new BusinessException("Can't change the Application Id.");

            if (storedInfo.LicenseClassId == localDrivingLicenseApplication.LicenseClassId)
                return;

            if (application.ApplicationStatus != ApplicationStatus.New && storedInfo.LicenseClassId != localDrivingLicenseApplication.LicenseClassId)
                throw new BusinessException("Can't change license class after the application is no longer new.");

            if (TestAppointmentData.DoesApplicationExist(localDrivingLicenseApplication.LocalDrivingLicenseApplicationId))
                throw new BusinessException("Can't change the license class after there is a Test Appointment related to it.");
        }
    }
}
