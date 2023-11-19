using EmailMngmntApi.DTOs;
using EmailMngmntApi.EntityModels;
using EmailMngmntApi.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmailMngmntApi.Repositories
{
    public class SentEmailsRepository : ISentEmailsRepository
    {
        private readonly EmailManagementApiContext _db;

        public SentEmailsRepository(EmailManagementApiContext db)
        {
            _db = db;
        }

        public async Task<List<SentEmailDTO>> GetUserSentEmailsAsync(Guid userId)
        {
            List<SentEmailDTO> userSentEmails = new List<SentEmailDTO>();
            try
            {
                userSentEmails = await _db.SentEmails.Where(x => x.UserId == userId).Select(x=>new SentEmailDTO(x)).ToListAsync();


            }
            catch (Exception ex)
            {
                return new List<SentEmailDTO>();
            }

            return userSentEmails;
        }

        public async Task<bool> SendEmailAsync(SentEmailDTO sentEmailDTO)
        {
            try
            {
                SentEmail sentEmail = new SentEmail();
                sentEmail.SentEmailId = Guid.NewGuid();
                sentEmail.From = sentEmailDTO.From;
                sentEmail.To = sentEmailDTO.To; 
                sentEmail.Title = sentEmailDTO.Title;
                sentEmail.Content = sentEmailDTO.Content;
                sentEmail.ClientAppId = null;
               Guid.TryParse(sentEmailDTO.UserId,out Guid userId);
                sentEmail.UserId = userId;
                await _db.SentEmails.AddAsync(sentEmail);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteEmailAsync(Guid emailId)
        {
            try
            {
                var email = await _db.SentEmails.FirstOrDefaultAsync(x => x.SentEmailId == emailId);

                _db.SentEmails.Remove(email);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
