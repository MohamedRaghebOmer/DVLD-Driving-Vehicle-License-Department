using System;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Business.EntityValidators
{
    internal class TestAppointmentValidator
    {
        public static void AddNewValidator(TestAppointment testAppointment)
        {
            Core.Validators.TestAppointmentValidator.Validate(testAppointment);

            if (!LocalDrivingLicenseApplicationData.Exists(testAppointment.LocalDrivingLicenseApplicationId))
                throw new BusinessException("The specified local driving license application does not exist.");

            if (TestAppointmentData.DoesApplicationExists(testAppointment.LocalDrivingLicenseApplicationId))
                throw new BusinessException("A test appointment already exists for the given local driving license application.");
            
            if (!UserData.Exists(testAppointment.CreatedByUserId))
                throw new BusinessException("The specified user does not exist.");
        }

        public static void UpdateValidator(TestAppointment testAppointment)
        {
            Core.Validators.TestAppointmentValidator.Validate(testAppointment);
            TestAppointment existingTestAppointment = TestAppointmentData.GetById(testAppointment.TestAppointmentId);

            if (TestAppointmentData.IsLocked(testAppointment.TestAppointmentId))
                throw new BusinessException("Can't change TestAppointment info after it locked.");

            if (existingTestAppointment.TestTypeId != testAppointment.TestTypeId)
                throw new BusinessException("Can't change Test Type after the test is done.");

            if (existingTestAppointment.LocalDrivingLicenseApplicationId != testAppointment.LocalDrivingLicenseApplicationId)
                throw new BusinessException("Can't change local diriving license application id after the test is done.");

            if (testAppointment.PaidFees < existingTestAppointment.PaidFees)
                throw new ValidationException("Can't decrease fees after it increased.");

            if (testAppointment.CreatedByUserId != existingTestAppointment.CreatedByUserId)
                throw new BusinessException("Can't change user created the appointment.");

            if (testAppointment.IsLocked == true)
                throw new BusinessException("Can't change this field, because it's auto set.");
        }

    }
}
