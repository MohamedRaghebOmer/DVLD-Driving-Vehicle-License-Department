using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Business.EntityValidators
{
    internal static class DriverValidator
    {
        public static void AddNewValidator(Driver driver)
        {
            Core.Validators.DriverValidator.Validate(driver);

            if (PersonData.Exists(driver.PersonId))
            {
                if (DriverData.IsPersonUsed(driver.PersonId, -1))
                    throw new BusinessException("The person is already associated with another driver.");
            }
            else
                throw new BusinessException("Person doesn't exists.");
        }

        public static void UpdateValidator(Driver driver)
        {
            Core.Validators.DriverValidator.Validate(driver);
            Driver storedInfo = DriverData.GetByDriverId(driver.DriverId);

            if (storedInfo.CreatedDate == driver.CreatedDate)
            {
                if (PersonData.Exists(driver.PersonId))
                {
                    if (DriverData.IsPersonUsed(driver.PersonId, driver.DriverId))
                        throw new BusinessException("The person is already associated with another driver.");
                }
                else
                    throw new BusinessException("Person doesn't exists.");
            }
            else
                throw new BusinessException("Creation date can't be changed.");
        }
    }
}   
