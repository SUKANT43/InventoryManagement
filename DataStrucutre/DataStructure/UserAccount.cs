using DataStrucutre.Enums;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DataStrucutre.DataStructure
{
    public class UserAccount
    {
        public UserAccount()
        {
            Id = Guid.NewGuid().ToString("N");
            EmployeeId = string.Empty;
            Name = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            CreatedAt = DateTime.Now;
            IsActive = true;
        }

        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserLevel UserLevel { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public static UserAccount Create(string employeeId, string name, string email, string password, UserLevel userLevel)
        {
            return new UserAccount
            {
                EmployeeId = employeeId,
                Name = name,
                Email = email,
                PasswordHash = HashPassword(password),
                UserLevel = userLevel,
                CreatedAt = DateTime.Now,
                IsActive = true
            };
        }

        public bool VerifyPassword(string password)
        {
            return PasswordHash == HashPassword(password);
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password ?? string.Empty));
                StringBuilder builder = new StringBuilder(bytes.Length * 2);

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
