using System;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Business.EntityValidators
{
    internal class TestAppointmentValidator
    {
        public static void AddNewValidator(TestAppointment testAppointment)
        {
            Core.Validators.TestAppointmentValidator.Validate(testAppointment);

            // Check if the local driving license application exists
            if (!LocalDrivingLicenseApplicationData.Exists(testAppointment.LocalDrivingLicenseApplicationId))
                throw new BusinessException("The specified local driving license application does not exist.");

            // Check if the user who created the test appointment exists
            if (!UserData.Exists(testAppointment.CreatedByUserId))
                throw new BusinessException("The specified user does not exist.");

            // Check if a test appointment already exists for the given test type and local driving license application
            if (TestAppointmentData.DoesApplicationExist(testAppointment.TestTypeId, testAppointment.LocalDrivingLicenseApplicationId))
                throw new BusinessException("A test appointment already exists for the given test type and local driving license application.");

            if (testAppointment.TestTypeId == TestType.WrittenTheoryTest)
            {
                // Check if a vision test appointment exists for the same local driving license application before scheduling a written theory test appointment
                if (!TestAppointmentData.DoesApplicationExist(TestType.VisionTest, testAppointment.LocalDrivingLicenseApplicationId))
                    throw new BusinessException("A vision test appointment must be scheduled before scheduling a written theory test appointment.");
            }

            if (testAppointment.TestTypeId == TestType.PracticalStreetTest)
            {
                // Check if a written theory test appointment exists for the same local driving license application before scheduling a practical street test appointment
                if (!TestAppointmentData.DoesApplicationExist(TestType.WrittenTheoryTest, testAppointment.LocalDrivingLicenseApplicationId))
                    throw new BusinessException("A written theory test appointment must be scheduled before scheduling a practical street test appointment.");
            }
        }

        public static void UpdateValidator(TestAppointment testAppointment)
        {
            Core.Validators.TestAppointmentValidator.Validate(testAppointment);
            TestAppointment existingTestAppointment = TestAppointmentData.GetById(testAppointment.TestAppointmentId);

            if (TestAppointmentData.IsLocked(testAppointment.TestAppointmentId))
                throw new BusinessException("TestAppointment cannot be modified because the test has already been completed.");

            if (existingTestAppointment.TestTypeId != testAppointment.TestTypeId)
                throw new BusinessException("Cannot change Test Type after the test is done.");

            if (existingTestAppointment.LocalDrivingLicenseApplicationId != testAppointment.LocalDrivingLicenseApplicationId)
                throw new BusinessException("Cannot change local driving license application id after the test is done.");

            if (testAppointment.AppointmentDate < DateTime.Now)
                throw new BusinessException("Appointment date cannot be in the past.");

            if (testAppointment.PaidFees < existingTestAppointment.PaidFees)
                throw new ValidationException("Cannot decrease fees after it increased.");

            if (testAppointment.CreatedByUserId != existingTestAppointment.CreatedByUserId)
                throw new BusinessException("Cannot change user who created the appointment.");
        }
    }
}
