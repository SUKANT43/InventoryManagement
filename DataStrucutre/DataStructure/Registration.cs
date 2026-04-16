using System;
using DataStrucutre.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucutre.DataStructure
{
    class Registration
    {
        public string Id { get; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserLevel UserLevel { get; set; }
        public Registration(string empId,string name, string email, string password, UserLevel userLevel)
        {
            Id = userLevel.ToString() + DateTime.Now.ToString();
            EmployeeId = empId;
            Name = name;
            Email = email;
            Password = password;
            UserLevel = userLevel;
        }
    }
}
