namespace WebApplication5.Services.Passwords 
{
    public interface IPasswordHandler 
    {
        string GetHash(string password);
        bool Verify(string password, string hashedPassword);
    }
}