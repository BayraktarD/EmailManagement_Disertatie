using EmailMngmntApi.DTOs;

namespace EmailMngmntApi.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> CreateAsync(UserDTO userDTO);
        Task<string> LoginAsync(Login loginCredentials);
    }
}
