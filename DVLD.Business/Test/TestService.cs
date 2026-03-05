using DVLD.Business.EntityValidators;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class TestService
    {
        public static Test Add(Test test)
        {
            TestValidator.AddNewValidator(test);

            try
            {
                int newTestId = TestRepository.Add(test);
                TestAppointmentRepository.Lock(test.TestAppointmentID);
                return TestRepository.GetById(newTestId);
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
                return TestRepository.GetById(testId);
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
                return TestRepository.GetAll();
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
                return TestRepository.UpdateNotes(testId, newNotes);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating notes for Test with ID = {testId}.", ex);
                throw new Exception("An error occurred while updating the test notes. Please try again later.", ex);
            }
        }

        public static bool HasPassed(string NationalNo, TestType testType)
        {
            return false;
            //if (string.IsNullOrWhiteSpace(NationalNo))
            //    return false;

            //try
            //{
            //    return TestRepository.HasPassed();
            //}
            //catch(Exception ex)
            //{
            //    AppLogger.LogError($"BLL: Error while checking if the person with national no = {NationalNo} has passed test type wit id {(int)testType}.", ex);
            //    throw new Exception("An error occurred while updating the test notes. Please try again later.", ex);
            //}
        }
    }
}