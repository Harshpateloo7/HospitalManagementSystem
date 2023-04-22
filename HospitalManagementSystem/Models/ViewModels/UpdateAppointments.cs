using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class UpdateAppointments
    {
        //This viewmodel is a class which stores information that we need to present to /Appointment/Update/{}



        // the existing Appointment information

        public AppointmentDto SelectedAppointment { get; set; }


        //All branch to choose from when updating this Appointment

        public IEnumerable<PatientDto> PatientOptions { get; set; }
    }
}