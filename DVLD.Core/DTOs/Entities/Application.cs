using System;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class Application
    {
        private int _applicationId;
        private int _applicantPersonId;
        private DateTime _applicationDate;
        private ApplicationType _applicationTypeId;
        private ApplicationStatus _applicationStatus;
        private DateTime _lastStatusDate;
        private decimal _paidFees;
        private int _createdByUserId;


        public int ApplicationID
        {
            get => _applicationId;
            private set
            {
                if (value <= 0)
                    throw new ValidationException("ApplicationId must be a positive integer.");
                _applicationId = value;
            }
        }

        public int ApplicantPersonID
        {
            get => _applicantPersonId;
            set
            {
                if (value <= 0)
                    throw new ValidationException("ApplicantPersonId must be a positive integer.");
                _applicantPersonId = value;
            }
        }

        public DateTime ApplicationDate
        {
            get => _applicationDate;
            private set
            {
                if (value > DateTime.Now)
                    throw new ValidationException("ApplicationDate cannot be in the future.");
                _applicationDate = value;
            }
        }

        public ApplicationType ApplicationTypeID
        {
            get => _applicationTypeId;
            set
            {
                if (!Enum.IsDefined(typeof(ApplicationType), value))
                    throw new ValidationException("Invalid ApplicationTypeId.");
                _applicationTypeId = value;
            }
        }

        public ApplicationStatus ApplicationStatus
        {
            get => _applicationStatus;
            private set
            {
                if (!Enum.IsDefined(typeof(ApplicationStatus), value))
                    throw new ValidationException("Invalid ApplicationStatus.");
                _applicationStatus = value;
            }
        }

        public DateTime LastStatusDate
        {
            get => _lastStatusDate;
            private set
            {
                if (value > DateTime.Now)
                    throw new ValidationException("LastStatusDate cannot be in the future.");
                _lastStatusDate = value;
            }
        }

        public decimal PaidFees
        {
            get => _paidFees;
            set
            {
                if (value < 0)
                    throw new ValidationException("PaidFees cannot be negative.");
                _paidFees = value;
            }
        }

        public int CreatedByUserID
        {
            get => _createdByUserId;
            private set
            {
                if (value <= 0)
                    throw new ValidationException("CreatedByUserId must be a positive integer.");
                _createdByUserId = value;
            }
        }


        public Application()
        {
            this._applicationId = -1; // Default value indicating not set
            this._applicantPersonId = -1; // Default value indicating not set
            this._applicationDate = new DateTime(1, 1, 1); // Default value indicating not set
            this._applicationTypeId = 0; // Default value indicating not set
            this._applicationStatus = 0; // Default value indicating not set
            this._lastStatusDate = new DateTime(1, 1, 1); // Default value indicating not set
            this._paidFees = 0; // Default value indicating not set
            this._createdByUserId = -1; // Default value indicating not set
        }

        public Application(int applicantPersonId, ApplicationType applicationTypeId, decimal paidFees) : this()
        {
            ApplicantPersonID = applicantPersonId;
            ApplicationTypeID = applicationTypeId;
            PaidFees = paidFees;
        }

        internal Application(int applicationId, int applicantPersonId, DateTime applicationDate, ApplicationType applicationTypeId, ApplicationStatus applicationStatus, DateTime lastStatusDate, decimal paidFees, int createdByUserId) : this()
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
