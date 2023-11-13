using EmailMngmntApi.EntityModels;
using System.Text.Json.Serialization;

namespace EmailMngmntApi.DTOs
{
    public class UserDTO
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public Guid? ClientAppId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }

        public UserDTO()
        {

        }

        public UserDTO(User user)
        {
            UserId = user.UserId;
            ClientAppId = user.ClientAppId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailAddress = user.EmailAddress;
        }

    }
}
