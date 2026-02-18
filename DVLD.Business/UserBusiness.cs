using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public class UserBusiness
    {
        public static bool Login(string username, string password)
        {
            // Check username existance.
            if (!UserData.Exists(username))
                return false;

            // Check if username is not active.
            if (!UserData.IsActive(username))
                throw new BusinessException("Your account was deactivated. Contact your admin to activate you.");

            // Check if the password is not correct.
            if (!UserData.IsCorrectUsernameAndPassword(username, password, true))
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
                    int newUserId = UserData.Add(user);
                    if (newUserId != -1)
                    {
                        return UserData.GetById(newUserId);
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
                    if (UserData.Update(user))
                        return UserData.GetById(user.UserId);
                    
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
                return UserData.GetById(userId);
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
                return UserData.GetByPersonId(personId);
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
                return UserData.GetByUsername(username);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user by name.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static DataTable GetAll()
        {
            try
            {
                return UserData.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading all users.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exists(int userId)
        {
            if (userId < 1)
                return false;

            try
            {
                return UserData.Exists(userId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking user existence by Id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool IsActive(int userId)
        {
            if (userId < 1)
                return false;
            try
            {
                return UserData.IsActive(userId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if user is active.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsUsernameUsed(string username, int excludedUserId = -1)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;
            try
            {
                return UserData.ExistsForUsername(username, excludedUserId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if username is used.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsPersonUsed(int personId, int excludedUserId = -1)
        {
            if (personId < 1)
                return false;
            try
            {
                return UserData.ExistsForPerson(personId, excludedUserId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if person is used.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteById(int userId)
        {
            if (userId < 1)
                return false;

            try
            {
                return UserData.DeleteById(userId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with Id = {userId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByPersonId(int personId)
        {
            if (personId < 1)
                return false;

            try
            {
                return UserData.DeleteByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with person Id = {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool DeleteByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            try
            {
                return UserData.DeleteByUsername(username);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with username = {username}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }
    }
}
