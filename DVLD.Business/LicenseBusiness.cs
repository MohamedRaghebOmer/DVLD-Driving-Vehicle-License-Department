using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Core.Logging;

namespace DVLD.Business
{
    public static class LicenseBusiness
    {
        public static License Save(License license)
        {
            // Issue new license
            if (license.LicenseID == -1)
            {
                EntityValidators.LicenseValidator.AddNewValidator(license);
                
                try
                {
                    Application application = ApplicationData.GetById(license.ApplicationId);
                    ApplicationType applicationType = application.ApplicationTypeID;
                    IssueReason issueReason;
                    int driverId = -1;
                    int newLicenseId = -1;

                    // Determine the issue reason based on the application type
                    switch (applicationType)
                    {
                        case ApplicationType.NewLocalDrivingLicenseService:
                            issueReason = IssueReason.FirstTime;
                            break;
                        case ApplicationType.RenewDrivingLicenseService:
                            issueReason = IssueReason.Renew;
                            break;
                        case ApplicationType.ReplacementForLostDrivingLicense:
                            issueReason = IssueReason.ReplacementLost;
                            break;
                        case ApplicationType.ReplacementForDamagedDrivingLicense:
                            issueReason = IssueReason.ReplacementDamaged;
                            break;
                        default:
                            throw new ValidationException("Invalid application type for issuing a license.");
                    }

                    // Check if the applicant person is already associated with a driver, if not create a new driver record                    
                    driverId = DriverData.IsPersonUsed(application.ApplicantPersonID, -1) ? 
                        DriverData.GetDriverIdByPersonId(application.ApplicantPersonID) : 
                        DriverData.Add(new Driver(application.ApplicantPersonID));

                    // Create the new license and add it to the database
                    if (driverId != -1)
                        newLicenseId = LicenseData.AddNewLicense(license, driverId, DateTime.Now, DateTime.Now.AddYears(LicenseClassData.GetDefaultValidityLength(license.LicenseClass)), issueReason);

                    // Update the application status to Completed.
                    bool isUpdated = ApplicationData.UpdateApplicationStatus(license.ApplicationId, ApplicationStatus.Completed);


                    if (newLicenseId != -1 && driverId != -1 && isUpdated)
                        return LicenseData.GetLicenseById(newLicenseId);
                    return null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("BLL: Error adding new license.");
                    throw new Exception("An error occurred while adding the license. Please try again later.", ex);
                }
            }
            else // Update existing license
            {
                EntityValidators.LicenseValidator.UpdateValidator(license);

                try
                {
                    return LicenseData.UpdateLicense(license) ? LicenseData.GetLicenseById(license.LicenseID) : null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"BLL: Error updating license with ID {license.LicenseID}.", ex);
                    throw new Exception("An error occurred while updating the license. Please try again later.", ex);
                }
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return LicenseData.GetAll();

            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting all licenses.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static License GetById(int licenseId)
        {
            try
            {
                return LicenseData.GetLicenseById(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static DataTable GetDriverLicenses(int driverId)
        {
            if (driverId <= 0)
                throw new ValidationException("Invalid Driver ID. It must be a positive integer.");

            try
            {
                return LicenseData.GetDriverLicenses(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting licenses for DriverID {driverId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool DeactivateLicense(int licenseId)
        {
            if (licenseId <= 0)
                throw new ValidationException("Invalid License ID. It must be a positive integer.");
            try
            {
                return LicenseData.DeactivateLicense(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deactivating license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool ReactivateLicense(int licenseId)
        {
            if (licenseId <= 0)
                throw new ValidationException("Invalid License ID. It must be a positive integer.");
            try
            {
                return LicenseData.ReactivateLicense(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reactivating license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool IsActive(int licenseId)
        {
            if (licenseId <= 0)
                throw new ValidationException("Invalid license id, it must be a positive integer.");

            try
            {
                return LicenseData.IsActive(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if license with ID {licenseId} is active.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool UpdateNotes(int licenseId, string newNotes)
        {
            if (licenseId <= 0)
                return false; // Invalid license ID, cannot update notes

            try
            {
                return LicenseData.UpdateNotes(licenseId, newNotes);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating notes for license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool UpdatePaidFees(int licenseId, decimal newPaidFees)
        {
            if (licenseId <= 0)
                return false; // Invalid license ID, cannot update paid fees

            if (newPaidFees < 0)
                throw new ValidationException("Paid fees cannot be negative.");

            if (LicenseData.GetLicenseById(licenseId).PaidFees == newPaidFees)
                return true; // No update needed, already has the same paid fees

            // Prevent reducing paid fees, as it may lead to inconsistencies in the system
            if (newPaidFees < LicenseData.GetLicenseById(licenseId).PaidFees)
                throw new ValidationException("New paid fees cannot be less than the current paid fees.");

            try
            {
                return LicenseData.UpdatePaidFees(licenseId, newPaidFees);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating paid fees for license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }
    }
}
