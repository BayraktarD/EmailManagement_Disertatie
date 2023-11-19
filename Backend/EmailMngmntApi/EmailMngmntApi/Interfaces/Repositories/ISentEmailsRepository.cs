using EmailMngmntApi.DTOs;
using EmailMngmntApi.EntityModels;

namespace EmailMngmntApi.Interfaces.Repositories
{
    public interface ISentEmailsRepository
    {
        Task<List<SentEmailDTO>> GetUserSentEmailsAsync(Guid userId);
        Task<bool> SendEmailAsync(SentEmailDTO sentEmail);
        Task<bool> DeleteEmailAsync(Guid emailId);
    }
}
