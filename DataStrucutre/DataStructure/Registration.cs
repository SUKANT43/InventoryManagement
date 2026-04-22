using DataStrucutre.Enums;
using System;

namespace DataStrucutre.DataStructure
{
    public class Registration
    {
        public Registration()
        {
            Id = Guid.NewGuid().ToString("N");
            EmployeeId = string.Empty;
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            UserLevel = UserLevel.User;
        }

        public Registration(string empId, string name, string email, string password, UserLevel userLevel)
            : this()
        {
            Id = userLevel + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            EmployeeId = empId;
            Name = name;
            Email = email;
            Password = password;
            UserLevel = userLevel;
        }

        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserLevel UserLevel { get; set; }
    }
}
