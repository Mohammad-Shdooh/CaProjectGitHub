using BAL.Interfaces;
using Entities.DTOs;
using Entities.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }


        /// <summary>
        /// Get All Orders  
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Order gotten SuccessFully </response>
        /// <response code="500">Internal Server error</response>
        [HttpGet("GetOrders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                GetOrdersResponse response = new GetOrdersResponse();
                response.orders = ordersService.GetOrders();

                if (response.orders != null && response.orders.Count > 0)
                {
                    response.ErrorCode = ErrorCodes.Success;
                    return Ok(JsonConvert.SerializeObject(response));
                   // return Ok(response);
                }
                response.ErrorCode = ErrorCodes.Success;
                response.Message = "There are no Orders yet .";
                return StatusCode(response.getHttpErrorCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GetOrdersResponse() { Message = "Oops! Something unexpected happened. Please try again later.", ErrorCode = ErrorCodes.InternalException });
            }
        }

        /// <summary>
        /// Add a new Order 
        /// </summary>
        /// <param name="order"></param>
        /// <returns>An ActionResult</returns>
        /// <response code="201">Order Added SuccessFully </response>
        /// <response code="500">Internal Server error</response>
        [HttpPost("AddNewOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddOrderResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public async Task<IActionResult> AddNewOrder([FromBody] AddOrderRequest order)
        {
            try
            {
                AddOrderResponse response = new AddOrderResponse(); 
                response = ordersService.AddNewOrder(order);
                if(response.ErrorCode == ErrorCodes.Success)
                {
                    return Ok(JsonConvert.SerializeObject(response));
                }
                return StatusCode(response.getHttpErrorCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AddOrderResponse() { Message = "Oops! Something unexpected happened. Please try again later.", ErrorCode = ErrorCodes.InternalException });

            }
        }

        /// <summary>
        /// Update The Order 
        /// </summary>
        /// <param name="order"></param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Order Updated SuccessFully </response>
        /// <response code="500">Internal Server error</response>
        [HttpPost("UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateOrderResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]

        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest order)
        {
            try
            {
                UpdateOrderResponse response = new UpdateOrderResponse();
                response = ordersService.UpdateOrder(order);
                if (response.ErrorCode == ErrorCodes.Success)
                {
                    return Ok(JsonConvert.SerializeObject(response));
                }
                return StatusCode(response.getHttpErrorCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AddOrderResponse() { Message = "Oops! Something unexpected happened. Please try again later.", ErrorCode = ErrorCodes.InternalException });

            }
        }

        /// <summary>
        /// Delete The Order 
        /// </summary>
        /// <param name="order"></param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Order Deleted SuccessFully </response>
        /// <response code="500">Internal Server error</response>
        [HttpPost("DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteOrderResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]

        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderRequest order)
        {
            try
            {
                DeleteOrderResponse response = new DeleteOrderResponse();
                response = ordersService.DeleteOrder(order);
                if (response.ErrorCode == ErrorCodes.Success)
                {
                    return Ok(JsonConvert.SerializeObject(response));
                }
                return StatusCode(response.getHttpErrorCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AddOrderResponse() { Message = "Oops! Something unexpected happened. Please try again later.", ErrorCode = ErrorCodes.InternalException });

            }
        }
    }
}
