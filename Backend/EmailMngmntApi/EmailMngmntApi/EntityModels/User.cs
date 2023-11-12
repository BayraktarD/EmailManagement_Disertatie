using System;
using System.Collections.Generic;

namespace EmailMngmntApi.EntityModels
{
    public partial class User
    {
        public Guid UserId { get; set; }
        public Guid? ClientAppId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        public virtual ClientApp? ClientApp { get; set; }
    }
}
