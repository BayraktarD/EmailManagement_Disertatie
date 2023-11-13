using EmailMngmntApi.DTOs;

namespace EmailMngmntApi.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(UserDTO userDTO, LoginHash passwordHash);
        Task<UserDTO> LoginAsync(Login loginCredentials);
    }
}
