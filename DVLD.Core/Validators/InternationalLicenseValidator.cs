using System;
using DVLD.Core.Exceptions;
using DVLD.Core.DTOs.Entities;

namespace DVLD.Core.Validators
{
    public static class InternationalLicenseValidator
    {
        public static void Validate(InternationalLicense internationalLicense)
        {
            if (internationalLicense == null)
                throw new ValidationException("International License cannot be empty.");

            if (internationalLicense.ApplicationID <= 0)
                throw new ValidationException("Application Id must be a positive integer.");

            if (internationalLicense.DriverID <= 0)
                throw new ValidationException("Driver Id must be a positive integer.");

            if (internationalLicense.IssuedUsingLocalLicenseID <= 0)
                throw new ValidationException("Local license id used to issue this license must be a positive integer.");

            if (internationalLicense.IssueDate == new DateTime(1, 1, 1) || internationalLicense.IssueDate > DateTime.Now)
                throw new ValidationException("Issue date must be a valid date.");

            if (internationalLicense.ExpirationDate == new DateTime(1, 1, 1) || internationalLicense.ExpirationDate < DateTime.Now)
                throw new ValidationException("Expiration date must be a valid date.");

            if (internationalLicense.CreatedByUserID <= 0)
                throw new ValidationException("User Id must be a positive integer.");
        }
    }
}
