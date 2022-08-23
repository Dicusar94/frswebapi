using Bussines.Abstraction.Authorization.Interfaces;
using Bussines.Abstraction.Authorization.Models.Input;
using Bussines.Abstraction.Authorization.Models.Output;
using Bussines.Abstraction.Exceptions;
using Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bussines.Services.Authorization.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthorizationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public LoginDto Login(UserCredentialsDto credentials)
        {
            var token = Token(credentials);

            var user = _userRepository.Find(x => x.Email == credentials.Email)!;

            if (user == null)
                throw new EntityNullReferenceException("User not found !");

            return new LoginDto
            {
                AccessToken = token.AccessToken,
                CurrentTime = token.CurrentTime,
                Expiration = token.Expiration,
                User = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role,
                }
            };
        }

        public TokenDto Token(UserCredentialsDto credentials)
        {
            var user = _userRepository.Find(user =>
                user.Email == credentials.Email &&
                user.Password == credentials.Password);

            if (user == null)
                throw new EntityNullReferenceException("User not found !");

            var tokenHandler = new JwtSecurityTokenHandler();
            var scerectKey = _configuration["AuthOptions:SecretKey"];
            var key = Encoding.ASCII.GetBytes(scerectKey);

            var tokenLifeTimeConfig = _configuration["AuthOptions:TokenLifetimeDays"].ToString();
            bool isValidLifeTime = int.TryParse(tokenLifeTimeConfig, out int tokenLifetime);
            tokenLifetime = isValidLifeTime ? tokenLifetime : 1;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, user.Role!),
                }),
                Issuer = _configuration["AuthOptions:Issuer"],
                Audience = _configuration["AuthOptions:Audience"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(tokenLifetime),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(key), 
                    algorithm : SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDto
            {
                AccessToken = tokenHandler.WriteToken(token),
                Email = user.Email,
                CurrentTime = tokenDescriptor.IssuedAt,
                Expiration = tokenDescriptor.Expires,
            };
        }
    }
}
