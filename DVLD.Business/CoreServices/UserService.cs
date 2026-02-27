using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public static class UserService
    {
        public static int Add(User user)
        {
            UserValidator.AddNewValidator(user);

            try
            {
                return UserRepository.Add(user);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while saving new user.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Login(string username, string password)
        {
            // Check username existance.
            if (!UserRepository.Exists(username))
                return false;

            // Check if username is not active.
            if (!UserRepository.IsActive(username))
                throw new BusinessException("Your account was deactivated. Contact your admin to activate you.");

            // Check if the password is not correct.
            if (!UserRepository.IsCorrectUsernameAndPassword(username, password, true))
                return false;

            // Existed username, Active, Correct password for the username
            return true;
        }

        public static User Save(User user)
        {
            if (user == null)
                throw new ValidationException("User cannot be empty.");

            // Add new 
            if (user.UserId == -1)
            {
                UserValidator.AddNewValidator(user);

                try
                {
                    int newUserId = UserRepository.Add(user);
                    if (newUserId != -1)
                    {
                        return UserRepository.GetById(newUserId);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("BLL: Error while adding a new user.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
            else // Update
            {
                UserValidator.UpdateValidator(user);

                try
                {
                    if (UserRepository.Update(user))
                        return UserRepository.GetById(user.UserId);
                    
                    return null;
                }
                catch(Exception ex)
                {
                    AppLogger.LogError($"BLL: Error while updating user with Id: {user.UserId}.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static User GetById(int userId)
        {
            if (userId < 1)
                return null;

            try
            {
                return UserRepository.GetById(userId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static User GetByPersonId(int personId)
        {
            if (personId < 1)
                return null;

            try
            {
                return UserRepository.GetByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user by person id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static User GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            try
            {
                return UserRepository.GetByUsername(username);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user by name.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static string GetPassword(int userId)
        {
            if (userId <= 0)
                return null;

            try
            {
                return UserRepository.GetPassword(userId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user password by user id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool ChangePassword(int userId, string newPassword)
        {
            if (userId < 0 || string.IsNullOrWhiteSpace(newPassword))
                return false;

            try
            {
                return UserRepository.ChangePassword(userId, newPassword);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while changing user password by user id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return UserRepository.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading all users.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAllWithDetails()
        {
            try
            {
                return UserRepository.GetAllWithDetails();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading all users with details.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exists(int userId)
        {
            if (userId < 1)
                return false;

            try
            {
                return UserRepository.Exists(userId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking user existence by Id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool ExistsByPersonId(int personId)
        {
            if (personId < 1)
                return false;

            try
            {
                return UserRepository.ExistsByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking user existence by person Id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool ExistsByUsername(string username, int excludedUserId = -1)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;
            
            try
            {
                return UserRepository.ExistsByUsername(username, excludedUserId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if username is used.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsActive(int userId)
        {
            if (userId < 1)
                return false;
            try
            {
                return UserRepository.IsActive(userId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if user is active.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Update(User user)
        {
            UserValidator.UpdateValidator(user);

            try
            {
                return UserRepository.Update(user);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating user with Id: {user.UserId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteById(int userId)
        {
            if (userId < 1)
                return false;

            try
            {
                return UserRepository.DeleteById(userId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with Id = {userId}.");
                throw new Exception("Cannot delete user because there is data related to it.", ex);
            }
        }
    }
}
