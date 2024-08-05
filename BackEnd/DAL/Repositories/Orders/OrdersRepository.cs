using DAL.DBContext;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly CarRentalContext _context;
        private readonly DbSet<Entities.Orders> _dbSet;

        public OrdersRepository(CarRentalContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Entities.Orders>();
        }

        public async Task<int> AddNewOrder(Entities.Orders order)
        {
            try
            {
                var query = _dbSet.Add(order); 
                await _context.SaveChangesAsync();
                
                return order.ID;

            }catch (Exception ex)
            {
                throw ex; 
            }
        }

        public bool DeleteOrder(int id)
        {
            try
            {
                var order = _dbSet.SingleOrDefault(odr => odr.ID == id);
                if (order != null )
                {
                    _dbSet.Remove(order);
                    _context.SaveChanges(); 
                    return true;
                }
                return false;

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entities.Orders> GetOrders()
        {
            try
            {
                // i was fetching the records with join to select the car data for each order .
                // to preview each order as one unit in the Front .
                // and in this way we don't need to hit a car table in new connection .
                // Note : i don't make it pagination because i know it's a test project and will not return a big data .
                var query = _dbSet
                .Join(
                    _context.Cars,
                     order => order.CarID,
                     car => car.ID,
                     (order, car) => new Entities.Orders
                     {
                        ID = order.ID,
                        UserName = order.UserName,
                        MobileNumber = order.MobileNumber,
                        Comments = order.Comments,
                        From = order.From,
                        To = order.To,
                        CarID = car.ID,
                        Car = new Cars {  ID = car.ID , Name = car.Name}
                     }
                   ).ToList();

                if (query != null)
                {
                    return query;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entities.Orders UpdateOrder(Entities.Orders newOrder)
        {
            try
            {
                var currentOrder = _dbSet.FirstOrDefault(order => order.ID == newOrder.ID);
                if (currentOrder != null)
                {
                    //update record data with the new data .
                    currentOrder.UserName = newOrder.UserName;
                    currentOrder.MobileNumber = newOrder.MobileNumber;
                    currentOrder.Comments = newOrder.Comments;
                    currentOrder.From = newOrder.From;
                    currentOrder.To = newOrder.To;
                    currentOrder.CarID = newOrder.CarID;

                    _context.SaveChanges();
                    
                }
                return currentOrder;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
