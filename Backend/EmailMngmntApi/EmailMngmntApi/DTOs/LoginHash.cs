namespace EmailMngmntApi.DTOs
{
    public class LoginHash
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] UsernameHash { get; set; }
        public byte[] UsernameSalt { get; set; }
    }
}
