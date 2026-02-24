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

            if (!PersonRepository.Exists(driver.PersonId))
                throw new BusinessException("Person doesn't exists.");
            
            if (DriverRepository.ExistsForPerson(driver.PersonId))
                throw new BusinessException("The person is already associated with another driver.");
        }
    }
}   
