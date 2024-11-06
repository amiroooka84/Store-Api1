namespace StoreApi.Models.FieldsRequest.AdminSide.ManageCategory
{
    public class EditCategoryFieldRequest
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public int CategoryId { get; set; }
    }
}
