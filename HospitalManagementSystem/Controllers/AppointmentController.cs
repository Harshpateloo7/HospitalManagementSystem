using HospitalManagementSystem.Models;
using HospitalManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static AppointmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/");
        }
        // GET: Appointment/List
        public ActionResult List()
        {
            //objective: communicate with our Appointment data api to retrieve a list of Appointments
            //curl https://localhost:44313/api/AppointmentData/ListAppointments

            string url = "AppointmentData/ListAppointments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            // Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<AppointmentDto> Appointments = response.Content.ReadAsAsync<IEnumerable<AppointmentDto>>().Result;
            //Debug.WriteLine("Number of Appointment received : ");
            //Debug.WriteLine(Appointments.Count());

            return View(Appointments);
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
            //objective:  communicate with our Appointment data api to reteieve one Appointment
            // curl https://localhost:44313/api/AppointmentData/FindAppointment/{id}


            string url = "AppointmentData/FindAppointment/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            AppointmentDto selectedAppointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            //Debug.WriteLine("Appointment received: ");
            //Debug.WriteLine(selectedAppointment.AppointmentName);



            return View(selectedAppointment);
        }


        public ActionResult Error()
        {
            return View();
        }
        // GET: Appointment/New
        public ActionResult New()
        {
            // information about all branch in the system
            // GET: api/PatientData/ListPatients

            string url = "PatientData/ListPatients";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PatientDto> PatientsOptions = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            return View(PatientsOptions);
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult Create(Appointment Appointment)
        {
            Debug.WriteLine("The json payload is : ");
            //objective: add a new Appointment into our system using the API
            //curl -H "Content-type:application/json" -d @Appointment.json  https://localhost:44313/api/AppointmentData/AddAppointment
            string url = "AppointmentData/AddAppointment";


            string jsonpayload = jss.Serialize(Appointment);

            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }



        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateAppointment ViewModel = new UpdateAppointment();
            // the existing Appointment information
            string url = "AppointmentData/FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentDto SelectedAppointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            ViewModel.SelectedAppointment = SelectedAppointment;

            //All branch to choose from when updating this Appointment
            url = "PatientData/ListPatients/";
            response = client.GetAsync(url).Result;
            IEnumerable<PatientDto> PatientOptions = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;

            ViewModel.PatientOptions = PatientOptions;

            return View(ViewModel);
        }
        // POST: Appointment/Update/5
        [HttpPost]
        public ActionResult Update(int id, Appointment Appointment)
        {
            string url = "AppointmentData/UpdateAppointment/" + id;
            string jsonpayload = jss.Serialize(Appointment);
            HttpContent content = new StringContent(jsonpayload);
            Debug.WriteLine(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }
        // GET: Appointment/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "AppointmentData/FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentDto selectedAppointment = response.Content?.ReadAsAsync<AppointmentDto>().Result;
            return View(selectedAppointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "AppointmentData/DeleteAppointment/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }
    }
}
