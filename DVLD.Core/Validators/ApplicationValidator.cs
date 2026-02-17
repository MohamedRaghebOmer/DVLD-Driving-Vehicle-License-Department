using System;
using DVLD.Core.Exceptions;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Core.Validators
{
    public static class ApplicationValidator
    {
        public static void Validate(Application application)
        {
            if (application == null)
                throw new ValidationException("Application cannot be null.");

            if (application.ApplicantPersonID <= 0)
                throw new ValidationException("ApplicantPersonId must be a positive integer.");

            if (!Enum.IsDefined(typeof(ApplicationType), application.ApplicationTypeID))
                throw new ValidationException("Invalid ApplicationTypeId.");

            if (application.PaidFees < 0)
                throw new ValidationException("PaidFees cannot be negative.");
        }
    }
}
