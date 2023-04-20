using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class UpdateApplicant
    {
        //This viewmodel is a class which stores information that we need to present to /Applicants/Update/{}



        // the existing Applicant information

        public ApplicantDto SelectedApplicant { get; set; }


        //All branch to choose from when updating this Applicant

        public IEnumerable<CareerDto> CareerOptions { get; set; }
    }
}