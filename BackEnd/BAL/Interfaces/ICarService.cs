using DAL.Entities;

namespace BAL.Interfaces
{
    public interface ICarService
    {
        public List<Cars> GetAllCarTypes();
    }
}
