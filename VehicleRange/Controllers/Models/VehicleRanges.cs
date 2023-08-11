using Newtonsoft.Json;

namespace VehicleRange.Controllers.Models
{
    public class VehicleRanges
    {
        public string code { get; set; }
        public string message { get; set; }
        public List<Vehicle> result { get; set; }
    }
    public class Vehicle
    {
        public string sequence { get; set; }
        public string vehicleType { get; set; }
        public List<VehicleDetails> vehicles { get; set; }
    }
    public class VehicleDetails
    {
        public string sequence { get; set; }
        public string brand_name { get; set; }
        public string vehicle_page_link { get; set; }
        public string starting_price { get; set; }
        public string engine { get; set; }
        public string fuel_type { get; set; }
        public string image { get; set; }

        [JsonProperty("360_image")]
        public List<string> _360_image { get; set; }
    }




}
