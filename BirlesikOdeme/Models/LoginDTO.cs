namespace BirlesikOdeme.Models
{
    public class LoginDTO
    {
        public string ApiKey { get; set; }
        public string Email { get; set; }
        public string Lang { get; set; }
        public string Password { get; set; }
        public string MerchantId { get; set; }
        public int MemberId { get; set; }
    }
}
