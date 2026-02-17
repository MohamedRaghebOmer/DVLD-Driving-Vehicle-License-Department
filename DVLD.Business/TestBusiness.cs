using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class TestBusiness
    {
        public static Test Add(Test test)
        {
            TestValidator.AddNewValidator(test);

            try
            {
                int newTestId = TestData.Add(test);
                TestAppointmentData.Lock(test.TestAppointmentID);
                return TestData.GetById(newTestId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating a new Test.");
                throw new Exception("An error occurred while creating the test. Please try again later.", ex);
            }
        }

        public static Test GetById(int testId)
        {
            if (testId <= 0)
                return null;

            try
            {
                return TestData.GetById(testId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while retrieving Test with ID = {testId}.", ex);
                throw new Exception("An error occurred while retrieving the test. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return TestData.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while retrieving all Tests.", ex);
                throw new Exception("An error occurred while retrieving the tests. Please try again later.", ex);
            }
        }

        public static bool UpdateNotes(int testId, string newNotes)
        {
            if (testId <= 0)
                return false;

            try
            {
                return TestData.UpdateNotes(testId, newNotes);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating notes for Test with ID = {testId}.", ex);
                throw new Exception("An error occurred while updating the test notes. Please try again later.", ex);
            }
        }
    }
}