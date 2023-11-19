using EmailMngmntApi.AES;
using EmailMngmntApi.DTOs;
using EmailMngmntApi.EntityModels;
using EmailMngmntApi.Interfaces.Repositories;
using EmailMngmntApi.Interfaces.Services;
using EmailMngmntApi.RSA;

namespace EmailMngmntApi.Services
{
    public class SentEmailsService : ISentEmailsServices
    {
        private readonly ISentEmailsRepository _sentEmailsRepository;
        private readonly IRSAHelper _rsaHelper;
        private readonly IAESHelper _aesHelper;
        public SentEmailsService(ISentEmailsRepository sentEmailsRepository, IRSAHelper rSAHelper, IAESHelper aESHelper)
        {
            _sentEmailsRepository = sentEmailsRepository;
            _rsaHelper = rSAHelper;
            _aesHelper = aESHelper;
        }

        public async Task<List<SentEmailDTO>> GetUserSentEmailsAsync(Guid userId)
        {
            List<SentEmailDTO> sentEmails = await _sentEmailsRepository.GetUserSentEmailsAsync(userId);

            List<SentEmailDTO> encryptedEmails = new List<SentEmailDTO>();
            try
            {
                foreach (var email in sentEmails)
                {
                    SentEmailDTO encryptedEmail = new SentEmailDTO();

                    var aesKey = _aesHelper.GetKey();

                    encryptedEmail.Title = _aesHelper.Encrypt(email.Title, aesKey);
                    encryptedEmail.Content = _aesHelper.Encrypt(email.Content, aesKey);
                    encryptedEmail.From = _aesHelper.Encrypt(email.From, aesKey);
                    encryptedEmail.To = _aesHelper.Encrypt(email.To, aesKey);
                    encryptedEmail.UserId = _aesHelper.Encrypt(email.UserId, aesKey);
                    encryptedEmail.SentEmailId = _aesHelper.Encrypt(email.SentEmailId, aesKey);

                    encryptedEmail.AESKey = _rsaHelper.Encrypt(aesKey);


                    encryptedEmails.Add(encryptedEmail);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

                return encryptedEmails;
        }

        public async Task<bool> SendEmailAsync(SentEmailDTO sentEmail)
        {
            var aesKey = _rsaHelper.Decrypt(sentEmail.AESKey);

            var decryptedEmail = new SentEmailDTO();
            decryptedEmail.Title = _aesHelper.Decrypt(sentEmail.Title, aesKey);
            decryptedEmail.Content = _aesHelper.Decrypt(sentEmail.Content, aesKey);
            decryptedEmail.From = _aesHelper.Decrypt(sentEmail.From, aesKey);
            decryptedEmail.To = _aesHelper.Decrypt(sentEmail.To, aesKey);
            decryptedEmail.UserId = _aesHelper.Decrypt(sentEmail.UserId, aesKey);

            return await _sentEmailsRepository.SendEmailAsync(decryptedEmail);
        }

        public async Task<bool> DeleteEmailAsync(Guid emailId)
        {
            return await _sentEmailsRepository.DeleteEmailAsync(emailId);
        }
    }
}
