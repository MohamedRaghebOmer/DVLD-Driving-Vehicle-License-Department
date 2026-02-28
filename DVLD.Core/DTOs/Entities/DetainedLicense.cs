using System;

namespace DVLD.Core.DTOs.Entities
{
    public class DetainedLicense
    {
        public int DetainID { get; private set; }

        public int LicenseID { get; set; }

        public DateTime DetainDate { get; private set; }

        public decimal FineFees { get; set; }

        public int CreatedByUserID { get; private set; }

        public bool IsReleased { get; private set; }

        public DateTime? ReleaseDate { get; private set; }

        public int? ReleasedByUserID { get; private set; }

        public int? ReleaseApplicationID { get; private set; }


        public DetainedLicense()
        {
            this.DetainID = -1; // Default value indicating not set
            this.LicenseID = -1; // Default value indicating not set
            this.DetainDate = DateTime.MinValue; // Default value indicating not set
            this.FineFees = 0; // Default value indicating not set
            this.CreatedByUserID = -1; // Default value indicating not set
            this.IsReleased = false; // Default value indicating not set
            this.ReleaseDate = null; // Default value indicating not set
            this.ReleasedByUserID = null; // Default value indicating not set
            this.ReleaseApplicationID = null; // Default value indicating not set
        }

        internal DetainedLicense(int detainId, int licenseId,
            DateTime detainDate, decimal fineFees, int createdByUserId,
            bool isReleased, DateTime? releaseDate, int? releasedByUserId,
            int? releaseApplicationId) : this()
        {
            DetainID = detainId;
            LicenseID = licenseId;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserId;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserId;
            ReleaseApplicationID = releaseApplicationId;
        }
    }
}
