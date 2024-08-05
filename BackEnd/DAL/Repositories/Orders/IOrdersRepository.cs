
namespace DAL.Repositories.Orders
{
    public interface IOrdersRepository
    {
        public List<Entities.Orders> GetOrders();
        public Task<int> AddNewOrder(Entities.Orders request);
        public Entities.Orders UpdateOrder(Entities.Orders request);
        public bool DeleteOrder(int id);

    }
}
