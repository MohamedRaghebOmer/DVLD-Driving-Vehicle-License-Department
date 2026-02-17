using System;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class TestAppointment
    {
        private int _testAppointmentId;
        private TestType _testTypeId;
        private int _localDrivingLicenseApplicationId;
        private DateTime _appointmentDate;
        private decimal _paidFees;
        private int _createdByUserId;
        private bool _isLocked;


        public int TestAppointmentId
        {
            get => _testAppointmentId;
            private set
            {
                if (value > 0)
                    _testAppointmentId = value;
                else
                    throw new ValidationException("Invalid TestAppointmentId");
            }
        }

        public TestType TestTypeId
        {
            get => _testTypeId;
            set => _testTypeId = value;
        }

        public int LocalDrivingLicenseApplicationId
        {
            get => _localDrivingLicenseApplicationId;
            set
            {
                if (value > 0)
                    _localDrivingLicenseApplicationId = value;
                else
                    throw new ValidationException("Invalid LocalDrivingLicenseApplicationId");
            }
        }

        public DateTime AppointmentDate
        {
            get => _appointmentDate;
            set
            {
                if (value > DateTime.Now)
                    _appointmentDate = value;
                else
                    throw new ValidationException("Appointment date must be in the future.");
            }
        }

        public decimal PaidFees
        {
            get => _paidFees;
            set
            {
                if (value >= 0)
                    _paidFees = value;
                else
                    throw new ValidationException("Paid fees cannot be negative.");
            }
        }

        public int CreatedByUserId
        {
            get => _createdByUserId;
            private set
            {
                if (value > 0)
                    _createdByUserId = value;
                else
                    throw new ValidationException("Invalid CreatedByUserId");
            }
        }

        public bool IsLocked
        {
            get => _isLocked;
            private set => _isLocked = value;
        }


        public TestAppointment()
        {
            this._testAppointmentId = -1;
            this._testTypeId = 0;
            this._localDrivingLicenseApplicationId = -1;
            this._appointmentDate = new DateTime(1, 1, 1);
            this._paidFees = 0;
            this._createdByUserId = -1;
            this._isLocked = false;
        }

        public TestAppointment(TestType testTypeId, int localDrivingLicenseApplicationId, DateTime appointmentDate, decimal paidFees) : this()
        {
            TestTypeId = testTypeId;
            LocalDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
        }

        internal TestAppointment(int testAppointmentId, TestType testTypeId, int localDrivingLicenseApplicationId, DateTime appointmentDate, decimal paidFees, int createdByUserId, bool isLocked) : this()
        {
            TestAppointmentId = testAppointmentId;
            TestTypeId = testTypeId;
            LocalDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserId = createdByUserId;
            IsLocked = isLocked;
        }
    }
}
