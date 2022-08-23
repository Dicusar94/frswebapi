using System;

namespace Bussines.Abstraction.Authorization.Models.Output
{
    public class LoginDto
    {
        public string AccessToken { get; set; } = null!;
        public DateTime? CurrentTime { get; set; }
        public DateTime? Expiration { get; set; }
        public UserDto User { get; set; } = null!;
    }
}
