using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HospitalManagementSystem.Models.ViewModels;
using HospitalManagementSystem.Models;
using System.Diagnostics;
using System.Web.Routing;
using Mysqlx.Crud;

namespace HospitalManagementSystem.Controllers
{
    public class ParkingController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        static ParkingController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/");
        }
        // GET: Parking/List
        public ActionResult List()
        {
            string url = "ParkingData/ListParkings";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ParkingDto> parking = response.Content.ReadAsAsync<IEnumerable<ParkingDto>>().Result;


            return View(parking);
        }

        // GET: Parking/Details/5
        public ActionResult Details(int id)
        {
            ParkingDetails ViewModel = new ParkingDetails();

            string url = "ParkingData/FindParking/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ParkingDto parking = response.Content.ReadAsAsync<ParkingDto>().Result;
            ViewModel.Parking = parking;

            url = "doctordata/ListDoctorsParking/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> RelatedDoctors = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            ViewModel.RelatedDoctors = RelatedDoctors;

            return View(ViewModel);
        }

        // GET: Parking/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Parking/Create
        [HttpPost]
        public ActionResult Create(Parking Parking)
        {
            string url = "ParkingData/addParking";

            string jsonpayload = jss.Serialize(Parking);

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

        // GET: Parking/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "Parkingdata/findParking/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ParkingDto Parking = response.Content.ReadAsAsync<ParkingDto>().Result;
            return View(Parking);
        }

        // POST: Parking/Update/5
        [HttpPost]
        public ActionResult Update(int id, Parking Parking)
        {
            string url = "Parkingdata/updateParking/" + id;
            string jsonpayload = jss.Serialize(Parking);
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

        // GET: Parking/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "ParkingData/findParking/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ParkingDto Parking = response.Content.ReadAsAsync<ParkingDto>().Result;


            return View(Parking);
        }
        public ActionResult Error()
        {
            return View();
        }

        // POST: Parking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Parking Parking)
        {
            string url = "ParkingData/deleteParking/" + id;
            string jsonpayload = jss.Serialize(Parking);

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
    }
}
