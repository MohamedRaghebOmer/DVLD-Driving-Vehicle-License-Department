using System;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Core.Validators
{
    public static class DriverValidator
    {
        public static void Validate(Driver driver)
        {
            if (driver == null)
                throw new ValidationException("Driver can't be empty.");

            if (driver.PersonId < 1)
                throw new ValidationException("Person Id can't be negative.");
        }
    }
}
