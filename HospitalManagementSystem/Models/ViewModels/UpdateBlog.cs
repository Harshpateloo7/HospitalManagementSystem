using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class UpdateBlog
    {
        //This viewmodel is a class which stores information that we need to present to /Blog/Update/{}



        // the existing Blog information

        public BlogDto SelectedBlog { get; set; }


        //All Doctor to choose from when updating this Blog

        public IEnumerable<DoctorDto> DoctorOptions { get; set; }
    }
}