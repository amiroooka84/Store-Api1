namespace StoreApi.Models.FieldsRequest.UserSide.Order
{
    public class CreateOrderFieldRequest
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<ProductOrderFieldRequest> Products { get; set; }
    }

    public class ProductOrderFieldRequest
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int Number { get; set; }
    }
}
