using DAL.Entities;
using Entities.DTOs;
namespace BAL.Interfaces
{
    public interface IOrdersService
    {
        // add order 
        // update order 
        //delete order 
        public List<Orders> GetOrders();
        public AddOrderResponse AddNewOrder(AddOrderRequest request);
        public UpdateOrderResponse UpdateOrder(UpdateOrderRequest request);
        public DeleteOrderResponse DeleteOrder(DeleteOrderRequest request);
    }
}
