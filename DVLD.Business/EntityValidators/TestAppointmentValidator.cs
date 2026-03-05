using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class TestAppointmentValidator
    {
        public static void AddNewValidator(TestAppointment testAppointment)
        {
            Core.Validators.TestAppointmentValidator.Validate(testAppointment);

            LocalDrivingLicenseApplication localDrivingLicenseApplication =
                LocalDrivingLicenseApplicationRepository.GetById(
                    testAppointment.LocalDrivingLicenseApplicationId);

            Application application =
                ApplicationRepository.GetById(
                    localDrivingLicenseApplication.ApplicationID);

            // Check if the local driving license application exists
            if (localDrivingLicenseApplication == null)
                throw new BusinessException("The specified local driving license application does not exist.");

            if (application.ApplicationTypeID ==
                ApplicationType.ReplacementForLostDrivingLicense ||
                application.ApplicationTypeID ==
                ApplicationType.ReplacementForLostDrivingLicense ||
                application.ApplicationTypeID ==
                ApplicationType.ReleaseDetainedDrivingLicense)
            {
                throw new BusinessException("This application type does not need a Test to be completed.");
            }

            if (application.ApplicationTypeID == ApplicationType.RenewDrivingLicenseService && testAppointment.TestTypeId != TestType.VisionTest)
                throw new BusinessException("This test type is not required for this application type.");

            // Check if a test appointment already exists for the given test type and local driving license application
            if (TestAppointmentRepository.ExistsForApplication(testAppointment.TestTypeId, testAppointment.LocalDrivingLicenseApplicationId))
                throw new BusinessException("A test appointment already exists for the given test type and local driving license application.");

            switch (testAppointment.TestTypeId)
            {
                case TestType.WrittenTheoryTest:
                    // Check if a vision test appointment exists for the same local driving license application before scheduling a written theory test appointment
                    if (!TestAppointmentRepository.ExistsForApplication(TestType.VisionTest, testAppointment.LocalDrivingLicenseApplicationId))
                        throw new BusinessException("A vision test appointment must be scheduled before scheduling a written theory test appointment.");

                    if (testAppointment.PaidFees != 20)
                        throw new BusinessException("Paid fees must be 20 dollars for a written theory test.");
                    break;

                case TestType.VisionTest:
                    if (testAppointment.PaidFees != 10)
                        throw new BusinessException("Paid fees must be 10 dollars for a practical street test.");
                    break;

                case TestType.PracticalStreetTest:
                    // Check if a written theory test appointment exists for the same local driving license application before scheduling a practical street test appointment
                    if (!TestAppointmentRepository.ExistsForApplication(TestType.WrittenTheoryTest, testAppointment.LocalDrivingLicenseApplicationId))
                        throw new BusinessException("A written theory test appointment must be scheduled before scheduling a practical street test appointment.");

                    decimal licenseClassFees = LicenseClassRepository.GetFees(localDrivingLicenseApplication.LicenseClassID);
                    if (testAppointment.PaidFees != licenseClassFees)
                        throw new BusinessException($"Paid fees must be {licenseClassFees} dollars for a vision test.");
                    break;
            }
        }
    }
}
