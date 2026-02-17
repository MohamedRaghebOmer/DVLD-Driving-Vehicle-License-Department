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
        public static TestAppointment Add(TestAppointment testAppointment)
        {
            TestAppointmentValidator.AddNewValidator(testAppointment);

            try
            {
                int insertedId = TestAppointmentData.Add(testAppointment);
                return TestAppointmentData.GetById(insertedId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating a new TestAppointment.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
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

        public static TestAppointment GetById(int testAppointmentId)
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

        public static bool UpdateDate(int testAppointmentId, DateTime newDate)
        {
            if (testAppointmentId <= 0)
                throw new ValidationException("Test Appointment id to update must me a postive integer.");

            if (TestAppointmentData.GetById(testAppointmentId).AppointmentDate <= DateTime.Now)
                throw new ValidationException("Can't update date of Test Appointment that has already passed.");
            
            if (newDate < DateTime.Now)
                throw new ValidationException("Test Appointment date cannot be in the past.");

            try
            {
                return TestAppointmentData.UpdateDate(testAppointmentId, newDate);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating date to Test Appointment wiht id = {testAppointmentId}.");
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
                return TestAppointmentData.Delete(testAppointmentId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting Test Appointment wiht id = {testAppointmentId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
