using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parking.Models
{
    public class ParkingHistory
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public DateTime StartHistoryDate { get; set; }

        [JsonIgnore]
        public virtual Owner? Owner { get; set; }
    }
}
