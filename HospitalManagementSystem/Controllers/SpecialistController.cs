using HospitalManagementSystem.Models.ViewModels;
using HospitalManagementSystem.Models;
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
    public class SpecialistController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static SpecialistController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/");
        }
        // GET: Specialist/List
        public ActionResult List()
        {
            //objective: communicate with our Specialist data api to retrieve a list of Specialists
            //curl https://localhost:44313/api/SpecialistData/ListSpecialists

            string url = "SpecialistData/ListSpecialists";
            HttpResponseMessage response = client.GetAsync(url).Result;
            // Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<SpecialistDto> specialists = response.Content.ReadAsAsync<IEnumerable<SpecialistDto>>().Result;
            //Debug.WriteLine("Number of Specialist received : ");
            //Debug.WriteLine(specialists.Count());

            return View(specialists);
        }

        // GET: Specialist/Details/5
        public ActionResult Details(int id)
        {
            //objective:  communicate with our Specialist data api to reteieve one Specialist
            // curl https://localhost:44313/api/SpecialistData/FindSpecialist/{id}


            string url = "SpecialistData/FindSpecialist/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            SpecialistDto selectedspecialist = response.Content.ReadAsAsync<SpecialistDto>().Result;
            //Debug.WriteLine("Specialist received: ");
            //Debug.WriteLine(selectedSpecialist.SpecialistName);



            return View(selectedspecialist);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Specialist/New
        public ActionResult New()
        {
            // information about all branch in the system
            // GET: api/DoctorData/ListDoctors

            string url = "DoctorData/ListDoctors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> DoctorsOptions = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            return View(DoctorsOptions);
        }

        // POST: Specialist/Create
        [HttpPost]
        public ActionResult Create(Specialist specialist)
        {
            Debug.WriteLine("The json payload is : ");
            //objective: add a new Specialist into our system using the API
            //curl -H "Content-type:application/json" -d @Specialist.json  https://localhost:44313/api/SpecialistData/AddSpecialist
            string url = "SpecialistData/AddSpecialist";


            string jsonpayload = jss.Serialize(specialist);

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

        // GET: Specialist/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateSpecialist ViewModel = new UpdateSpecialist();
            // the existing Specialist information
            string url = "SpecialistData/FindSpecialist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            SpecialistDto SelectedSpecialist = response.Content.ReadAsAsync<SpecialistDto>().Result;
            ViewModel.SelectedSpecialist = SelectedSpecialist;

            //All branch to choose from when updating this Specialist
            url = "DoctorData/ListDoctors/";
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> DoctorOptions = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;

            ViewModel.DoctorOptions = DoctorOptions;

            return View(ViewModel);
        }

        // POST: Specialist/Update/5
        [HttpPost]
        public ActionResult Update(int id, Specialist specialist)
        {
            string url = "SpecialistData/UpdateSpecialist/" + id;
            string jsonpayload = jss.Serialize(specialist);
            HttpContent content = new StringContent(jsonpayload);

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

        // GET: Specialist/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "SpecialistData/FindSpecialist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            SpecialistDto selectedspecialist = response.Content?.ReadAsAsync<SpecialistDto>().Result;
            return View(selectedspecialist);
        }

        // POST: Specialist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "SpecialistData/DeleteSpecialist/" + id;
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
