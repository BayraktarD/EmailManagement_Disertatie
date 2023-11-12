namespace EmailMngmntApi.DTOs
{
    public class PasswordHash
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
