using EmailMngmntApi.DTOs;
using EmailMngmntApi.Interfaces.Repositories;
using EmailMngmntApi.Interfaces.Services;
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

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<bool> CreateAsync(UserDTO userDTO)
        {
            var passwordHash = CreatePasswordHash(userDTO.Password);
            return await _userRepository.CreateAsync(userDTO, passwordHash);
        }

        public async Task<string> LoginAsync(Login loginCredentials)
        {
            string jwtToken = string.Empty;
            var userDTO = await _userRepository.LoginAsync(loginCredentials);

            if (userDTO.UserId != Guid.Empty)
            {
                jwtToken = CreateToken(userDTO);
            }

            return jwtToken;
        }

        private PasswordHash CreatePasswordHash(string password)
        {
            PasswordHash hash = new PasswordHash();

            using (var hmac = new HMACSHA512())
            {
                hash.Salt = hmac.Key;
                hash.Hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return hash;
        }

        private string CreateToken(UserDTO userDTO)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,userDTO.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddMinutes(10),
                signingCredentials:creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }



    }
}
