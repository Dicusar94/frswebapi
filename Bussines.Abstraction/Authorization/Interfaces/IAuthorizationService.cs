using Bussines.Abstraction.Authorization.Models.Input;
using Bussines.Abstraction.Authorization.Models.Output;

namespace Bussines.Abstraction.Authorization.Interfaces
{
    public interface IAuthorizationService
    {
        TokenDto Token(UserCredentialsDto singInCredentials);
        LoginDto Login(UserCredentialsDto credentials);
    }
}
