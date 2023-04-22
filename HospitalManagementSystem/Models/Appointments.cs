using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Appointments
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime AppointmentDateAndTime { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public ICollection<Doctor> Doctor { get; set; }
    }
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }

        public DateTime AppointmentDateAndTime { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; }
    }
}