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
    public class CareerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CareerData/ListCareers
        //curl https://localhost:44313/api/CareerData/ListCareers
        [HttpGet]
        public IEnumerable<CareerDto> ListCareers()
        {
            List<Career> Careers = db.Careers.ToList();
            List<CareerDto> CareerDtos = new List<CareerDto>();

            Careers.ForEach(b => CareerDtos.Add(new CareerDto()
            {
                CareerId = b.CareerId,
                CareerTitle = b.CareerTitle,
                CareerDesc = b.CareerDesc,
                CareerSalary = b.CareerSalary,
                CareerLocation = b.CareerLocation
            }));
            return CareerDtos;
        }

        // GET: api/CareerData/FindCareer/5
        //curl https://localhost:44313/api/CareerData/FindCareer/{id}
        [ResponseType(typeof(Career))]
        [HttpGet]
        public IHttpActionResult FindCareer(int id)
        {
            Career Career = db.Careers.Find(id);
            CareerDto CareerDto = new CareerDto()
            {
                CareerId = Career.CareerId,
                CareerTitle = Career.CareerTitle,
                CareerDesc = Career.CareerDesc,
                CareerSalary = Career.CareerSalary,
                CareerLocation = Career.CareerLocation
            };
            if (Career == null)
            {
                return NotFound();
            }

            return Ok(CareerDto);
        }

        // POST: api/CareerData/UpdateCareer/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCareer(int id, Career career)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != career.CareerId)
            {
                return BadRequest();
            }

            db.Entry(career).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareerExists(id))
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

        // POST: api/CareerData/AddCareer
        // curl -d @branch.json -H "Content-type:application/json" https://localhost:44313/api/CareerData/AddCareer
        [ResponseType(typeof(Career))]
        [HttpPost]
        public IHttpActionResult AddCareer(Career career)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Careers.Add(career);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = career.CareerId }, career);
        }

        // POST: api/CareerData/DeleteCareer/5
        //curl -d "" https://localhost:44313/api/CareerData/DeleteCareer/2
        [ResponseType(typeof(Career))]
        [HttpPost]

        public IHttpActionResult DeleteCareer(int id)
        {
            Career career = db.Careers.Find(id);
            if (career == null)
            {
                return NotFound();
            }

            db.Careers.Remove(career);
            db.SaveChanges();

            return Ok(career);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CareerExists(int id)
        {
            return db.Careers.Count(e => e.CareerId == id) > 0;
        }
    }
}