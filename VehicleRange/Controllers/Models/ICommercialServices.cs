using JWTAuthorization.Model;

namespace VehicleRange.Controllers.Models
{
    public interface ICommercialServices
    {
        Task<ListReturnType<Vehicle>> GetVehicleRangeList(string component_type);
    }
}
