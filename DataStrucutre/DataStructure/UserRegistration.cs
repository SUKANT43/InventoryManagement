using DataStrucutre.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucutre.DataStructure
{
    class UserRegistration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserLevel UserLevel { get; set; }
        public UserRegistration(string name,string email,string password,UserLevel userLevel)
        {
            Name = name;
            Email = email;
            Password = password;
            UserLevel = userLevel;
        }
    }
}
