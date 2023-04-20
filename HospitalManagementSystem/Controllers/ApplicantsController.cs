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
    public class ApplicantsController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static ApplicantsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/");
        }
        // GET: Applicants/List
        public ActionResult List()
        {
            //objective: communicate with our applicant data api to retrieve a list of applicants
            //curl https://localhost:44313/api/ApplicantData/ListApplicants

            string url = "ApplicantData/ListApplicants";
            HttpResponseMessage response = client.GetAsync(url).Result;
            // Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ApplicantDto> applicants = response.Content.ReadAsAsync<IEnumerable<ApplicantDto>>().Result;
            //Debug.WriteLine("Number of Applicant received : ");
            //Debug.WriteLine(applicants.Count());

            return View(applicants);
        }

        // GET: Applicants/Details/5
        public ActionResult Details(int id)
        {
            //objective:  communicate with our applicant data api to reteieve one applicant
            // curl https://localhost:44313/api/ApplicantData/FindApplicant/{id}


            string url = "ApplicantData/FindApplicant/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            ApplicantDto selectedapplicant = response.Content.ReadAsAsync<ApplicantDto>().Result;
            //Debug.WriteLine("applicant received: ");
            //Debug.WriteLine(selectedapplicant.ApplicantName);



            return View(selectedapplicant);
        }

       

        // GET: Applicants/New
        public ActionResult New()
        {
            // information about all branch in the system
            // GET: api/CareerData/ListCareers

            string url = "CareerData/ListCareers";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<CareerDto> careersOptions = response.Content.ReadAsAsync<IEnumerable<CareerDto>>().Result;
            return View(careersOptions);
        }

        // POST: Applicants/Create
        [HttpPost]
        public ActionResult Create(Applicant applicant)
        {
            Debug.WriteLine("The json payload is : ");
            //objective: add a new applicant into our system using the API
            //curl -H "Content-type:application/json" -d @Applicant.json  https://localhost:44313/api/ApplicantData/AddApplicant
            string url = "ApplicantData/AddApplicant";


            string jsonpayload = jss.Serialize(applicant);

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

        // GET: Applicants/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateApplicant ViewModel = new UpdateApplicant();
            // the existing Applicant information
            string url = "ApplicantData/FindApplicant/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ApplicantDto SelectedApplicant = response.Content.ReadAsAsync<ApplicantDto>().Result;
            ViewModel.SelectedApplicant = SelectedApplicant;

            //All branch to choose from when updating this Applicant
            url = "CareerData/ListCareers/";
            response = client.GetAsync(url).Result;
            IEnumerable<CareerDto> CareerOptions = response.Content.ReadAsAsync<IEnumerable<CareerDto>>().Result;

            ViewModel.CareerOptions = CareerOptions;

            return View(ViewModel);
        }

        // POST: Applicants/Update/5
        [HttpPost]
        public ActionResult Update(int id, Applicant applicant)
        {
            string url = "ApplicantData/UpdateApplicant/" + id;
            string jsonpayload = jss.Serialize(applicant);
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

        // GET: Applicants/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "ApplicantData/FindApplicant/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ApplicantDto selectedapplicant = response.Content?.ReadAsAsync<ApplicantDto>().Result;
            return View(selectedapplicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "ApplicantData/DeleteApplicant/" + id;
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
