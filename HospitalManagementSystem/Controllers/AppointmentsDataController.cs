using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalManagementSystem.Migrations;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AppointmentData/ListAppointments
        [HttpGet]
        public IEnumerable<AppointmentDto> ListAppointments()
        {
            List<Appointments> Appointments = db.Appointmentss.ToList();
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
            List<Appointments> Appointments = db.Appointmentss.Where(p => p.PatientId == id).ToList();
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
        [ResponseType(typeof(Appointments))]
        public IHttpActionResult FindAppointment(int id)
        {
            Debug.WriteLine(id);
            Appointments Appointment = db.Appointmentss.Find(id);
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
        public IHttpActionResult UpdateAppointment(int id, Appointments Appointment)
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
        public IHttpActionResult AddAppointment(Appointments appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointmentss.Add(appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointment.AppointmentId }, appointment);
        }

        // POST: api/AppintmentData/AppointmentPatient/5
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult DeleteAppointment(int id)
        {
            Appointments appointment = db.Appointmentss.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointmentss.Remove(appointment);
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
            return db.Appointmentss.Count(e => e.AppointmentId == id) > 0;
        }
    }
}