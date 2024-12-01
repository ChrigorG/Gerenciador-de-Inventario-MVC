using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Shared.Helper
{
    public static class Util
    {
        public static bool IsNullOrZero(int? value)
        {
            return !value.HasValue || value.Value == 0;
        }

        public static bool IsNullOrDefault(DateTime? date)
        {
            return !date.HasValue || date.Value == default(DateTime);
        }

        public static bool IsNullOrDefault(DateTimeOffset? date)
        {
            return !date.HasValue || date.Value == default(DateTimeOffset);
        }

        public static string DateHoursMinutes(DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("dd/MM/yyyy HH:mm") : TripleTrace();
        }

        public static int? OnlyNumbers(string value)
        {
            string result = Regex.Replace(value, "[^0-9]", "");

            if (!string.IsNullOrEmpty(result) && int.TryParse(result, out int number))
            {
                return number;
            }

            return null;
        }

        public static string GeneratePassword(DateTime birthday, string firstName)
        {
            string password = string.Empty;
                        
            if(firstName.Length >= 2)
            {
                password = firstName.ToLower().Substring(0, 2);
            }

            password += birthday.ToString("ddMM");

            return HashPassword(password);
        }

        // Método para criar uma senha com hash usando SHA256 que é mais seguro que o MD5
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", "ç@t1", password);
                // ComputeHash - retorna byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static string TripleTrace()
        {
            return "---";
        }
    }
}
