using EmailMngmntApi.DTOs;

namespace EmailMngmntApi.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(UserDTO userDTO, PasswordHash passwordHash);
        Task<UserDTO> LoginAsync(Login loginCredentials);
    }
}
