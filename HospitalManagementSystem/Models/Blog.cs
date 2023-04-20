using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Content { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }

    public class BlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Content { get; set; }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }

}