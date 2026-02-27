using System;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class Application
    {
        public int ApplicationID { get; private set; }

        public int ApplicantPersonID { get; set; }

        public DateTime ApplicationDate { get; private set; }

        public ApplicationType ApplicationTypeID { get; set; }

        public ApplicationStatus ApplicationStatus { get; private set; }

        public DateTime LastStatusDate { get; private set; }

        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; private set; }


        public Application()
        {
            this.ApplicationID = -1; // Default value indicating not set
            this.ApplicantPersonID = -1; // Default value indicating not set
            this.ApplicationDate = new DateTime(1, 1, 1); // Default value indicating not set
            this.ApplicationTypeID = 0; // Default value indicating not set
            this.ApplicationStatus = 0; // Default value indicating not set
            this.LastStatusDate = new DateTime(1, 1, 1); // Default value indicating not set
            this.PaidFees = 0; // Default value indicating not set
            this.CreatedByUserID = -1; // Default value indicating not set
        }

        internal Application(int applicationId, int applicantPersonId, 
            DateTime applicationDate, ApplicationType applicationTypeId, 
            ApplicationStatus applicationStatus, DateTime lastStatusDate, 
            decimal paidFees, int createdByUserId) : this()
        {
            ApplicationID = applicationId;
            ApplicantPersonID = applicantPersonId;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeId;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserId;
        }
    }
}
