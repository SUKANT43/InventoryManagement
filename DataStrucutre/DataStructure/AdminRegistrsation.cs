using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucutre.DataStructure
{
    public class AdminRegistrsation
    {

        public enum RegistrationStatus
        {
            Pending,
            Completed
        }

        public string Name { get; }
        public string Email { get; }
        public string Password { get; }


        public AdminRegistrsation(string name, string email, string password)
        {

        }
    }
}

