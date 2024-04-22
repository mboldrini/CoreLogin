using System.Security.Cryptography;
using System.Text;

namespace CoreLogin_Infrastructure.Helpers
{
  public class PasswordHelper
  {
    public static string CriptographPassword(string password)
    {
      using (SHA256 sha256Hash = SHA256.Create())
      {
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
          builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
      }
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
      string hashedInput = CriptographPassword(password);

      return string.Equals(hashedInput, hashedPassword);
    }
  }
}
