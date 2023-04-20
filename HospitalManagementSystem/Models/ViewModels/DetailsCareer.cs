using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class DetailsCareer
    {
        public CareerDto SelectedCareer { get; set; }
        public IEnumerable<ApplicantDto> RelatedApplicants { get; set; }
    }
}