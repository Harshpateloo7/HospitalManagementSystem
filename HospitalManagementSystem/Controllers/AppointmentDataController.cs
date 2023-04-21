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
    public class AppointmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AppointmentData/ListAppointments
        [HttpGet]
        public IEnumerable<AppointmentDto> ListAppointments()
        {
            List<Appointment> Appointments = db.Appointments.ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(a => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentId = a.AppointmentId,
                AppointmentDateAndTime = a.AppointmentDateAndTime,
                PatientName = a.Patient.PatientName
            }));
            return AppointmentDtos;
        }

        // GET: api/AppointmentData/ListAppointForPatient
        [HttpGet]
        public IEnumerable<AppointmentDto> ListAppointForPatient(int id)
        {
            List<Appointment> Appointments = db.Appointments.Where(p => p.PatientId == id).ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(p => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentId = p.AppointmentId,
                AppointmentDateAndTime = p.AppointmentDateAndTime,
                PatientName = p.Patient.PatientName
            }));
            return AppointmentDtos;
        }


        // GET: api/AppointmentData/FindAppointment/5
        [HttpGet]
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult FindAppointment(int id)
        {
            Appointment Appointment = db.Appointments.Find(id);
            AppointmentDto AppointmentDto = new AppointmentDto()
            {
                AppointmentId = Appointment.AppointmentId,
                AppointmentDateAndTime = Appointment.AppointmentDateAndTime,
                PatientName = Appointment.Patient.PatientName
            };
            if (Appointment == null)
            {
                return NotFound();
            }

            return Ok(AppointmentDto);
        }

        // POST: api/AppintmentData/UpdateAppointment/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAppointment(int id, Appointment Appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Appointment.AppointmentId)
            {
                return BadRequest();
            }

            db.Entry(Appointment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/AppointmentData/AddAppointment
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult AddAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointment.AppointmentId }, appointment);
        }

        // POST: api/AppintmentData/AppointmentPatient/5
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult DeleteAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointment);
            db.SaveChanges();

            return Ok(appointment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentExists(int id)
        {
            return db.Appointments.Count(e => e.AppointmentId == id) > 0;
        }
    }
}