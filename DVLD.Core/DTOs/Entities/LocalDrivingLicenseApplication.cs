using System;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class LocalDrivingLicenseApplication
    {
        public int LocalDrivingLicenseApplicationID { get; private set; }

        public int ApplicationID { get; set; }

        public LicenseClass LicenseClassID { get; set; }

        public LocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = 0; // Default value indicating not set
        }

        internal LocalDrivingLicenseApplication(int localId, 
            int applicationId, LicenseClass licenseClassId) : this()
        {
            LocalDrivingLicenseApplicationID = localId;
            ApplicationID = applicationId;
            LicenseClassID = licenseClassId;
        }
    }
}
