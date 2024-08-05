using BAL.Interfaces;
using DAL.Entities;
using DAL.Repositories.CarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepo;
        public CarService(ICarRepository carRepo)
        {
           this.carRepo = carRepo;
        }
        public List<Cars> GetAllCarTypes()
        {
            try
            {
                return carRepo.GetAllCarTypes();

            }catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
