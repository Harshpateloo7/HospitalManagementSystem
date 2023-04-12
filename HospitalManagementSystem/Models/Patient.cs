using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        //Patient belong to one Branch
        //A Branch can have many Paients
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

       

    }
    public class PatientDto
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public string PatientPrescription { get; set; }

        public string PatientEmail { get; set; }

        public string PatientPhone { get; set; }

        public string PatientAddress { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }
}