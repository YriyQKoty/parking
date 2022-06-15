using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parking.Models
{
    public  class Owner
    {
        public Owner()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public virtual ParkingHistory? ParkingHistory { get; set; }
        [JsonIgnore]
        public virtual ICollection<Car> Cars { get; set; }
    }
}
