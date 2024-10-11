namespace Store_Api.Models.FieldsRequest.AccountField
{
    public class EditProfileFieldRequest
    {
        public string ?FirstName { get; set; }
        public string ?LastName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
    }
}
