using System;

namespace Bussines.Abstraction.Authorization.Models.Output
{
    public class TokenDto
    {
        public string AccessToken { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? Expiration { get; set; }
        public DateTime? CurrentTime { get; set; }
    }
}
