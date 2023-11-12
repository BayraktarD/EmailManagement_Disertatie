using System;
using System.Collections.Generic;

namespace EmailMngmntApi.EntityModels
{
    public partial class ClientApp
    {
        public ClientApp()
        {
            Users = new HashSet<User>();
        }

        public Guid ClientAppId { get; set; }
        public string? AppDisplayName { get; set; }
        public string? AppKey { get; set; }
        public string? SendGridKey { get; set; }
        public string? EmailAddress { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
