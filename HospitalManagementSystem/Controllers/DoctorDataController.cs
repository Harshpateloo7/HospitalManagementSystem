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
using System.Web.Routing;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class DoctorDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DoctorData/ListDoctors
        [HttpGet]
        public IEnumerable<DoctorDto> ListDoctors()
        {
            List<Doctor> Doctors = db.Doctors.ToList();
            List<DoctorDto> DoctorDtos = new List<DoctorDto>();

            Doctors.ForEach(a => DoctorDtos.Add(new DoctorDto()
            {
                DoctorId = a.DoctorId,
                DoctorName = a.DoctorName,
                DoctorDescription = a.DoctorDescription,
                ParkingPosition = a.Parking.ParkingPosition
            }));

            return DoctorDtos;
        }

        // GET: api/DoctorData/ListDoctorsParking/3
        [HttpGet]
        public IEnumerable<DoctorDto> ListDoctorsParking(int id)
        {
            List<Doctor> Doctors = db.Doctors.Where(a => a.ParkingId == id).ToList();
            List<DoctorDto> DoctorDtos = new List<DoctorDto>();

            Doctors.ForEach(a => DoctorDtos.Add(new DoctorDto()
            {
                DoctorId = a.DoctorId,
                DoctorName = a.DoctorName,
                DoctorDescription = a.DoctorDescription,
                ParkingPosition = a.Parking.ParkingPosition
            }));

            return DoctorDtos;
        }

        // GET: api/DoctorData/ListDoctorsMedicine/3
        [HttpGet]
        public IEnumerable<DoctorDto> ListDoctorsMedicine(int id)
        {
            List<Doctor> Doctors = db.Doctors.Where(a => a.Medicines.Any(
                        k => k.MedicineId == id
                )).ToList();
            List<DoctorDto> DoctorDtos = new List<DoctorDto>();

            Doctors.ForEach(a => DoctorDtos.Add(new DoctorDto()
            {
                DoctorId = a.DoctorId,
                DoctorName = a.DoctorName,
                DoctorDescription = a.DoctorDescription,
                ParkingPosition = a.Parking.ParkingPosition
            }));

            return DoctorDtos;
        }

        [HttpPost]
        [Route("api/Doctor/AssociateDoctorWithMedicine/{doctorid}/{medicineid}")]
        public IHttpActionResult AssociateDoctorWithMedicine(int doctorid, int medicineid)
        {

            Doctor SelectedDoctor = db.Doctors.Include(a => a.Medicines).Where(a => a.DoctorId == doctorid).FirstOrDefault();
            Medicine Medicine = db.Medicines.Find(medicineid);

            if (SelectedDoctor == null || Medicine == null)
            {
                return NotFound();
            }


            SelectedDoctor.Medicines.Add(Medicine);
            db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("api/Doctor/UnAssociateDoctorWithMedicine/{doctorid}/{medicineid}")]
        public IHttpActionResult UnAssociateDoctorWithMedicine(int doctorid, int medicineid)
        {

            Doctor SelectedDoctor = db.Doctors.Include(a => a.Medicines).Where(a => a.DoctorId == doctorid).FirstOrDefault();
            Medicine Medicine = db.Medicines.Find(medicineid);

            if (SelectedDoctor == null || Medicine == null)
            {
                return NotFound();
            }


            SelectedDoctor.Medicines.Remove(Medicine);
            db.SaveChanges();

            return Ok();
        }

        // GET: api/DoctorData/FindDoctor/5
        [ResponseType(typeof(Doctor))]
        [HttpGet]
        public IHttpActionResult FindDoctor(int id)
        {
            Doctor Doctor = db.Doctors.Find(id);
            DoctorDto DoctorDtos = new DoctorDto()
            {
                DoctorId = Doctor.DoctorId,
                DoctorName = Doctor.DoctorName,
                DoctorDescription = Doctor.DoctorDescription,
                ParkingPosition = Doctor.Parking.ParkingPosition
            };
            if (Doctor == null)
            {
                return NotFound();
            }

            return Ok(DoctorDtos);
        }

        // POST: api/DoctorData/UpdateDoctor/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDoctor(int id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctor.DoctorId)
            {
                return BadRequest();
            }

            db.Entry(doctor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/DoctorData/AddDoctor
        [ResponseType(typeof(Doctor))]
        [HttpPost]
        public IHttpActionResult AddDoctor(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Doctors.Add(doctor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = doctor.DoctorId }, doctor);
        }

        // POST: api/DoctorData/DeleteDoctor/5
        [ResponseType(typeof(Doctor))]
        [HttpPost]
        public IHttpActionResult DeleteDoctor(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(doctor);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoctorExists(int id)
        {
            return db.Doctors.Count(e => e.DoctorId == id) > 0;
        }
    }
}