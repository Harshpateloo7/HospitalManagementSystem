using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class AppointmentDetails
    {
         public AppointmentDto Appointment { get; set; }
        public IEnumerable<DoctorDto> Doctor { get; set; }
        public IEnumerable<PatientDto> Patient{ get; set; }
    }
}