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
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace HospitalManagementSystem.Controllers
{
    public class PatientDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Patient in the system
        /// </summary>
        /// <returns>
        /// Header: 200 (ok)
        /// Content: All Patient in the database
        /// </returns>
        /// <example>
        /// GET: api/PatientData/ListPatients
        /// </example>
        [HttpGet]
        public IEnumerable<PatientDto> ListPatients()
        {
            List<Patient> Patients = db.Patients.ToList();
            List<PatientDto> PatientDtos = new List<PatientDto>();

            Patients.ForEach(p => PatientDtos.Add(new PatientDto(){
                PatientId = p.PatientId,
                PatientName = p.PatientName,
                PatientEmail= p.PatientEmail,
                PatientPhone= p.PatientPhone,
                PatientPrescription = p.PatientPrescription,
                PatientAddress = p.PatientAddress,
                BranchName = p.Branch.BranchName
            }));
            return PatientDtos;
        }


        /// <summary>
        /// Gathers information about Patient related to a particular Branch Id
        /// </summary>
        /// <returns>
        /// Header: 200 (ok)
        /// Content: All Patient in the database
        /// </returns>
        /// <param name="id">Branch Id</param>
        /// <example>
        /// GET: api/PatientData/ListPatientsForBranch/3
        /// </example>
        [HttpGet]
        public IEnumerable<PatientDto> ListPatientsForBranch(int id)
        {
            List<Patient> Patients = db.Patients.Where(p=>p.BranchId==id).ToList();
            List<PatientDto> PatientDtos = new List<PatientDto>();

            Patients.ForEach(p => PatientDtos.Add(new PatientDto()
            {
                PatientId = p.PatientId,
                PatientName = p.PatientName,
                PatientEmail = p.PatientEmail,
                PatientPhone = p.PatientPhone,
                PatientPrescription = p.PatientPrescription,
                PatientAddress = p.PatientAddress,
                BranchName = p.Branch.BranchName
            }));
            return PatientDtos;
        }

        // GET: api/PatientData/FindPatient/5
        [HttpGet]
        [ResponseType(typeof(Patient))]
        public IHttpActionResult FindPatient(int id)
        {
            Patient Patient = db.Patients.Find(id);
            PatientDto PatientDto = new PatientDto()
            {
                PatientId = Patient.PatientId,
                PatientName = Patient.PatientName,
                PatientEmail = Patient.PatientEmail,
                PatientPhone = Patient.PatientPhone,
                PatientPrescription = Patient.PatientPrescription,
                PatientAddress = Patient.PatientAddress,
                BranchName = Patient.Branch.BranchName
            };
            if (Patient == null)
            {
                return NotFound();
            }

            return Ok(PatientDto);
        }

        // POST: api/PatientData/UpdatePatient/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            db.Entry(patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/PatientData/AddPatient
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult AddPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(patient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patient.PatientId }, patient);
        }

        // POST: api/PatientData/DeletePatient/5
        //curl -d "" https://localhost:44313/api/PatientData/DeletePatient/5
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult DeletePatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            
            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
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

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.PatientId == id) > 0;
        }
    }
}