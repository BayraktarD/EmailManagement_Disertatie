using System;
using System.Collections.Generic;

namespace EmailMngmntApi.EntityModels
{
    public partial class SentEmail
    {
        public Guid SentEmailId { get; set; }
        public Guid? ClientAppId { get; set; }
        public Guid? UserId { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
