using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class UpdateDoctor
    {
        public DoctorDto Doctor { get; set; }


        public IEnumerable<ParkingDto> Parking { get; set; }
    }
}