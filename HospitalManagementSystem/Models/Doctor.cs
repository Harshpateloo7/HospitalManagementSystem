using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorDescription { get; set; }

        [ForeignKey("Parking")]
        public int ParkingId { get; set; }
        public virtual Parking Parking { get; set; }

        public ICollection<Medicine> Medicines { get; set; }
    }

    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorDescription { get; set; }

        public string ParkingPosition { get; set; }


    }
}