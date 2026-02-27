using DVLD.Core.Exceptions;
using System;
using System.Runtime.InteropServices;

namespace DVLD.Core.DTOs.Entities
{
    public class User
    {
        public int UserId { get; private set; }

        public int PersonId { get; set; }
       
        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }      

        public User()
        {
            this.UserId = -1;
            this.PersonId = -1;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;
        }

        internal User(int userId, int personId, string username, string password, bool isActive) : this()
        {
            this.UserId = userId;
            this.PersonId = personId;
            this.Username = username;
            this.Password = password;
            this.IsActive = isActive;
        }
    }
}
