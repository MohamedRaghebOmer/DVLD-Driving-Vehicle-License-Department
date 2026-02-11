using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class Test
    {
        private int _testId;
        private int _testAppointmentId;
        private bool _testResult;
        private string _notes;
        private int _createdByUserId;

        
        public int TestID
        {
            get => _testId;

            private set
            {
                if (value <= 0)
                    throw new ValidationException("Test Id must be a positive integer.");
                _testId = value;
            }
        }

        public int TestAppointmentID
        {
            get => _testAppointmentId;

            set
            {
                if (value <= 0)
                    throw new ValidationException("Tets Appointment Id must be a positive intger.");
                _testAppointmentId = value;
            }
        }

        public bool TestResult
        {
            get => _testResult;
            set => _testResult = value;
        }

        public string Notes
        {
            get => _notes;
            set => _notes = value;
        }

        public int CreatedByUserID
        {
            get => _createdByUserId;

            set
            {
                if (value <= 0)
                    throw new ValidationException("User Id must be a positve intger.");
            }
        }


        public Test()
        {
            this._testId = -1;
            this._testAppointmentId = -1;
            this._testResult = false;
            this._notes = string.Empty;
            this._createdByUserId = -1;
        }

        public Test(int testAppointmentId, bool testResult, string notes, int createdByUserId) : this()
        {
            TestAppointmentID = testAppointmentId;
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserId;
        }

        internal Test(int testId, int testAppointmentId, bool testResult, string notes, int createdByUserId) : this()
        {
            TestID = testId;
            TestAppointmentID = testAppointmentId;
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserId;
        }
    }
}
