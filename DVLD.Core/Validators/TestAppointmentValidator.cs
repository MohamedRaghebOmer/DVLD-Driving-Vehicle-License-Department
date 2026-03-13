using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using System;

namespace DVLD.Core.Validators
{
    public static class TestAppointmentValidator
    {
        public static void Validate(TestAppointment testAppointment)
        {
            if (testAppointment == null)
                throw new ValidationException("Test Appointment cannot be empty.");

            if (!Enum.IsDefined(typeof(TestType), testAppointment.TestTypeId))
                throw new ValidationException("Invalid Test Type.");

            if (testAppointment.AppointmentDate.Date < DateTime.Now.Date)
                throw new ValidationException("The appointment date cannot be in the past.");

            if (testAppointment.LocalDrivingLicenseApplicationId <= 0)
                throw new ValidationException("Local Driving License Application Id does not exist.");

            if (testAppointment.RetakeTestApplicationID <= 0)
                throw new ValidationException("Retake Test Application Id does not exist.");
        }
    }
}
