using BAL.Interfaces;
using DAL.Entities;
using Entities.DTOs;
using Entities.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarService carService;
        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        /// <summary>
        /// Get All Car Types  
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Cars gotten SuccessFully </response>
        /// <response code="500">Internal Server error</response>
        [HttpGet("GetAllCarTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCarsResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetAllCarTypes()
        {
            try
            {
                GetCarsResponse response = new GetCarsResponse();
                response.cars = carService.GetAllCarTypes();

                if (response.cars.Count > 0)
                {
                    response.ErrorCode = ErrorCodes.Success;
                    return Ok(JsonConvert.SerializeObject(response));
                }
                response.ErrorCode = ErrorCodes.Success;
                return StatusCode(response.getHttpErrorCode() ,response );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GetCarsResponse() { Message = "Oops! Something unexpected happened. Please try again later.", ErrorCode = ErrorCodes.InternalException });

            }
        }
    }
}
