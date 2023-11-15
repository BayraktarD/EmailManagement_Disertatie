using EmailMngmntApi.DTOs;
using EmailMngmntApi.EntityModels;
using EmailMngmntApi.Interfaces.Repositories;
using EmailMngmntApi.RSA;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace EmailMngmntApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EmailManagementApiContext _db;
        private readonly IRSAHelper _rSAHelper;

        /// <summary>
        /// Address Repository Constructor
        /// </summary>
        public UserRepository(EmailManagementApiContext db, IRSAHelper rSAHelper)
        {
            _db = db;
            _rSAHelper = rSAHelper;
        }

        public async Task<bool> CreateAsync(UserDTO userDTO, LoginHash loginHash)
        {
            bool resutl;
            try
            {
                var user = new User()
                {
                    UserId = Guid.NewGuid(),
                    FirstName =  userDTO.FirstName,
                    LastName = userDTO.LastName,
                    UsernameHash = loginHash.UsernameHash,
                    UsernameSalt = loginHash.UsernameSalt,
                    ClientAppId = null,
                    EmailAddress = userDTO.EmailAddress,
                    PasswordHash = loginHash.PasswordHash,
                    PasswordSalt = loginHash.PasswordSalt
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
                
                var users = await _db.Users.ToListAsync();
                var usersWithSameEmailAddress = users.Where(x => x.EmailAddress == loginCredentials.EmailAddress).ToList();
                if (users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        bool result = ComputePasswordHash(loginCredentials.Password, user.PasswordHash, user.PasswordSalt, loginCredentials.Username, user.UsernameHash, user.UsernameSalt);
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

        private bool ComputePasswordHash(string password, byte[] passwordHash, byte[] passwordSalt, string username, byte[] usernameHash, byte[] usernameSalt)
        {
            bool passwordIsGood = false;
            bool usernameIsGood = false;

            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computePasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                passwordIsGood = computePasswordHash.SequenceEqual(passwordHash);
            }

            using (var hmac = new HMACSHA512(usernameSalt))
            {
                var computeUsernameHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(username));

                usernameIsGood = computeUsernameHash.SequenceEqual(usernameHash);
            }

            return (usernameIsGood == passwordIsGood == true);
        }
    }
}
