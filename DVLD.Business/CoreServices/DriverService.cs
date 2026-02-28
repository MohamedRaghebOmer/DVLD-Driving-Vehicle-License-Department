using DVLD.Business.EntityValidators;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class DriverService
    {
        public static Driver Add(Driver driver)
        {
            DriverValidator.AddNewValidator(driver);

            try
            {
                return (DriverRepository.Add(driver) != -1) ? DriverRepository.GetById(driver.DriverId) : null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while adding a new driver.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Driver GetByDriverId(int driverId)
        {
            if (driverId < 1)
                return null;

            try
            {
                return DriverRepository.GetById(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get driver by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Driver GetByPersonId(int personId)
        {
            if (personId < 1)
                return null;

            try
            {
                return DriverRepository.GetByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get driver by person id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return DriverRepository.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all drivers.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool ExistsForPerson(int personId, int excludedDriverId = -1)
        {
            if (personId < 1)
                return false;

            try
            {
                return DriverRepository.ExistsForPerson(personId, excludedDriverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if person is used by any driver excluding a specific driver.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exists(int driverId)
        {
            if (driverId < 1)
                return false;

            try
            {
                return DriverRepository.Exists(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking existence of driver by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByDriverId(int driverId)
        {
            if (driverId < 1)
                return false;

            try
            {
                return DriverRepository.DeleteByDriverId(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Errror while deleting dirver with id = {driverId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByPersonId(int personId)
        {
            if (personId < 1)
                return false;

            try
            {
                return DriverRepository.DeleteByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Errror while deleting dirver with person id = {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }
    }
}
