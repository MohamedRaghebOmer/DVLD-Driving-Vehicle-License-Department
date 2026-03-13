using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class LicenseClassService
    {
        private const string _licenseClassErrorMessage = "Undefined license class.";

        public static DataTable GetAll()
        {
            try
            {
                return LicenseClassRepository.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting all license classes.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static int GetMinimumAllowedAge(LicenseClass licenseClass)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException(_licenseClassErrorMessage);

            try
            {
                return LicenseClassRepository.GetMinimumAllowedAge(licenseClass);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting minimum allowed age for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static int GetDefaultValidityLength(LicenseClass licenseClass)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException(_licenseClassErrorMessage);

            try
            {
                return LicenseClassRepository.GetDefaultValidityLength(licenseClass);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting default validity length for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static decimal GetClassFees(LicenseClass licenseClass)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException(_licenseClassErrorMessage);
            try
            {
                return LicenseClassRepository.GetFees(licenseClass);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting class fees for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool UpdateMinimumAllowedAge(LicenseClass licenseClass, int newMinimumAge)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException(_licenseClassErrorMessage);
            if (newMinimumAge < 0)
                throw new ValidationException("Minimum age cannot be negative.");
            try
            {
                return LicenseClassRepository.UpdateMinimumAllowedAge(licenseClass, newMinimumAge);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while updating minimum allowed age for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool UpdateDefaultValidityLength(LicenseClass licenseClass, int newValidityLength)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException(_licenseClassErrorMessage);
            if (newValidityLength < 0)
                throw new ValidationException("Validity length cannot be negative.");
            try
            {
                return LicenseClassRepository.UpdateDefaultValidityLength(licenseClass, newValidityLength);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while updating default validity length for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool UpdateClassFees(LicenseClass licenseClass, decimal newFees)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException(_licenseClassErrorMessage);

            if (newFees < 0)
                throw new ValidationException("new Fees cannot be negative.");

            try
            {
                return LicenseClassRepository.UpdateClassFees(licenseClass, newFees);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while updating class fees for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static string GetLicenseClassName(LicenseClass licenseClass)
        {
            switch (licenseClass)
            {
                case LicenseClass.Class1_SmallMotorcycle:
                    return "Class 1 - Small Motorcycle";

                case LicenseClass.Class2_HeavyMotorcycle:
                    return "Class 2 - Heavy Motorcycle";

                case LicenseClass.Class3_OrdinaryDrivingLicense:
                    return "Class 3 - Ordinary Driving License";

                case LicenseClass.Class4_Commercial:
                    return "Class 4 - Commercial";

                case LicenseClass.Class5_Agricultural:
                    return "Class 5 - Agricultural";

                case LicenseClass.Class6_SmallAndMediumBus:
                    return "Class 6 - Small and Medium Bus";

                case LicenseClass.Class7_TruckAndHeavyVehicle:
                    return "Class 7 - Truck and Heavy Vehicle";
            }

            return null;
        }

    }
}
