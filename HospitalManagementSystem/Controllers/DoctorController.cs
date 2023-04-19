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

namespace HospitalManagementSystem.Controllers
{
    public class DoctorController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static DoctorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/");
        }

        // GET: Doctor/List
        public ActionResult List()
        {
            string url = "DoctorData/ListDoctors";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DoctorDto> doctor = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;


            return View(doctor);
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            DoctorDetails ViewModel = new DoctorDetails();
            string url = "DoctorData/FindDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorDto doctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            ViewModel.Doctor = doctor;

            url = "medicinedata/ListOfMedicinesByDoctor/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<MedicineDto> Medicines = response.Content.ReadAsAsync<IEnumerable<MedicineDto>>().Result;
            ViewModel.Medicine = Medicines;

            url = "medicinedata/ListOtherMedicines/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<MedicineDto> OtherMedicines = response.Content.ReadAsAsync<IEnumerable<MedicineDto>>().Result;
            ViewModel.OtherMedicines = OtherMedicines;

            return View(ViewModel);
        }

        //POST: Doctor/Associate/{doctorid}
        [HttpPost]
        public ActionResult Associate(int id, int medicineId)
        {
            string url = "Doctor/AssociateDoctorWithMedicine/" + id + "/" + medicineId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //POST: Doctor/UnAssociate/{doctorid}
        [HttpGet]
        public ActionResult UnAssociate(int id, int medicineId)
        {
            string url = "Doctor/UnAssociateDoctorWithMedicine/" + id + "/" + medicineId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Doctor/New
        public ActionResult New()
        {
            string url = "ParkingData/ListParkings";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ParkingDto> parking = response.Content.ReadAsAsync<IEnumerable<ParkingDto>>().Result;


            return View(parking);
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(Doctor Doctor)
        {
            string url = "DoctorData/adddoctor";

            string jsonpayload = jss.Serialize(Doctor);

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

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDoctor ViewModel = new UpdateDoctor();
            string url = "DoctorData/finddoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorDto doctor = response.Content.ReadAsAsync<DoctorDto>().Result;

            ViewModel.Doctor = doctor;

            url = "ParkingData/ListParkings/";
            response = client.GetAsync(url).Result;
            IEnumerable<ParkingDto> parking = response.Content.ReadAsAsync<IEnumerable<ParkingDto>>().Result;

            ViewModel.Parking = parking;

            return View(ViewModel);
        }


        // POST: Doctor/Update/5
        [HttpPost]
        public ActionResult Update(int id, Doctor Doctor)
        {
            string url = "DoctorData/updatedoctor/" + id;
            string jsonpayload = jss.Serialize(Doctor);

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

        // GET: Doctor/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "DoctorData/finddoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorDto doctor = response.Content.ReadAsAsync<DoctorDto>().Result;


            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Doctor Doctor)
        {
            string url = "DoctorData/deletedoctor/" + id;
            string jsonpayload = jss.Serialize(Doctor);

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
