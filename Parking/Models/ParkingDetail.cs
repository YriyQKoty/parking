using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parking.Models
{
    public  class ParkingDetail
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public int? StationId { get; set; }
        public DateTime ExpireTime { get; set; }
        public DateTime BookTime { get; set; }
        public int? ParkingHistoryId { get; set; }
        
        [JsonIgnore]
        public decimal? Bill { get; set; }

        [JsonIgnore]
        public virtual Car? Car { get; set; }
        [JsonIgnore]
        public virtual Station? Station { get; set; }
    }
}
