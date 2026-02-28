using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;

namespace DVLD.Business
{
    public static class TestTypeService
    {
        public static DataTable GetAll()
        {
            try
            {
                return TestTypeRepository.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all test type.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static decimal GetTestTypeFees(TestType testType)
        {
            if (!Enum.IsDefined(typeof(TestType), testType))
                throw new ValidationException("Invalid test type.");

            try
            {
                return TestTypeRepository.GetTestTypeFees(testType);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to get fees for test type with id = {(int)testType}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool UpdateFees(TestType testType, decimal newFees)
        {
            if (!Enum.IsDefined(typeof(TestType), testType))
                throw new ValidationException("Invalid test type.");

            if (newFees < 0)
                throw new ValidationException("New fees can't be negative.");

            try
            {
                return TestTypeRepository.UpdateFees(testType, newFees);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating fees for test type with id = {(int)testType}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
