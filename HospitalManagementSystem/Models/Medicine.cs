using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace HospitalManagementSystem.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string DosePerDay { get; set; }

        public ICollection<Doctor> Doctor { get; set; }
    }

    public class MedicineDto
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string DosePerDay { get; set; }

    }
}