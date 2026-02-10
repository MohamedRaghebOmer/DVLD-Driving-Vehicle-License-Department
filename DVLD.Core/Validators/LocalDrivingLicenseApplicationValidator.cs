using System;
using DVLD.Core.Exceptions;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Core.Validators
{
    public static class LocalDrivingLicenseApplicationValidator
    {
        public static void Validate(LocalDrivingLicenseApplication localDrivingLicenseApplication)
        {
            if (localDrivingLicenseApplication == null)
                throw new ValidationException("LocalDrivingLicenseApplication cannot be empty.");

            if (localDrivingLicenseApplication.ApplicationId <= 0)
                throw new ValidationException("ApplicationId must be a positive integer.");

            if (!Enum.IsDefined(typeof(LicenseClass), localDrivingLicenseApplication.LicenseClassId))
                throw new ValidationException("Invalid LicenseClassId.");
        }
    }
}
