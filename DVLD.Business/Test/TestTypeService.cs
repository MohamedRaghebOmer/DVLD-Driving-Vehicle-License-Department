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

        public static DataTable Get(TestType testType)
        {
            try
            {
                return TestTypeRepository.Get(testType);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get test type.");
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

        public static bool Update(TestType testType, string newTitle,
            string newDescription, decimal newFees)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new BusinessException("Test Type Title must be at least 1 character.");

            if (newFees < 0)
                throw new BusinessException("Fees must be positive.");

            try
            {
                return TestTypeRepository.Update(testType, newTitle,
                    newDescription, newFees);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating test type with id = {(int)testType}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
