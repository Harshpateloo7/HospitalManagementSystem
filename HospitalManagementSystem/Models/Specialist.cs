using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Specialist
    {
        [Key]
        public int SpecialistId { get; set; }
        public string SpecialistName { get; set; }


        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }


    }


    public class SpecialistDto
    {
        public int SpecialistId { get; set; }

        public string SpecialistName { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }
    }
}