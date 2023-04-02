using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        public string PatientName { get; set;}

        public string PatientPrescription { get; set; }

        public string PatientEmail { get; set; }

        public string PatientPhone { get; set;}

        public string PatientAddress { get; set; }

    }
    public class PatientDto
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public string PatientPrescription { get; set; }

        public string PatientEmail { get; set; }

        public string PatientPhone { get; set; }

        public string PatientAddress { get; set; }
    }
}