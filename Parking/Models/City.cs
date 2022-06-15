using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parking.Models
{
    public class City
    {
        public City()
        {
            Stations = new HashSet<Station>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Station> Stations { get; set; }
    }
}
