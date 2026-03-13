using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class TestAppointmentValidator
    {
        private static readonly TestAppointmentValidationContext context = new TestAppointmentValidationContext();

        private class TestAppointmentValidationContext
        {
            public TestAppointment TestAppointment;
            public LocalDrivingLicenseApplication LocalApplication;
            public Application Application;
        }

        private static void LoadContext(TestAppointment testAppointment)
        {
            context.TestAppointment = testAppointment;

            context.LocalApplication = LocalDrivingLicenseApplicationRepository.GetById(
            testAppointment.LocalDrivingLicenseApplicationId);

            if (context.LocalApplication != null)
                context.Application = ApplicationRepository.GetById(context.LocalApplication.ApplicationID);

        }

        private static void ValidateForeignKeysReferences()
        {
            // Check if the local driving license application exists
            if (context.LocalApplication == null)
                throw new BusinessException("The specified local driving license application does not exist.");

            // Check if the retake test application exists
            if (context.TestAppointment.RetakeTestApplicationID != null
                && !ApplicationRepository.Exists(context.TestAppointment.RetakeTestApplicationID ?? 0))
                throw new BusinessException("The specified retake test application does not exist.");
        }

        private static void ValidateForeignKeysRequiredFields()
        {
            if (context.Application.ApplicationTypeID == ApplicationType.ReplacementForLostDrivingLicense ||
                context.Application.ApplicationTypeID == ApplicationType.ReplacementForLostDrivingLicense ||
                context.Application.ApplicationTypeID == ApplicationType.ReleaseDetainedDrivingLicense)
            {
                throw new BusinessException("This application type does not need a Test to be completed.");
            }

            if (context.Application.ApplicationTypeID == ApplicationType.RenewDrivingLicenseService
                && context.TestAppointment.TestTypeId != TestType.VisionTest)
            {
                throw new BusinessException("This test type is not required for this application type.");
            }
        }

        private static void ValidateForeignKeys()
        {
            ValidateForeignKeysReferences();
            ValidateForeignKeysRequiredFields();
        }

        private static void TestValidation()
        {
            // Check if there is already a passed test for the local application and test type
            if (TestRepository.IsPassedByLocalAppId(context.TestAppointment.LocalDrivingLicenseApplicationId,
                context.TestAppointment.TestTypeId))
                throw new BusinessException("A passed test already exists for the given test type and local driving license application.");


            // If the test type is a written theory test, check if the vision test has been passed.
            if (context.TestAppointment.TestTypeId == TestType.WrittenTheoryTest
            && !TestRepository.IsPassedByLocalAppId
            (context.TestAppointment.LocalDrivingLicenseApplicationId, TestType.VisionTest))
            {
                throw new BusinessException("The vision test must be passed before scheduling a written theory test.");
            }
            // If the test type is a practical street test, check if the written theory test has been passed.
            else if (context.TestAppointment.TestTypeId == TestType.PracticalStreetTest
                && !TestRepository.IsPassedByLocalAppId(context.TestAppointment.LocalDrivingLicenseApplicationId,
                TestType.WrittenTheoryTest))
            {
                throw new BusinessException("A written theory test appointment must be scheduled before scheduling a practical street test appointment.");
            }

            // Check if the given retake test application already has a test appointment
            if (context.TestAppointment.RetakeTestApplicationID != null
                && TestAppointmentRepository.ExistsByRetakeTestAppId(
                    context.TestAppointment.RetakeTestApplicationID ?? 0))
                throw new BusinessException("A test appointment already exists for the given retake test application.");

            if (context.TestAppointment.RetakeTestApplicationID == null
                && context.Application.ApplicationTypeID == ApplicationType.RetakeTest)
            {
                throw new BusinessException("This application type requires a retake test application.");
            }
        }

        public static void ValidateForAdd(TestAppointment testAppointment)
        {
            Core.Validators.TestAppointmentValidator.Validate(testAppointment);
            LoadContext(testAppointment);
            ValidateForeignKeys();
            TestValidation();
        }
    }
}
