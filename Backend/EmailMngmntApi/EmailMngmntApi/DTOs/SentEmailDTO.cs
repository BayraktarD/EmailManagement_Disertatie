using EmailMngmntApi.EntityModels;
using System.Text.Json.Serialization;

namespace EmailMngmntApi.DTOs
{
    public class SentEmailDTO
    {
        public string SentEmailId { get; set; }
        
        public string UserId { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? AESKey { get; set; }

        public SentEmailDTO() { }
        public SentEmailDTO(SentEmail sentEmail)
        {
            SentEmailId = sentEmail.SentEmailId.ToString(); UserId = sentEmail.UserId.ToString(); From = sentEmail.From; To = sentEmail.To; Title = sentEmail.Title; Content = sentEmail.Content;
        }
    }
}
