using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class DoctorDetails
    {
        public DoctorDto Doctor { get; set; }
        public IEnumerable<MedicineDto> Medicine { get; set; }
        public IEnumerable<MedicineDto> OtherMedicines { get; set; }
    }
}