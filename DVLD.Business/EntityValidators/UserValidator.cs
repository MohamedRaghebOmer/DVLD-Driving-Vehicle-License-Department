using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class UserValidator
    {
        public static void AddNewValidator(User user)
        {
            Core.Validators.UserValidator.Validate(user);

            if (UserRepository.ExistsByPersonId(user.PersonId))
                throw new BusinessException("The person is already associated with another user.");

            if (UserRepository.ExistsByUsername(user.Username))
                throw new BusinessException("The username is already taken.");
        }

        public static void UpdateValidator(User user)
        {
            Core.Validators.UserValidator.Validate(user);

            if (user.UserId < 1)
                throw new BusinessException($"User With Id = {user.UserId} does not exist.");

            if (UserRepository.ExistsByPersonId(user.PersonId, user.UserId))
                throw new BusinessException("The person is already associated with another user.");

            if (UserRepository.ExistsByUsername(user.Username, user.UserId))
                throw new BusinessException("The username is already taken by another user.");
        }
    }
}
