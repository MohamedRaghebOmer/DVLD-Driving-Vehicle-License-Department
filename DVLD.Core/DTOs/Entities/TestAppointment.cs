using DVLD.Core.DTOs.Enums;
using System;

namespace DVLD.Core.DTOs.Entities
{
    public class TestAppointment
    {
        public int TestAppointmentId { get; private set; }

        public TestType TestTypeId { get; set; }

        public int LocalDrivingLicenseApplicationId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public decimal PaidFees { get; private set; }

        public int CreatedByUserId { get; private set; }

        public bool IsLocked { get; private set; }

        public int? RetakeTestApplicationID { get; set; }

        public TestAppointment()
        {
            this.TestAppointmentId = -1;
            this.TestTypeId = 0;
            this.LocalDrivingLicenseApplicationId = -1;
            this.AppointmentDate = new DateTime(1, 1, 1);
            this.PaidFees = 0;
            this.CreatedByUserId = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = null;
        }

        internal TestAppointment(int testAppointmentId, TestType testTypeId,
            int localDrivingLicenseApplicationId, DateTime appointmentDate,
            decimal paidFees, int createdByUserId, bool isLocked, int? retakeTestApplicationID) : this()
        {
            TestAppointmentId = testAppointmentId;
            TestTypeId = testTypeId;
            LocalDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserId = createdByUserId;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;
        }
    }
}
