namespace StoreApi.Models.FieldsRequest.AccountField
{
    public class EditAddressFieldRequest
    {
        public int id { get; set; }
        public string ?Address { get; set; }
        public string ?PostCode { get; set; }
    }
}
