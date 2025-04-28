using System.ComponentModel.DataAnnotations;

namespace StoreApi.Models.FieldsRequest.AccountField
{
    public class VerifiFieldRequest
    {

        public string? PhoneNumber { get; set; }
        public string? Code { get; set; }
    }
}
