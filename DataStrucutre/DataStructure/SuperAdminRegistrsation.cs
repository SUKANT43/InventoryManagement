using DataStrucutre.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucutre.DataStructure
{
    public class SuperAdminRegistrsation
    {
        public string Id { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RegistrationStatus Status { get; set; }
        public UserLevel UserLevel { get; }
        public SuperAdminRegistrsation(string name, string email, string password)
        {
            Id = UserLevel.ToString() + DateTime.Now.ToString();
            Name = name;
            Email = email;
            Password = password;
            Status = RegistrationStatus.Pending;
            UserLevel = UserLevel.SuperAdmin;
        }
    }
}

