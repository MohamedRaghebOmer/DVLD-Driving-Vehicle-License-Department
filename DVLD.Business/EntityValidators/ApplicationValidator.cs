using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Data;
using System;

namespace DVLD.Business.EntityValidators
{
    internal static class ApplicationValidator
    {
        private static bool IsAgeAllowed(DateTime dateOfBirth, int minAge)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;

            if (DateTime.Now.Date < dateOfBirth.AddYears(age))
                age--;

            return age >= minAge;
        }

        public static void AddNewValidator(Application application, LicenseClass licenseClass)
        {
            Core.Validators.ApplicationValidator.Validate(application);

            // Check if the person exists.
            if (!PersonRepository.Exists(application.ApplicantPersonID))
                throw new BusinessException("Applicant person does not exist.");


            Person person = PersonRepository.GetById(application.ApplicantPersonID);
            int minAllowedAge = LicenseClassRepository.GetMinimumAllowedAge(licenseClass);

            // Check if the person is older than the minimum allowed age.
            if (!IsAgeAllowed(person.DateOfBirth, minAllowedAge))
                throw new BusinessException($"The applicant must be at least {minAllowedAge} years old.");

            // Check if the person already has an application with the same type.
            if (LocalDrivingLicenseApplicationRepository.ExistsByPerson(application.ApplicantPersonID, licenseClass, application.ApplicationTypeID, ApplicationStatus.New))
                throw new BusinessException("There is already an uncompleted application of the same type.");

            bool doesApplicantHaveLicense = LicenseRepository.ExistsByDriver(DriverRepository.GetDriverIdByPersonId(application.ApplicantPersonID), licenseClass);
            if (application.ApplicationTypeID == ApplicationType.NewLocalDrivingLicenseService)
            {
                if (doesApplicantHaveLicense)
                    throw new BusinessException("The applicant already has a driving license from this class.");
            }
            else if (application.ApplicationTypeID != ApplicationType.RetakeTest) // Renew, Replace, Release
            {
                if (!doesApplicantHaveLicense)
                    throw new BusinessException("The applicant does not have a driving license from this class.");

                if (!LicenseRepository.IsActive(application.ApplicantPersonID, licenseClass))
                    throw new BusinessException("The license is not active.");

                if (LicenseRepository.IsExpired(application.ApplicantPersonID, licenseClass))
                    throw new BusinessException("The license is expired.");

                if (DetainedLicenseRepository.IsDetained(application.ApplicantPersonID, licenseClass)
                    && application.ApplicationTypeID != ApplicationType.ReleaseDetainedDrivingLicense)
                {
                    throw new BusinessException("The license is detained.");
                }
            }
        }

        //public static void NewInternationalLicenseValidator(Application application)
        //{
        //    Core.Validators.ApplicationValidator.Validate(application);

        //    // Check if the person exists.
        //    if (!PersonRepository.Exists(application.ApplicantPersonID))
        //        throw new BusinessException("Applicant person does not exist.");

        //    // Check if the person already has an application with the same type.
        //    if (ApplicationRepository.ExistsForPerson(application.ApplicantPersonID, ApplicationType.NewInternationalLicense, ApplicationStatus.New))
        //        throw new BusinessException("There is already an uncompleted application of the same type.");

        //    if (ApplicationRepository.ExistsForPerson(application.ApplicantPersonID, ApplicationType.NewLocalDrivingLicenseService, ApplicationStatus.Completed))
        //        throw new BusinessException("Person already has a completed local driving license application.");

        //    // Check if the ApplicationFees is not payed completely.
        //    if (application.PaidFees < ApplicationTypeRepository.GetFees(ApplicationType.NewInternationalLicense))
        //        throw new BusinessException("Application fees are not payed completely.");

        //    int driverId = DriverRepository.GetIdByPersonId(application.ApplicantPersonID);
        //    if (!LicenseRepository.ExistsForDriver(driverId, LicenseClass.Class3_OrdinaryDrivingLicense, true))
        //        throw new BusinessException("The applicant does not have an active class 3 driving license.");
        //}
    }
}
