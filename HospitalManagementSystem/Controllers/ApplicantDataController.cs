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
using HospitalManagementSystem.Migrations;
using HospitalManagementSystem.Models;
using Applicant = HospitalManagementSystem.Models.Applicant;

namespace HospitalManagementSystem.Controllers
{
    public class ApplicantDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
        /// <summary>
        /// Returns all Applicant in the system
        /// </summary>
        /// <returns>
        /// Header: 200 (ok)
        /// Content: All Applicant in the database
        /// </returns>
        /// <example>
        /// GET: api/ApplicantData/ListApplicants
        /// </example>
        [HttpGet]
        public IEnumerable<ApplicantDto> ListApplicants()
        {
            List<Applicant> Applicants = db.Applicants.ToList();
            List<ApplicantDto> ApplicantDtos = new List<ApplicantDto>();

            Applicants.ForEach(p => ApplicantDtos.Add(new ApplicantDto()
            {
                ApplicantId = p.ApplicantId,
                ApplicantName = p.ApplicantName,
                ApplicantEmail = p.ApplicantEmail,
                ApplicantPhone = p.ApplicantPhone,
                ApplicantAddress = p.ApplicantAddress,
                ApplicantCoverLetter = p.ApplicantCoverLetter,
                CareerTitle = p.Career.CareerTitle,
                CareerId = p.CareerId,
            }));
            return ApplicantDtos;
        }

        /// <summary>
        /// Gathers information about Applicant related to a particular Branch Id
        /// </summary>
        /// <returns>
        /// Header: 200 (ok)
        /// Content: All Applicant in the database
        /// </returns>
        /// <param name="id">Branch Id</param>
        /// <example>
        /// GET: api/ApplicantData/ListApplicantsForCareer/3
        /// </example>
        [HttpGet]
        public IEnumerable<ApplicantDto> ListApplicantsForCareer(int id)
        {
            List<Applicant> Applicants = db.Applicants.Where(p => p.CareerId == id).ToList();
            List<ApplicantDto> ApplicantDtos = new List<ApplicantDto>();

            Applicants.ForEach(p => ApplicantDtos.Add(new ApplicantDto()
            {
                ApplicantId = p.ApplicantId,
                ApplicantName = p.ApplicantName,
                ApplicantEmail = p.ApplicantEmail,
                ApplicantPhone = p.ApplicantPhone,
                ApplicantAddress = p.ApplicantAddress,
                ApplicantCoverLetter = p.ApplicantCoverLetter,
                CareerTitle = p.Career.CareerTitle,
                CareerId = p.CareerId,
            }));
            return ApplicantDtos;
        }

        // GET: api/ApplicantData/FindApplicant/5
        [HttpGet]
        [ResponseType(typeof(Applicant))]
        public IHttpActionResult FindApplicant(int id)
        {
            Applicant Applicant = db.Applicants.Find(id);
            ApplicantDto ApplicantDto = new ApplicantDto()
            {
                ApplicantId = Applicant.ApplicantId,
                ApplicantName = Applicant.ApplicantName,
                ApplicantEmail = Applicant.ApplicantEmail,
                ApplicantPhone = Applicant.ApplicantPhone,
                ApplicantAddress = Applicant.ApplicantAddress,
                ApplicantCoverLetter = Applicant.ApplicantCoverLetter,
                CareerTitle = Applicant.Career.CareerTitle,
                CareerId = Applicant.CareerId,
            };
            if (Applicant == null)
            {
                return NotFound();
            }

            return Ok(ApplicantDto);
        }


        // POST: api/ApplicantData/UpdateApplicant/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateApplicant(int id, Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicant.ApplicantId)
            {
                return BadRequest();
            }

            db.Entry(applicant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(id))
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

        // POST: api/ApplicantData/AddApplicant
        [ResponseType(typeof(Applicant))]
        public IHttpActionResult AddApplicant(Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Applicants.Add(applicant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = applicant.ApplicantId }, applicant);
        }

        // POST: api/ApplicantData/DeleteApplicant/5
        //curl -d "" https://localhost:44313/api/ApplicantData/DeleteApplicant/5
        [ResponseType(typeof(Applicant))]
        [HttpPost]
        public IHttpActionResult DeleteApplicant(int id)
        {
            Applicant applicant = db.Applicants.Find(id);

            if (applicant == null)
            {
                return NotFound();
            }

            db.Applicants.Remove(applicant);
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

        private bool ApplicantExists(int id)
        {
            return db.Applicants.Count(e => e.ApplicantId == id) > 0;
        }
    }
}