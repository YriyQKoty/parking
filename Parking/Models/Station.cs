using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parking.Models
{
    public class Station
    {
        public Station()
        {
            ParkingDetails = new HashSet<ParkingDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int PlacesCount { get; set; }
        public int? CityId { get; set; }
        public int CarsCount { get; set; }
        public decimal? PricePerHour { get; set; }

        
        [JsonIgnore]
        public virtual City? City { get; set; }
        [JsonIgnore]
        public virtual ICollection<ParkingDetail> ParkingDetails { get; set; }
    }
}
