using EmailMngmntApi.DTOs;
using EmailMngmntApi.Interfaces.Repositories;
using EmailMngmntApi.Interfaces.Services;
using EmailMngmntApi.RSA;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EmailMngmntApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IRSAHelper _rSAHelper;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IRSAHelper rSAHelper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _rSAHelper = rSAHelper;
        }

        public async Task<bool> CreateAsync(UserDTO userDTO)
        {
            UserDTO decryptedUserData = new UserDTO();

            decryptedUserData.UserName = _rSAHelper.Decrypt(userDTO.UserName);
            decryptedUserData.Password = _rSAHelper.Decrypt(userDTO.Password);
            decryptedUserData.FirstName = _rSAHelper.Decrypt(userDTO.FirstName);
            decryptedUserData.LastName = _rSAHelper.Decrypt(userDTO.LastName);
            decryptedUserData.EmailAddress = _rSAHelper.Decrypt(userDTO.EmailAddress);


            Login login = new Login()
            {
                Username = decryptedUserData.UserName, Password = decryptedUserData.Password
            };

            var loginHash = CreateLoginHash(login);
            return await _userRepository.CreateAsync(decryptedUserData, loginHash);
        }

        public async Task<string> LoginAsync(Login loginCredentials)
        {
            string jwtToken = string.Empty;
            Login decryptedLoginCredentials = new Login();
            decryptedLoginCredentials.Username = _rSAHelper.Decrypt(loginCredentials.Username);
            decryptedLoginCredentials.Password = _rSAHelper.Decrypt(loginCredentials.Password);
            decryptedLoginCredentials.EmailAddress = _rSAHelper.Decrypt(loginCredentials.EmailAddress);



            var userDTO = await _userRepository.LoginAsync(decryptedLoginCredentials);

            if (userDTO.UserId != Guid.Empty)
            {
                jwtToken = CreateToken(userDTO);
            }


            return jwtToken;
        }

        private LoginHash CreateLoginHash(Login login)
        {
            LoginHash hash = new LoginHash();

            using (var hmac = new HMACSHA512())
            {
                hash.PasswordSalt = hmac.Key;
                hash.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(login.Password));
            }

            using (var hmac = new HMACSHA512())
            {
                hash.UsernameSalt = hmac.Key;
                hash.UsernameHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(login.Username));
            }

            return hash;
        }

        private string CreateToken(UserDTO userDTO)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,userDTO.FirstName+" "+userDTO.LastName),
                new Claim(ClaimTypes.Role,"ROLE_ADMIN"),
                new Claim(ClaimTypes.UserData,userDTO.UserId.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(100),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }



    }
}
