using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class InternationalLicenseValidator
    {
        public static void ValidateForAdd(int localClass3LicenseId)
        {
            License localLicense = null;

            // Check if the local license exists
            if (localClass3LicenseId <= 0 || (localLicense = LicenseRepository.GetById(localClass3LicenseId)) == null)
                throw new BusinessException("The local license does not exist.");

            // Check if the local license is not already assigned to an international license.
            if (InternationalLicenseRepository.ExistsByLocalLicenseId(localLicense.LicenseID))
                throw new BusinessException("The local license is already assigned to an international license.");

            // Check if the driver is already assigned to an international license.
            if (InternationalLicenseRepository.ExistsByDriver(localLicense.DriverId))
                throw new BusinessException("The driver is already assigned to an international license.");

            // Check if the local license is a class 3 license.
            if (localLicense.LicenseClass != LicenseClass.Class3_OrdinaryDrivingLicense)
                throw new BusinessException("The local license must be a class 3 license.");

            // LocalLicense must be Active and not expired
            if (!localLicense.IsActive)
                throw new BusinessException("Local license is not active.");

            // Check if the local license is not expired.
            if (LicenseRepository.IsExpired(localLicense.LicenseID))
                throw new BusinessException("Local license is expired.");

            // Check if the local license is not detained.
            if (DetainedLicenseRepository.IsDetained(localLicense.LicenseID))
                throw new BusinessException("The local license is detained.");
        }
    }
}
