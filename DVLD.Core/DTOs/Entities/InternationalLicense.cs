using System;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class InternationalLicense
    {
        public int InternationalLicenseID { get; private set; }

        public int ApplicationID { get; set; }

        public int DriverID { get; set; }

        public int IssuedUsingLocalLicenseID { get; set; }

        public DateTime IssueDate { get; private set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; private set; }

        public int CreatedByUserID { get; private set; }


        internal InternationalLicense(int internationalLicenseId, int applicationId, int driverId, int issuedUsingLocalLicenseId, DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserId) : this()
        {
            InternationalLicenseID = internationalLicenseId;
            ApplicationID = applicationId;
            DriverID = driverId;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseId;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserId;
        }
    }
}
