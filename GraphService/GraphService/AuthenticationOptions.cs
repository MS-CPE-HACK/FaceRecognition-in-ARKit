namespace GraphService
{
    public class AuthenticationOptions
    {
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string SecretWord { get; set; }
    }
}