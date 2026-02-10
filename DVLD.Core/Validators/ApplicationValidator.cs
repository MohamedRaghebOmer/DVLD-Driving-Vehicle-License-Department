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

            if (application.ApplicantPersonId <= 0)
                throw new ValidationException("ApplicantPersonId must be a positive integer.");

            if (application.ApplicationDate == new DateTime(1, 1, 1))
                throw new ValidationException("ApplicationDate must be a valid date.");

            if (!Enum.IsDefined(typeof(ApplicationType), application.ApplicationTypeId))
                throw new ValidationException("Invalid ApplicationTypeId.");

            if (!Enum.IsDefined(typeof(ApplicationStatus), application.ApplicationStatus))
                throw new ValidationException("Invalid ApplicationStatus.");

            if (application.LastStatusDate == new DateTime(1, 1, 1))
                throw new ValidationException("LastStatusDate must be a valid date.");

            if (application.PaidFees < 0)
                throw new ValidationException("PaidFees cannot be negative.");

            if (application.CreatedByUserId <= 0)
                throw new ValidationException("CreatedByUserId must be a positive integer.");
        }
    }
}
