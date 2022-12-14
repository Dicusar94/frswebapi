using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Infrastructure.Configurations
{
    public class AuthOptions
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int TokenLifetimeDays { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey!));
        }
    }
}
