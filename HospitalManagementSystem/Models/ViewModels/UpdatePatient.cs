using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class UpdatePatient
    {
        //This viewmodel is a class which stores information that we need to present to /Patient/Update/{}



        // the existing Patient information

        public PatientDto SelectedPatient { get; set; }


        //All branch to choose from when updating this Patient

        public IEnumerable<BranchDto> BranchOptions { get; set; }
    }
}