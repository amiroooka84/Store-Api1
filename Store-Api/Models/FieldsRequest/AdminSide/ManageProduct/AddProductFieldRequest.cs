using DocumentFormat.OpenXml;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using StoreApi.Models.FieldsRequest.IDField;
using System.Text.Json.Serialization;
using System.Web.Mvc;

namespace StoreApi.Models.FieldsRequest.AdminSide.ManageProduct
{
    public class AddProductFieldRequest
    {
        
        public string? Name { get; set; }
        public string? Slack { get; set; }
        public int Code { get; set; }
        public string? Brand { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        [AllowHtml]
        public string? Description { get; set; }
        [AllowHtml]
        public string? specs { get; set; }
        public int CategoryId { get; set; }
        public List<ProductColorFieldRequest> Colors { get; set; }
        public List<ProductTagFieldRequest> Tags { get; set; }
        public string? ImagePath { get; set; }
        public string? Image3DPath { get; set; }

        public List<ImagePathFieldRequest> ImagesPath { get; set; }

    }
}
