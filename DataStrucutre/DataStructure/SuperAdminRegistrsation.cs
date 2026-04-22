using DataStrucutre.Enums;
using System;

namespace DataStrucutre.DataStructure
{
    public class SuperAdminRegistrsation
    {
        public SuperAdminRegistrsation()
        {
            Id = "SuperAdmin-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Status = RegistrationStatus.Pending;
            UserLevel = UserLevel.SuperAdmin;
        }

        public SuperAdminRegistrsation(string name, string email, string password)
            : this()
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RegistrationStatus Status { get; set; }
        public UserLevel UserLevel { get; set; }
    }
}
