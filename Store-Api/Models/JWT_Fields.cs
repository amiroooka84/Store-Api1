namespace StoreApi.Models
{
    [Serializable]
    public class JWT_Fields
    {
        public string? Token { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime Expire_Time { get; set; }
    }
}
