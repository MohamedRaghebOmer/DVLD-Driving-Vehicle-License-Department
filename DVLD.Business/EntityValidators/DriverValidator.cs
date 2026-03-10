using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class DriverValidator
    {
        public static void AddNewValidator(Driver driver)
        {
            Core.Validators.DriverValidator.Validate(driver);

            if (!PersonRepository.Exists(driver.PersonId))
                throw new BusinessException("Person doesn't exists.");

            if (DriverRepository.ExistsByPersonId(driver.PersonId))
                throw new BusinessException("The person is already associated with another driver.");
        }
    }
}
