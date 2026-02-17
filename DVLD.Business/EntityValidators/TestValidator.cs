using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal class TestValidator
    {
        public static void AddNewValidator(Test test)
        {
            Core.Validators.TestValidator.Validate(test);

            if (test.TestID != -1)
                throw new ValidationException("This TestID is not valid for a new test.");

            if (!TestAppointmentData.Exists(test.TestAppointmentID))
                throw new ValidationException("The specified TestAppointmentID does not exist.");

            if (TestData.ExistsForTestAppointment(test.TestAppointmentID))
                throw new ValidationException("A test with the same TestAppointmentID already exists.");
        }
    }
}