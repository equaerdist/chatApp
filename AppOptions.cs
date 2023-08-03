using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Encodings;
namespace WebApplication5
{
    public class AppOptions
    {
        public string SeqApiKey { get; set; } = string.Empty;
        private string secret_key_auth {get; set;} = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string Issuer {get; set;} = string.Empty;
        public SymmetricSecurityKey Key { get => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key_auth));}
    }
}
