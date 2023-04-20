using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Applicant
    {
        [Key]
        public int ApplicantId { get; set; }

        public string ApplicantName { get; set; }

        public string ApplicantCoverLetter { get; set; }

        public string ApplicantEmail { get; set; }

        public string ApplicantPhone { get; set; }

        public string ApplicantAddress { get; set; }

        [ForeignKey("Career")]
        public int CareerId { get; set; }
        public virtual Career Career { get; set; }
    }

    public class ApplicantDto
    {
        public int ApplicantId { get; set; }

        public string ApplicantName { get; set; }

        public string ApplicantCoverLetter { get; set; }

        public string ApplicantEmail { get; set; }

        public string ApplicantPhone { get; set; }

        public string ApplicantAddress { get; set; }

        public int CareerId { get; set; }
        public string CareerTitle { get; set; }

    }
}