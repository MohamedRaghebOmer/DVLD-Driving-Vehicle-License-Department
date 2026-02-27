using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Core.Helpers;
using DVLD.Core.Logging;
using DVLD.Data;
using System;
using System.Data;
using System.IO;

namespace DVLD.Business
{
    public static class PersonService
    {
        public static int Add(Person person)
        {
            if (person == null)
                throw new ValidationException("Person info cannot be empty.");

            try
            {
                return PersonRepository.Add(person);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while saving new person.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Person GetById(int personId)
        {
            if (personId < 1)
                return null;

            try
            {
                return PersonRepository.GetById(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading person with Id = {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Person GetByNationalNo(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
                return null;

            try
            {
                return PersonRepository.GetByNationalNo(nationalNo);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading person with national number = {nationalNo}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static int GetIdByNationalNo(string NationalNo)
        {
            if (string.IsNullOrEmpty(NationalNo))
                return -1;

            try
            {
                return PersonRepository.GetIdByNationalNo(NationalNo);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading person id with national number = {NationalNo}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static string GetImagePathByPersonId(int personId)
        {
            if (personId < 1) return null;

            string fileName = PersonRepository.GetImagePathByPersonId(personId);

            if (fileName == null)
                return null;

            return Path.Combine(PathHelper.ImagesFolderPath, fileName);
        }

        public static string GetImagePathByNationalNo(string nationalNo)
        {
            if (string.IsNullOrEmpty(nationalNo))
                return null;

            string fileName = PersonRepository.GetImagePathByNationalNo(nationalNo);

            if (fileName == null)
                return null;

            return Path.Combine(PathHelper.ImagesFolderPath, fileName);
        }

        public static string GetImagePathByFileName(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                return null;

            return Path.Combine(PathHelper.ImagesFolderPath, imageFileName);
        }

        public static DataTable GetAllWithDateParts()
        {
            try
            {
                return PersonRepository.GetAllWithDateParts();
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading all people with date parts.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exists(int personId)
        {
            if (personId < 1)
                return false;

            try
            {
                return PersonRepository.Exists(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if person with Id = {personId} exists.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exists(string nationalNumber, int excludePersonId = -1)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber))
                return false;
            try
            {
                return PersonRepository.Exists(nationalNumber, excludePersonId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if national number = {nationalNumber} is used.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsEmailUsed(string email, int excludePersonId = -1)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                return PersonRepository.IsEmailUsed(email, excludePersonId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if email = {email} is used by other.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsPhoneUsed(string phone, int excludePersonId = -1)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            try
            {
                return PersonRepository.IsPhoneUsed(phone, excludePersonId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if phone = {phone} is used by other.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Update(Person person)
        {
            if (person == null)
                throw new ValidationException("Person info cannot be empty.");

            try
            {
                return PersonRepository.Update(person);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating person with Id = {person.PersonID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(int personId)
        {
            if (personId < 1)
                return false;

            string imagePath = GetImagePathByPersonId(personId);

            bool isDeleted = PersonRepository.Delete(personId);

            if (!isDeleted)
                return false;

            try
            {
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    File.Delete(imagePath);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting person with Id = {personId}.");
                throw new Exception("Can't delete person because there is data associated with it.", ex);
            }

            return true;
        }
    }
}
