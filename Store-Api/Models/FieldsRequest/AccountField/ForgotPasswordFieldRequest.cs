namespace StoreApi.Models.FieldsRequest.AccountField
{
    public class ForgotPasswordFieldRequest
    {

        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? Code { get; set; }
        public string? ConfirmCode { get; set; }

    }
}
