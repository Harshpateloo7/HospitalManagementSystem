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
    
    public class PatientController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static PatientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/PatientData/");
        }
        // GET: Patient/List
        public ActionResult List()
        {
            //objective: communicate with our patient data api to retrieve a list of patients
            //curl https://localhost:44313/api/PatientData/ListPatients
          
            string url = "ListPatients";
            HttpResponseMessage response = client.GetAsync(url).Result;
           // Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            //Debug.WriteLine("Number of Patient received : ");
            //Debug.WriteLine(patients.Count());

            return View(patients);
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            //objective:  communicate with our patient data api to reteieve one patient
            // curl https://localhost:44313/api/PatientData/FindPatient/{id}


            string url = "FindPatient/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            PatientDto selectedpatient = response.Content.ReadAsAsync<PatientDto>().Result;
            //Debug.WriteLine("patient received: ");
            //Debug.WriteLine(selectedpatient.PatientName);
            


            return View(selectedpatient);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Patient/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            Debug.WriteLine("The json payload is : ");
            //objective: add a new patient into our system using the API
            //curl -H "Content-type:application/json" -d @Patient.json  https://localhost:44313/api/PatientData/AddPatient
            string url = "AddPatient";

            
            string jsonpayload = jss.Serialize(patient);
            
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

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            // the existing Patient information
            string url = "FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto selectedpatient = response.Content.ReadAsAsync<PatientDto>().Result;
            return View(selectedpatient);
        }

        // POST: Patient/Update/5
        [HttpPost]
        public ActionResult Update(int id, Patient patient)
        {
            string url = "UpdatePatient/" + id;
            string jsonpayload = jss.Serialize(patient);
            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url,content).Result;
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

        // GET: Patient/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto selectedpatient = response.Content?.ReadAsAsync<PatientDto>().Result;
            return View(selectedpatient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DeletePatient/" + id;
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
