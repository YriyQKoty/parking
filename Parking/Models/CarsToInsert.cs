using System;
using System.Collections.Generic;

namespace Parking.Models
{
    public class CarsToInsert
    {
        public int RowId { get; set; }
        public string Number { get; set; }
        public int? SeatsCount { get; set; }
        public int? OwnerId { get; set; }
    }
}
