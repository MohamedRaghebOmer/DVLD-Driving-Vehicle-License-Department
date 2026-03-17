using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Data;
using System;

namespace DVLD.Business.EntityValidators
{
    internal static class DetainedLicenseValidator
    {
        public static void ValidateForDetain(int licenseId)
        {
            License license = null;

            if (licenseId <= 0 || (license = LicenseRepository.GetById(licenseId)) == null)
                throw new BusinessException("License does not exist.");

            if (license.ExpirationDate <= DateTime.Now)
                throw new BusinessException("License is expired.");

            if (!license.IsActive)
                throw new BusinessException("License is not active.");

            if (DetainedLicenseRepository.IsDetained(licenseId))
                throw new BusinessException("License is already detained.");
        }

        public static void ValidateForRelease(int licenseId)
        {
            License license = null;

            if (licenseId <= 0 || (license = LicenseRepository.GetById(licenseId)) == null)
                throw new BusinessException("License does not exist.");

            if (!license.IsActive)
                throw new BusinessException("License is not active");

            if (license.ExpirationDate <= DateTime.Now)
                throw new BusinessException("License is expired.");

            if (!DetainedLicenseService.IsDetained(licenseId))
                throw new BusinessException("License is not detained");
        }
    }
}
