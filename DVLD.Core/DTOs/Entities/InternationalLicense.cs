using System;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class InternationalLicense
    {
        private int _internationalLicenseId;
        private int _applicationId;
        private int _driverId;
        private int _issuedUsingLocalLicenseId;
        private DateTime _issueDate;
        private DateTime _expirationDate;
        private bool _isActive;
        private int _createdByUserId;


        public int InternationalLicenseID
        {
            get => _internationalLicenseId;
            private set
            {
                if (value <= 0) 
                    throw new ValidationException("International License ID must be greater than 0.");

                _internationalLicenseId = value;
            }
        }

        public int ApplicationID
        {
            get => _applicationId;
            set
            {
                if (value <= 0)
                    throw new ValidationException("Application ID must be greater than 0.");
                _applicationId = value;
            }
        }

        public int DriverID
        {
            get => _driverId;
            set
            {
                if (value <= 0)
                    throw new ValidationException("Driver ID must be greater than 0.");
                _driverId = value;
            }
        }

        public int IssuedUsingLocalLicenseID
        {
            get => _issuedUsingLocalLicenseId;
            set
            {
                if (value <= 0)
                    throw new ValidationException("Issued Using Local License ID must be greater than 0.");
                _issuedUsingLocalLicenseId = value;
            }
        }

        public DateTime IssueDate
        {
            get => _issueDate;
            private set
            {
                if (value == null || value == DateTime.MinValue || value > DateTime.Now)
                    throw new ValidationException("Invalid Issue Date.");
                
                _issueDate = value;
            }
        }

        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set
            {
                if (value == null || value == DateTime.MinValue || value < DateTime.Now)
                    throw new ValidationException("Invalid Expiration Date.");
                
                _expirationDate = value;
            }
        }

        public bool IsActive
        {
            get => _isActive;
            private set => _isActive = value;
        }

        public int CreatedByUserID
        {
            get => _createdByUserId;
            private set
            {
                if (value <= 0)
                    throw new ValidationException("Created By User ID must be greater than 0.");
                _createdByUserId = value;
            }
        }


        private InternationalLicense()
        {
            this._internationalLicenseId = -1;
            this._applicationId = -1;
            this._driverId = -1;
            this._issuedUsingLocalLicenseId = -1;
            this._issueDate = DateTime.MinValue;
            this._expirationDate = DateTime.MinValue;
            this._isActive = false;
            this._createdByUserId = -1;
        }

        public InternationalLicense(int applicationId, int driverId, int issuedUsingLocalLicenseId, DateTime expirationDate) : this()
        {
            ApplicationID = applicationId;
            DriverID = driverId;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseId;
            ExpirationDate = expirationDate;
        }

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
