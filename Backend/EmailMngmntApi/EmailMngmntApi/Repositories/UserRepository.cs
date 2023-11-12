using EmailMngmntApi.DTOs;
using EmailMngmntApi.EntityModels;
using EmailMngmntApi.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace EmailMngmntApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EmailManagementApiContext _db;

        /// <summary>
        /// Address Repository Constructor
        /// </summary>
        public UserRepository(EmailManagementApiContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(UserDTO userDTO, PasswordHash passwordHash)
        {
            bool resutl;
            try
            {
                var user = new User()
                {
                    UserId = Guid.NewGuid(),
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    UserName = userDTO.UserName,
                    ClientAppId = null,
                    EmailAddress = userDTO.EmailAddress,
                    PasswordHash = passwordHash.Hash,
                    PasswordSalt = passwordHash.Salt
                };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();


                resutl = true;
            }
            catch (Exception ex)
            {
                resutl = false;
            }

            return resutl;
        }

        public async Task<UserDTO> LoginAsync(Login loginCredentials)
        {
            UserDTO userDTO = new UserDTO(); ;
            try
            {
                var users = await _db.Users.Where(x => x.UserName == loginCredentials.Username).ToListAsync();
                if (users.Count > 0)
                {
                    foreach (var user in users)
                    {
                       bool result = ComputePasswordHash(loginCredentials.Password, user.PasswordHash, user.PasswordSalt);
                        if (result == true)
                        {
                            userDTO = new UserDTO(user);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return userDTO;
        }

        private bool ComputePasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
