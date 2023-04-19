using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class MedicineDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MedicineData/ListMedicines
        [System.Web.Http.HttpGet]
        public IEnumerable<MedicineDto> ListMedicines()
        {
            List<Medicine> Medicine = db.Medicines.ToList();
            List<MedicineDto> MedicineDtos = new List<MedicineDto>();

            Medicine.ForEach(a => MedicineDtos.Add(new MedicineDto()
            {
                MedicineId = a.MedicineId,
                MedicineName = a.MedicineName,
                DosePerDay = a.DosePerDay
            }));

            return MedicineDtos;
        }

        // GET: api/MedicineData/ListOfMedicinesByDoctor/2
        [System.Web.Http.HttpGet]
        public IEnumerable<MedicineDto> ListOfMedicinesByDoctor(int id)
        {
            List<Medicine> Medicine = db.Medicines.Where(
                    k => k.Doctor.Any(
                        a => a.DoctorId == id)
                    ).ToList();
            List<MedicineDto> MedicineDtos = new List<MedicineDto>();

            Medicine.ForEach(a => MedicineDtos.Add(new MedicineDto()
            {
                MedicineId = a.MedicineId,
                MedicineName = a.MedicineName,
                DosePerDay = a.DosePerDay
            }));

            return MedicineDtos;
        }

        // GET: api/MedicineData/ListOtherMedicines/2
        [System.Web.Http.HttpGet]
        public IEnumerable<MedicineDto> ListOtherMedicines(int id)
        {
            List<Medicine> Medicine = db.Medicines.Where(
                    k => !k.Doctor.Any(
                        a => a.DoctorId == id)
                    ).ToList();
            List<MedicineDto> MedicineDtos = new List<MedicineDto>();

            Medicine.ForEach(a => MedicineDtos.Add(new MedicineDto()
            {
                MedicineId = a.MedicineId,
                MedicineName = a.MedicineName,
                DosePerDay = a.DosePerDay
            }));

            return MedicineDtos;
        }

        // GET: api/MedicineData/FindMedicine/5
        [ResponseType(typeof(Medicine))]
        [System.Web.Http.HttpGet]
        public IHttpActionResult FindMedicine(int id)
        {
            Medicine Medicine = db.Medicines.Find(id);
            MedicineDto MedicineDtos = new MedicineDto()
            {
                MedicineId = Medicine.MedicineId,
                MedicineName = Medicine.MedicineName,
                DosePerDay = Medicine.DosePerDay
            };
            if (Medicine == null)
            {
                return NotFound();
            }

            return Ok(MedicineDtos);
        }

        // PUT: api/MedicineData/UpdateMedicine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateMedicine(int id, Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicine.MedicineId)
            {
                return BadRequest();
            }

            db.Entry(medicine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MedicineData/AddMedicine
        [ResponseType(typeof(Medicine))]
        public IHttpActionResult AddMedicine(Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medicines.Add(medicine);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = medicine.MedicineId }, medicine);
        }

        // DELETE: api/MedicineData/DeleteMedicine/5
        [ResponseType(typeof(Medicine))]
        [HttpPost]
        public IHttpActionResult DeleteMedicine(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return NotFound();
            }

            db.Medicines.Remove(medicine);
            db.SaveChanges();

            return Ok(medicine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicineExists(int id)
        {
            return db.Medicines.Count(e => e.MedicineId == id) > 0;
        }
    }
}