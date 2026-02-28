namespace DVLD.Core.DTOs.Entities
{
    public class Test
    {
        public int TestID { get; private set; }

        public int TestAppointmentID { get; set; }

        public bool TestResult { get; set; }

        public string Notes { get; set; }

        public int CreatedByUserID { get; private set; }


        public Test()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;
        }

        internal Test(int testId, int testAppointmentId, bool testResult,
            string notes, int createdByUserId) : this()
        {
            TestID = testId;
            TestAppointmentID = testAppointmentId;
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserId;
        }
    }
}
