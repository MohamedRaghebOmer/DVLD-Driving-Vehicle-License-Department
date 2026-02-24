using System;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Core.DTOs.Entities
{
    public class Person
    {
        public int PersonID { get; private set; }

        public string NationalNumber { get; set; }
        
        public string FirstName { get; set; }

        public string SecondName { get; set; }
         
        public string ThirdName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        public Gender Gender { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        
        public int NationalityCountryID { get; set; }

        public string ImagePath { get; set; }


        public Person()
        {
            PersonID = -1;
            NationalNumber = string.Empty;
            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.MinValue;
            Gender = Gender.Male;
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            NationalityCountryID = -1;
            ImagePath = string.Empty;
        }
    
        internal Person(int personId, string nationalNo,  string firstName, string secondName, string thirdName,
                      string lastName, DateTime dateOfBirth, Gender gender, string address, string phone,
                      string email, int nationalityCountryId, string imagePath) : this()
        {
            this.PersonID = personId;
            this.NationalNumber = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryId;
            this.ImagePath = imagePath;
        }
    }
}
