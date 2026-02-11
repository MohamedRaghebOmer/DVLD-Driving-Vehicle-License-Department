using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public static class TestAppointmentBusiness
    {
        public static TestAppointment Save(TestAppointment testAppointment)
        {
            // Add New TestAppointment
            if (testAppointment.TestAppointmentId == -1)
            {
                TestAppointmentValidator.AddNewValidator(testAppointment);

                try
                {
                    int insertedId = TestAppointmentData.AddNewTestAppointment(testAppointment);
                    return TestAppointmentData.GetById(insertedId);
                }
                catch(Exception ex)
                {
                    AppLogger.LogError("BLL: Error while creating a new TestAppointment.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
            else // Update an existing TestAppointment.
            {
                TestAppointmentValidator.UpdateValidator(testAppointment);

                try
                {
                    if (TestAppointmentData.UpdateTestAppointment(testAppointment))
                        return TestAppointmentData.GetById(testAppointment.TestAppointmentId);
                    return null;
                }
                catch(Exception ex)
                {
                    AppLogger.LogError($"BLL: Error while updating an existing TestAppointment with ID = {testAppointment.TestAppointmentId}.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static DataTable GetAllTestAppointments()
        {
            try
            {
                return TestAppointmentData.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all TestAppointments.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static TestAppointment GetTestAppointment(int testAppointmentId)
        {
            if (testAppointmentId <= 0)
                return null;

            try
            {
                return TestAppointmentData.GetById(testAppointmentId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while Get Test Appointment wiht id = {testAppointmentId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsLocked(int testAppointmentId)
        {
            if (testAppointmentId <= 0)
                throw new ValidationException("Test Appointment id to check must me a postive integer.");

            try
            {
                return TestAppointmentData.IsLocked(testAppointmentId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while check is locked to Test Appointment wiht id = {testAppointmentId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(int testAppointmentId)
        {
            if (testAppointmentId <= 0)
                throw new ValidationException("Test Appointment id to delete must me a postive integer.");

            try
            {
                return TestAppointmentData.DeleteTestAppointment(testAppointmentId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting Test Appointment wiht id = {testAppointmentId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }


        }
    }
}
