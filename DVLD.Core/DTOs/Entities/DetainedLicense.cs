using System;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class DetainedLicense
    {
        private int _detainId;
        private int _licenseId;
        private DateTime _detainDate;
        private decimal _fineFees;
        private int _createdByUserId;
        private bool _isReleased;
        private DateTime? _releaseDate;
        private int? _releasedByUserId;
        private int?_releaseApplicationId;


        public int DetainID
        {
            get => _detainId;
            internal set
            {
                if (value < 0)
                    throw new ValidationException("Detain ID cannot be negative.");
                _detainId = value;
            }
        }

        public int LicenseID
        {
            get => _licenseId;
            
            set
            {
                if (value < 0)
                    throw new ValidationException("License ID cannot be negative.");
                _licenseId = value;
            }
        }

        public DateTime DetainDate
        {
            get => _detainDate;
            
            internal set
            {
                if (value > DateTime.Now)
                    throw new ValidationException("Detain date cannot be in the future.");
                _detainDate = value;
            }
        }

        public decimal FineFees
        {
            get => _fineFees;
            
            set
            {
                if (value < 0)
                    throw new ValidationException("Fine fees cannot be negative.");
                _fineFees = value;
            }
        }

        public int CreatedByUserID
        {
            get => _createdByUserId;
            
            set
            {
                if (value < 0)
                    throw new ValidationException("Created by user ID cannot be negative.");
                _createdByUserId = value;
            }
        }

        public bool IsReleased
        {
            get => _isReleased;
            internal set => _isReleased = value;
        }

        public DateTime? ReleaseDate
        {
            get => _releaseDate;
            
            internal set
            {
                if (value == null)
                    _releaseDate = null;
                else if (value > DateTime.Now)
                    throw new ValidationException("Release date cannot be in the future.");
                
                _releaseDate = value;
            }
        }

        public int? ReleasedByUserID
        {
            get => _releasedByUserId;
            
            set
            {
                if (value == null)
                    _releaseApplicationId = null;
                else if (value < 0)
                    throw new ValidationException("Released by user ID cannot be negative.");
                _releasedByUserId = value;
            }
        }

        public int? ReleaseApplicationID
        {
            get => _releaseApplicationId;
            
            set
            {
                if (value == null)
                    _releaseApplicationId = null;
                else if (value < 0)
                    throw new ValidationException("Release application ID cannot be negative.");
                _releaseApplicationId = value;
            }
        }


        private DetainedLicense()
        {
            this._detainId = -1; // Default value indicating not set
            this._licenseId = -1; // Default value indicating not set
            this._detainDate = new DateTime(1, 1, 1); // Default value indicating not set
            this._fineFees = 0; // Default value indicating not set
            this._createdByUserId = -1; // Default value indicating not set
            this._isReleased = false; // Default value indicating not set
            this._releaseDate = null; // Default value indicating not set
            this._releasedByUserId = null; // Default value indicating not set
            this._releaseApplicationId = null; // Default value indicating not set
        }

        public DetainedLicense(int licenseId, decimal fineFees, int createdByUserId, int? releaseByUserId , int? releaseApplicationId) : this()
        {
            LicenseID = licenseId;
            FineFees = fineFees;
            CreatedByUserID = createdByUserId;
            ReleasedByUserID = releaseByUserId;
            ReleaseApplicationID = releaseApplicationId;
        }

        internal DetainedLicense(int detainId, int licenseId, DateTime detainDate, decimal fineFees, int createdByUserId, bool isReleased, DateTime? releaseDate, int? releasedByUserId, int? releaseApplicationId) : this()
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
