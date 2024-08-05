using DAL.DBContext;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.CarRepository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalContext _context;
        private readonly DbSet<Cars> _dbSet;
        public CarRepository(CarRentalContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Cars>();
        }
        public List<Cars> GetAllCarTypes()
        {
            try
            {
                var query = _dbSet.ToList();
                if (query != null)
                {
                    return query;
                }
                return null;
                
            }catch (Exception ex)
            {
                throw ex; 
            }
        }

        public Cars GetCarByID(int carID)
        {
            try
            {
                var query = _dbSet.FirstOrDefault(car=> car.ID == carID);
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
    }
}
