using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class ParkingDetails
    {
        public ParkingDto Parking { get; set; }
        public IEnumerable<DoctorDto> RelatedDoctors { get; set; }
    }
}