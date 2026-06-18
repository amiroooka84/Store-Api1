namespace StoreApi.Models.Classes.Payment
{
    public class PaymentVerify
    {
        public string? paidAt { get; set; }
        public int amount { get; set; }
        public int result { get; set; }
        public int status { get; set; }
        public int refNumber { get; set; }
        public string? cardNumber { get; set; }
        public string? orderId { get; set; }
        public string? message { get; set; }


    }


    public class VPOrder
    {
        public string? merchant { get; set; }
        public long trackId { get; set; }
    }
}
