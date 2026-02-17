using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Business.EntityValidators;
using DVLD.Core.Logging;

namespace DVLD.Business
{
    public static class DriverBusiness
    {
        public static Driver Add(Driver driver)
        {
            DriverValidator.AddNewValidator(driver);
                
            try
            {
                return (DriverData.Add(driver) != -1) ? DriverData.GetById(driver.DriverId) : null;
            }
            catch(Exception ex)
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
                return DriverData.GetById(driverId);
            }
            catch(Exception ex)
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
                return DriverData.GetByPersonId(personId);
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
                return DriverData.GetAll();
            }
            catch(Exception ex)
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
                return DriverData.ExistsForPerson(personId, excludedDriverId);
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
                return DriverData.Exists(driverId);
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
                return DriverData.DeleteByDriverId(driverId);
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
                return DriverData.DeleteByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Errror while deleting dirver with person id = {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }
    }
}
