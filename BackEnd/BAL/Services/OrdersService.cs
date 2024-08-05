using BAL.Interfaces;
using DAL.Entities;
using DAL.Repositories.CarRepository;
using DAL.Repositories.Orders;
using Entities.DTOs;
using Entities.Tools;

namespace BAL.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository ordersRepo;
        private readonly ICarRepository carRepo;
        public OrdersService(IOrdersRepository ordersRepo, ICarRepository carRepo)
        {
            this.ordersRepo = ordersRepo;
            this.carRepo = carRepo;
        }

        public AddOrderResponse AddNewOrder(AddOrderRequest request)
        {
            try
            {
                AddOrderResponse response = new AddOrderResponse();
                Cars car = new Cars(); 
                Orders order = new Orders();

                // Check if this car type exist in Cars table before adding the order . (Double check) because we will preview the cars in the UI From Cars table .
                car = carRepo.GetCarByID(request.CarID);
                if(car == null)
                {
                    // if doesn't found we will not insert the order .
                    response.ErrorCode = ErrorCodes.Faild;
                    response.Message = "We Don't Have This Car Type .";
                    
                    return response;
                }else
                {
                    // set DB order instance to insert into DB .
                    order.UserName = request.UserName;
                    order.MobileNumber = request.MobileNumber;
                    order.Comments = request.Comments;
                    order.From = request.From;
                    order.To = request.To;
                    order.CarID = car.ID;

                    int orderID = ordersRepo.AddNewOrder(order).Result;
                    if(orderID > 0 )
                    {
                        response.Order.ID = orderID;
                        response.Order = order;
                        response.Order.Car = new Cars { ID = car.ID, Name = car.Name };
                        response.ErrorCode = ErrorCodes.Success;
                        response.Message = "Order Added Successfully .";
                        return response;
                    }

                    response.ErrorCode = ErrorCodes.Faild;
                    response.Message = "Can not Add Order .Try again .";
                    return response;
                }

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public DeleteOrderResponse DeleteOrder(DeleteOrderRequest request)
        {
            try
            {
               DeleteOrderResponse response = new DeleteOrderResponse();
                bool result = ordersRepo.DeleteOrder(request.OrderID);
                if (result)
                {
                    response.ErrorCode= ErrorCodes.Success;
                    response.Message = "The Order Deleted Successfully . ";
                    return response;
                }
                response.ErrorCode = ErrorCodes.Faild;
                response.Message = "The Order Not Found . ";
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Orders> GetOrders()
        {
            try
            {
                return ordersRepo.GetOrders();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UpdateOrderResponse UpdateOrder(UpdateOrderRequest request)
        {
            try
            {
                UpdateOrderResponse response = new UpdateOrderResponse();
                Cars car = new Cars();
                Orders order = new Orders();

                // also , i am hitting the Car table here for Double check .to handle as much as possible of exceptions .
                car = carRepo.GetCarByID(request.CarID);
                if (car == null)
                {
                    // if doesn't found we will not insert the order .
                    response.ErrorCode = ErrorCodes.Faild;
                    response.Message = "We Don't Have This Type Of Cars .";

                    return response;
                }
                else
                {
                    // set DB order instance to insert into DB .
                    order.ID = request.ID;
                    order.UserName = request.UserName;
                    order.MobileNumber = request.MobileNumber;
                    order.Comments = request.Comments;
                    order.From = request.From;
                    order.To = request.To;
                    order.CarID = car.ID;

                    var updatedOrder = ordersRepo.UpdateOrder(order);
                    if (updatedOrder == null)
                    {
                        response.ErrorCode = ErrorCodes.Faild;
                        response.Message = "The Order With Number : " +order.ID + " doesn't exist .";
                        return response;  
                    }
                      response.ErrorCode = ErrorCodes.Success;
                      response.order = updatedOrder;
                      response.order.Car = new Cars { ID = car.ID, Name = car.Name };
                      return response;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
