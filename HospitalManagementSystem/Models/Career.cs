using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Career
    {
        [Key]
        public int CareerId { get; set; }

        public string CareerTitle { get; set; }

        public string CareerDesc { get; set; }

        public string CareerSalary { get; set; }

        public string CareerLocation { get; set; }
    }

    public class CareerDto
    {
        public int CareerId { get; set; }

        public string CareerTitle { get; set; }

        public string CareerDesc { get; set; }

        public string CareerSalary { get; set; }

        public string CareerLocation { get; set; }
    }
}