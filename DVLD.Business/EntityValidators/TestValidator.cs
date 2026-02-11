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

            if (!TestAppointmentData.Exists(test.TestAppointmentID))
                throw new ValidationException("The specified TestAppointmentID does not exist.");

            if (TestData.DoesTestAppointmentExists(test.TestAppointmentID))
                throw new ValidationException("A test with the same TestAppointmentID already exists.");

            if (!UserData.Exists(test.CreatedByUserID))
                throw new ValidationException("The specified CreatedByUserID does not exist.");
        }
    }
}