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
    public class CareerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static CareerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/");
        }
        // GET: Career/List
        public ActionResult List()
        {
            //objective: communicate with our career data api to retrive a list of careers
            //curl https://localhost:44313/api/CareerData/ListCareers

            string url = "CareerData/ListCareers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<CareerDto> careers = response.Content.ReadAsAsync<IEnumerable<CareerDto>>().Result;
            Debug.WriteLine("Number of careeres received: ");
            Debug.WriteLine(careers.Count());

            return View(careers);
        }


        // GET: Career/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our career data api to reterive one career
            //curl https://localhost:44313/api/CareerData/FindCareer/{id} 
            DetailsCareer ViewModel = new DetailsCareer();

            string url = "CareerData/FindCareer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CareerDto SelectedCareer = response.Content.ReadAsAsync<CareerDto>().Result;
            /*Debug.WriteLine("career receievd: ");
            Debug.WriteLine(SelectedCareer.CareerTitle);*/

            ViewModel.SelectedCareer = SelectedCareer;
            //Showcase information about Applicant related to this Career
            // Send a request to gather information about Applicant related to particular Career Id
            url = "ApplicantData/ListApplicantsForCareer/" + id;
            response = client.GetAsync(url).Result;
            Debug.WriteLine(response);
            IEnumerable<ApplicantDto> RelatedApplicants = response.Content.ReadAsAsync<IEnumerable<ApplicantDto>>().Result;

            ViewModel.RelatedApplicants = RelatedApplicants;


            return View(ViewModel);
        }


        public ActionResult Error()
        {
            return View();
        }

        // GET: Career/New
        public ActionResult New()
        {

            return View();
        }




        // POST: Career/Create
        [HttpPost]
        public ActionResult Create(Career career)
        {
            Debug.WriteLine("The json playload is: ");
            Debug.WriteLine(career.CareerTitle);

            //objective: add a new career into our system using the API
            //curl -H "Content-type:application/json" -d @career.json https://localhoast:44313/api/CareerData/AddCareer

            string url = "CareerData/AddCareer";


            string jsonpayload = jss.Serialize(career);

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


        // GET: Career/Edit/5
        public ActionResult Edit(int id)
        {
            // the existing Career Information
            string url = "CareerData/FindCareer/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            CareerDto selectedcareer = response.Content.ReadAsAsync<CareerDto>().Result;
            return View(selectedcareer);
        }

        // POST: Career/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Career career)
        {
            string url = "CareerData/UpdateCareer/" + id;
            string jsonpayload = jss.Serialize(career);
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

        // GET: Career/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "CareerData/FindCareer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CareerDto selectedcareer = response.Content.ReadAsAsync<CareerDto>().Result;
            return View(selectedcareer);
        }

        // POST: Career/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "CareerData/DeleteCareer/" + id;
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
