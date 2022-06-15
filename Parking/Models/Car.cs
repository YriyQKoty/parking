using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parking.Models
{
    public class Car
    {
        public Car()
        {
            ParkingDetails = new HashSet<ParkingDetail>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public int? SeatsCount { get; set; }
        public int? OwnerId { get; set; }
        public bool? IsParked { get; set; }

        [JsonIgnore]
        public virtual Owner? Owner { get; set; }
        [JsonIgnore]
        public virtual ICollection<ParkingDetail> ParkingDetails { get; set; }
    }
}
