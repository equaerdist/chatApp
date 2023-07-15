namespace WebApplication5.Services.Passwords
{
    public class BcryptPasswordHandler : IPasswordHandler
    {
        public string GetHash(string password)
        {
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashPassword;
        }

        public bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}