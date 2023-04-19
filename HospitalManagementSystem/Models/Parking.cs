using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Parking
    {
        [Key]
        public int ParkingId { get; set; }
        public string ParkingPosition { get; set; }
        public string ParkingWing { get; set; }
    }
    public class ParkingDto
    {
        public int ParkingId { get; set; }
        public string ParkingPosition { get; set; }
        public string ParkingWing { get; set; }

    }
}