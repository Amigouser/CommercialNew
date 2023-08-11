using JWTAuthorization.Helper;
using JWTAuthorization.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using System.Text;
using VehicleRange.Controllers.Models;

namespace VehicleRange.Controllers.BusinessLogic
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CommercialServices : ControllerBase, ICommercialServices
    {
        [HttpGet]
        [Route("api/GetVehicles")]
        public async Task<ListReturnType<Vehicle>> GetVehicleRangeList(string component_type)
        {
            ListReturnType<Vehicle> vehicleRange = new ListReturnType<Vehicle>();

            #region Token Validating //Validate Token
            string? authorization = HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorization))
            {
                ServiceHeaderInfo headerInfo = JwtHelper.AuthenticateJWTToken(authorization);

                if (!headerInfo.IsAuthenticated)
                {
                    vehicleRange.code = (int)ServiceMassageCode.UNAUTHORIZED_REQUEST;
                    vehicleRange.message = Convert.ToString(ServiceMassageCode.ERROR) + " - Token is invailid.";
                    vehicleRange.result = null;
                    return vehicleRange;
                }
            }
            #endregion
            Vehicle Getvehicle = null;
            List<Vehicle> GetvehicleList = new List<Vehicle>();

            List<VehicleDetails>? vehicleDetailList = null;
            VehicleDetails vehicleDetails = new VehicleDetails();

            string URL = "https://run.mocky.io/v3/a83f3480-c8d1-49e8-8ab4-63ec0c87cafd";
            var res = APIHelper(URL, "");
            VehicleRanges? GetData = System.Text.Json.JsonSerializer.Deserialize<VehicleRanges>(res);

            foreach (var item in GetData.result)
            {
                if (item.vehicleType == "Goods_Carrier")
                {
                    Getvehicle = new Vehicle()
                    {
                        sequence = item.sequence,
                        vehicleType = item.vehicleType
                    };
                    vehicleDetailList = new List<VehicleDetails>();
                    foreach (var veh_details in item.vehicles)
                    {
                        vehicleDetails = new VehicleDetails()
                        {
                            sequence = veh_details.sequence,
                            brand_name = veh_details.brand_name,
                            vehicle_page_link = veh_details.vehicle_page_link,
                            starting_price = veh_details.starting_price,
                            engine = veh_details.engine,
                            fuel_type = veh_details.fuel_type,
                            image = veh_details.image,
                            _360_image = veh_details._360_image,
                        };
                        vehicleDetailList.Add(vehicleDetails);
                    }
                }
                else
                {
                    Getvehicle = new Vehicle()
                    {
                        sequence = item.sequence,
                        vehicleType = item.vehicleType
                    };
                    vehicleDetailList = new List<VehicleDetails>();
                    foreach (var veh_details in item.vehicles)
                    {
                        vehicleDetails = new VehicleDetails()
                        {
                            sequence = veh_details.sequence,
                            brand_name = veh_details.brand_name,
                            vehicle_page_link = veh_details.vehicle_page_link,
                            starting_price = veh_details.starting_price,
                            engine = veh_details.engine,
                            fuel_type = veh_details.fuel_type,
                            image = veh_details.image,
                            _360_image = veh_details._360_image,
                        };
                        vehicleDetailList.Add(vehicleDetails);
                    }
                }
                Getvehicle.vehicles = vehicleDetailList;

                GetvehicleList.Add(Getvehicle);
            }
            vehicleRange.code = (int)ServiceMassageCode.SUCCESS;
            vehicleRange.message = Convert.ToString(ServiceMassageCode.SUCCESS);
            vehicleRange.result = GetvehicleList;
            return vehicleRange;
        }
        public static string APIHelper(string url, object requestObj)
        {
            string jsonResponse = string.Empty;
            Stream stream = null;
            Stream newstream = null;
            try
            {
                if (!ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12))
                {
                    ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12;
                }

                Uri uri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Headers.Add("token", "dR$545#h^jjmanJ");
                request.ContentType = "application/json";
                request.Method = "POST";
                if (requestObj != null)
                {
                    string sb = JsonConvert.SerializeObject(requestObj);
                    byte[] data = Encoding.ASCII.GetBytes(sb);
                    stream = request.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    response.Headers.Add("Authorization", "abcd");
                    newstream = response.GetResponseStream();
                    if (newstream != null)
                    {
                        StreamReader sr = new StreamReader(newstream);
                        jsonResponse = sr.ReadToEnd();
                        string errorResponse = "\"p_err_cd\":\"1\"";
                        if (jsonResponse.Contains(errorResponse))
                        {
                            return null;
                        }
                        sr.Close();
                    }
                }
            }
            catch (Exception)
            {
                ////catch
            }
            finally
            {
                if (newstream != null)
                {
                    newstream.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return jsonResponse;
        }

    }
}
