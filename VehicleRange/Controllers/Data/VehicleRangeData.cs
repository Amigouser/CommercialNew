using Microsoft.AspNetCore.Mvc;
using VehicleRange.Controllers.BusinessLogic;
using VehicleRange.Controllers.Models;

namespace ComercialTest.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleRangeData : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult>GetVehicleRangeData(string component_type)
        {
           ICommercialServices _commercialServices = new CommercialServices();
            var GetData = _commercialServices.GetVehicleRangeList(component_type);
            return Ok(await GetData);
           
        }

    }
}
