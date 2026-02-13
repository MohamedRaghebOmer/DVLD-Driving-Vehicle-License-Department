using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Core.Validators
{
    public static class TestValidator
    {
        public static void Validate(Test test)
        {
            if (test == null)
                throw new ValidationException("Test object cannot be null.");

            if (test.TestAppointmentID <= 0)
                throw new ValidationException("TestAppointmentID must be a positive integer.");

            if (test.CreatedByUserID <= 0)
                throw new ValidationException("CreatedByUserID must be a positive integer.");
        }
    }
}