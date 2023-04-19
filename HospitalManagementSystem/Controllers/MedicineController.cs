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
    public class MedicineController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        static MedicineController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/");
        }
        // GET: Medicine/List
        public ActionResult List()
        {
            string url = "MedicineData/ListMedicines";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<MedicineDto> medicine = response.Content.ReadAsAsync<IEnumerable<MedicineDto>>().Result;


            return View(medicine);
        }

        // GET: Medicine/Details/5
        public ActionResult Details(int id)
        {
            MedicineDetails ViewModel = new MedicineDetails();

            string url = "MedicineData/FindMedicine/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            MedicineDto medicine = response.Content.ReadAsAsync<MedicineDto>().Result;
            ViewModel.Medicine = medicine;

            url = "Doctordata/ListDoctorsMedicine/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> PrescibedByDoctors = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            ViewModel.PrescibedByDoctors = PrescibedByDoctors;

            return View(ViewModel);
        }

        // GET: Medicine/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Medicine/Create
        [HttpPost]
        public ActionResult Create(Medicine Medicine)
        {
            string url = "MedicineData/addMedicine";

            string jsonpayload = jss.Serialize(Medicine);

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

        // GET: Medicine/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "Medicinedata/findMedicine/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MedicineDto Medicine = response.Content.ReadAsAsync<MedicineDto>().Result;
            return View(Medicine);
        }

        // POST: Medicine/Update/5
        [HttpPost]
        public ActionResult Update(int id, Medicine Medicine)
        {
            string url = "Medicinedata/updateMedicine/" + id;
            string jsonpayload = jss.Serialize(Medicine);
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
        public ActionResult Error()
        {
            return View();
        }

        // GET: Medicine/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "MedicineData/findMedicine/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MedicineDto Medicine = response.Content.ReadAsAsync<MedicineDto>().Result;


            return View(Medicine);
        }

        // POST: Medicine/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Medicine Medicine)
        {
            string url = "MedicineData/deleteMedicine/" + id;
            string jsonpayload = jss.Serialize(Medicine);

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
