using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Core.Validators
{
    public static class DetainedLicenseValidator
    {
        public static void Validate(DetainedLicense detainedLicense)
        {
            if (detainedLicense == null)
                throw new ValidationException("DetainedLicense cannot be null.");

            if (detainedLicense.LicenseID <= 0)
                throw new ValidationException("LicenseID must be a positive integer.");

            if (detainedLicense.FineFees < 0)
                throw new ValidationException("FineFees cannot be negative.");
        }
    }
}
