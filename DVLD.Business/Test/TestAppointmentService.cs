using DVLD.Business.EntityValidators;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class TestAppointmentService
    {
        public static int Add(TestAppointment testAppointment)
        {
            TestAppointmentValidator.ValidateForAdd(testAppointment);

            try
            {
                return TestAppointmentRepository.Add(testAppointment);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while creating a new TestAppointment.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return TestAppointmentRepository.GetAll();
            }
            catch (Exception ex)
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
                return TestAppointmentRepository.GetById(testAppointmentId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while Get Test Appointment with id = {testAppointmentId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static int GetIdByLocalAppId(int localApplicationId)
        {
            if (localApplicationId <= 0)
                return -1;

            try
            {
                return TestAppointmentRepository.GetIdByLocalAppId(localApplicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while Get Test Appointment with LocalDrivingLicenseApplicationId = {localApplicationId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static TestAppointment GetByLocalAppId(int localApplicationId)
        {
            if (localApplicationId <= 0)
                return null;

            try
            {
                return TestAppointmentRepository.GetByLocalAppId(localApplicationId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while Get Test Appointment with LocalDrivingLicenseApplicationId = {localApplicationId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool ExistsByLocalApplicationId(int localApplicationId, bool isLocked = false)
        {
            if (localApplicationId <= 0)
                return false;

            try
            {
                return TestAppointmentRepository.ExistsByLocalApplicationId(localApplicationId, isLocked);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if Test Appointment with LocalDrivingLicenseApplicationId = {localApplicationId} exists.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetShortAppointmentInfo(int localApplicationId, TestType testType)
        {
            if (localApplicationId <= 0)
                return null;

            try
            {
                return TestAppointmentRepository.GetShortAppointmentInfo(localApplicationId, testType);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while Get Test Appointment with id = {localApplicationId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Update(TestAppointment testAppointment)
        {
            if (testAppointment == null)
                throw new ValidationException("Test Appointment to update can't be null.");

            if (TestAppointmentRepository.IsLocked(testAppointment.TestAppointmentId))
                throw new ValidationException("Can't update Test Appointment that is locked.");

            if (testAppointment.AppointmentDate.Date < DateTime.Now.Date)
                throw new ValidationException("Test Appointment date cannot be in the past.");

            try
            {
                return TestAppointmentRepository.Update(testAppointment);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating Test Appointment with id = {testAppointment.TestAppointmentId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsLocked(int testAppointmentId)
        {
            if (testAppointmentId <= 0)
                throw new ValidationException("Test Appointment id to check must me a postive integer.");

            try
            {
                return TestAppointmentRepository.IsLocked(testAppointmentId);
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
                return false;

            try
            {
                return TestAppointmentRepository.Delete(testAppointmentId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting Test Appointment with id = {testAppointmentId}.");
                throw new Exception("Deletion failed. This test appointment has associated data.", ex);
            }
        }

        public static bool IsThereOpenedAppointment(int localApplicationId, bool isLocked = false)
            => ExistsByLocalApplicationId(localApplicationId, isLocked);
    }
}
