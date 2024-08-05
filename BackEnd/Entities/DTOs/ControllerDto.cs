using DAL.Entities;
using Entities.Tools;

namespace Entities.DTOs
{
    public class BaseResponse
    {
        public string Message { get; set; } = "Success";
        public ErrorCodes ErrorCode { get; set; } = ErrorCodes.InternalException;
        public bool Result => ErrorCode == ErrorCodes.Success;
    }

    public class GetCarsResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {

                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        } 
        public List<Cars> cars = new List<Cars>();
    }

    public class GetOrdersResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {

                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
        public List<Orders> orders = new List<Orders>();

    }
    public class AddOrderRequest
    {
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string Comments { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int CarID { get; set; }
    }
    public class AddOrderResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {

                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.Faild:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
        
        public Orders Order = new Orders();
    }

    public class UpdateOrderRequest
    {
        public int ID { get; set; } 
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string Comments { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int CarID { get; set; }
    }
    public class UpdateOrderResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {

                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.Faild:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }

        public Orders order = new Orders();
    }
    public class DeleteOrderRequest
    {
        public int OrderID { get; set; }
    }
    public class DeleteOrderResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {

                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.Faild:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
     }
}
