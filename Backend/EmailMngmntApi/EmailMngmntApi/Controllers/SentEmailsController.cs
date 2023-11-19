using EmailMngmntApi.DTOs;
using EmailMngmntApi.EntityModels;
using EmailMngmntApi.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailMngmntApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentEmailsController : ControllerBase
    {
        private readonly ISentEmailsServices _sentEmailsServices;

        public SentEmailsController(ISentEmailsServices sentEmailsServices)
        {
            _sentEmailsServices = sentEmailsServices;
        }

        [Authorize]
        [HttpGet("GetUserSentEmails")]
        public async Task<IActionResult> GetUserSentEmailsAsync([FromQuery]string userId)
        {
            try
            {
                var result = await _sentEmailsServices.GetUserSentEmailsAsync(Guid.Parse(userId));
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailAsync([FromBody]SentEmailDTO sentEmail)
        {
            try
            {
                var result = await _sentEmailsServices.SendEmailAsync(sentEmail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("DeleteEmail")]
        public async Task<IActionResult> DeleteEmailAsync([FromQuery] string emailId)
        {
            try
            {
                var result = await _sentEmailsServices.DeleteEmailAsync(Guid.Parse(emailId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
