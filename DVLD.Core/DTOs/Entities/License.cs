using System;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class License
    {
        public int LicenseID { get; private set; }

        public int ApplicationId { get; set; }

        public int DriverId { get; set; }

        public LicenseClass LicenseClass { get; set; }
        
        public DateTime IssueDate { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        public string Notes { get; set; }

        public decimal PaidFees { get; set; }

        public bool IsActive { get; private set; }

        public IssueReason IssueReason { get; private set; }

        public int CreatedByUserId { get; private set; }

        // Constructor for receiving data from DAL when retrieving a license (LicenseId is included).
        internal License(int licenseId, int applicationId, int driverId, 
            LicenseClass licenseClass, DateTime issueDate, 
            DateTime expirationDate, string notes, decimal paidFees, 
        bool isActive, IssueReason issueReason, int createdByUserId) : this()
        {
            LicenseID = licenseId;
            ApplicationId = applicationId;
            DriverId = driverId;
            LicenseClass = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserId = createdByUserId;
        }
    }
}
