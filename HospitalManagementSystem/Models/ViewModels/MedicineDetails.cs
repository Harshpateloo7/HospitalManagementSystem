using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class MedicineDetails
    {
        public MedicineDto Medicine { get; set; }
        public IEnumerable<DoctorDto> PrescibedByDoctors { get; set; }
    }
}