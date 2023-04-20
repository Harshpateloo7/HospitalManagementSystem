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
    public class SpecialistDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Specialist in the system
        /// </summary>
        /// <returns>
        /// Header: 200 (ok)
        /// Content: All Specialist in the database
        /// </returns>
        /// <example>
        /// GET: api/SpecialistData/ListSpecialists
        /// </example>
        [HttpGet]
        public IEnumerable<SpecialistDto> ListSpecialists()
        {
            List<Specialist> Specialists = db.Specialists.ToList();
            List<SpecialistDto> SpecialistDtos = new List<SpecialistDto>();

            Specialists.ForEach(p => SpecialistDtos.Add(new SpecialistDto()
            {
                SpecialistId = p.SpecialistId,
                SpecialistName = p.SpecialistName,
                DoctorName = p.Doctor.DoctorName
            }));
            return SpecialistDtos;
        }


        /// <summary>
        /// Gathers information about Specialist related to a particular Branch Id
        /// </summary>
        /// <returns>
        /// Header: 200 (ok)
        /// Content: All Specialist in the database
        /// </returns>
        /// <param name="id">Branch Id</param>
        /// <example>
        /// GET: api/SpecialistData/ListSpecialistsForBranch/3
        /// </example>
        [HttpGet]
        public IEnumerable<SpecialistDto> ListSpecialistsForDoctor(int id)
        {
            List<Specialist> Specialists = db.Specialists.Where(p => p.DoctorId == id).ToList();
            List<SpecialistDto> SpecialistDtos = new List<SpecialistDto>();

            Specialists.ForEach(p => SpecialistDtos.Add(new SpecialistDto()
            {
                SpecialistId = p.SpecialistId,
                SpecialistName = p.SpecialistName,
                DoctorName = p.Doctor.DoctorName
            }));
            return SpecialistDtos;
        }

        // GET: api/SpecialistData/FindSpecialist/5
        [HttpGet]
        [ResponseType(typeof(Specialist))]
        public IHttpActionResult FindSpecialist(int id)
        {
            Specialist Specialist = db.Specialists.Find(id);
            SpecialistDto SpecialistDto = new SpecialistDto()
            {
                SpecialistId = Specialist.SpecialistId,
                SpecialistName = Specialist.SpecialistName,
                DoctorName = Specialist.Doctor.DoctorName
            };
            if (Specialist == null)
            {
                return NotFound();
            }

            return Ok(SpecialistDto);
        }

        // POST: api/SpecialistData/UpdateSpecialist/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateSpecialist(int id, Specialist specialist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != specialist.SpecialistId)
            {
                return BadRequest();
            }

            db.Entry(specialist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialistExists(id))
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

        // POST: api/SpecialistData/AddSpecialist
        [ResponseType(typeof(Specialist))]
        [HttpPost]
        public IHttpActionResult AddSpecialist(Specialist specialist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Specialists.Add(specialist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = specialist.SpecialistId }, specialist);
        }

        // POST: api/SpecialistData/DeleteSpecialist/5
        //curl -d "" https://localhost:44313/api/SpecialistData/DeleteSpecialist/5
        [ResponseType(typeof(Specialist))]
        [HttpPost]
        public IHttpActionResult DeleteSpecialist(int id)
        {
            Specialist specialist = db.Specialists.Find(id);

            if (specialist == null)
            {
                return NotFound();
            }

            db.Specialists.Remove(specialist);
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

        private bool SpecialistExists(int id)
        {
            return db.Specialists.Count(e => e.SpecialistId == id) > 0;
        }
    
    }
}