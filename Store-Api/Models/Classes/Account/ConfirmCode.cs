namespace Store_Api.Models.Classes.Account
{
    public class ConfirmCode
    {
        public string? PhoneNumber { get; set; }
        public string? Code { get; set; }
        public DateTime? ExpireTime { get; set; }

    }
}
