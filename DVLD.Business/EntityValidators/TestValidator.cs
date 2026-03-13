using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class TestValidator
    {
        public static void AddNewValidator(Test test)
        {
            Core.Validators.TestValidator.Validate(test);

            if (!TestAppointmentRepository.Exists(test.TestAppointmentID))
                throw new ValidationException("The specified TestAppointmentID does not exist.");

            if (TestRepository.ExistsByTestAppointment(test.TestAppointmentID))
                throw new ValidationException("A test with the same TestAppointmentID already exists.");
        }
    }
}