using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class UpdateSpecialist
    {
        //This viewmodel is a class which stores information that we need to present to /Specialist/Update/{}



        // the existing Specialist information

        public SpecialistDto SelectedSpecialist { get; set; }


        //All branch to choose from when updating this Specialist

        public IEnumerable<DoctorDto> DoctorOptions { get; set; }
    }
}