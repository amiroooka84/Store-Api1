using StoreApi.Entity._Order;

namespace StoreApi.Models.FieldsRequest.AdminSide.ManageOrder
{
    public class ChangeOrderStateFieldRequest
    {
        public int orderId { get; set; }
        public int orderState { get; set; }
         
    }
}
