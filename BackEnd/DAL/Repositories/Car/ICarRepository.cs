using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.CarRepository
{
    public interface ICarRepository
    {
        public List<Cars> GetAllCarTypes();
        public Cars GetCarByID(int carID);
    }
}
