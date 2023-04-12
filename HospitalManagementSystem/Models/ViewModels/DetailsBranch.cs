using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class DetailsBranch
    {
        public BranchDto SelectedBranch { get; set; }
        public IEnumerable<PatientDto> RelatedPatients { get; set; }
    }
}