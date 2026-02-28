using System;

namespace DVLD.Core.DTOs.Entities
{
    public class Driver
    {
        public int DriverId { get; private set; }

        public int PersonId { get; set; }

        public int CreatedByUserId { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public Driver()
        {
            this.DriverId = -1;
            this.PersonId = -1;
            this.CreatedByUserId = -1;
            this.CreatedDate = new DateTime(1900, 1, 1); // Default value for short date
        }

        internal Driver(int driverId, int personId, int createdByUserId,
            DateTime createdDate) : this()
        {
            this.DriverId = driverId;
            this.PersonId = personId;
            this.CreatedByUserId = createdByUserId;
            this.CreatedDate = createdDate;
        }
    }
}
