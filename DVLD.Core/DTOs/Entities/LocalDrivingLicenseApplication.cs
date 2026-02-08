using System;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class LocalDrivingLicenseApplication
    {
        private int _localDrivingLicenseApplicationId;
        private int _applicationId;
        private LicenseClass _licenseClassId;

        public int LocalDrivingLicenseApplicationId
        {
            get => _localDrivingLicenseApplicationId;
            private set
            {
                if (value <= 0)
                    throw new ValidationException("LocalDrivingLicenseApplicationId must be a positive integer.");
                _localDrivingLicenseApplicationId = value;
            }
        }

        public int ApplicationId
        {
            get => _applicationId;
            set
            {
                if (value <= 0)
                    throw new ValidationException("ApplicationId must be a positive integer.");
                _applicationId = value;
            }
        }

        public LicenseClass LicenseClassId
        {
            get => _licenseClassId;
            set
            {
                if (!Enum.IsDefined(typeof(LicenseClass), value))
                    throw new ValidationException("Invalid LicenseClassId.");
                _licenseClassId = value;
            }
        }


        public LocalDrivingLicenseApplication()
        {
            this._localDrivingLicenseApplicationId = -1;
            this._applicationId = -1;
            this._licenseClassId = 0; // Default value indicating not set
        }

        public LocalDrivingLicenseApplication(int applicationId, LicenseClass licenseClassId) : this()
        {
            ApplicationId = applicationId;
            LicenseClassId = licenseClassId;
        }

        internal LocalDrivingLicenseApplication(int localDrivingLicenseApplicationId, int applicationId, LicenseClass licenseClassId) : this()
        {
            LocalDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            ApplicationId = applicationId;
            LicenseClassId = licenseClassId;
        }
    }
}
