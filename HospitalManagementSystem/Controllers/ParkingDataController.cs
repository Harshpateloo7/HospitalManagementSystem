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
using Mysqlx.Crud;

namespace HospitalManagementSystem.Controllers
{
    public class ParkingDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ParkingData/ListParkings
        [HttpGet]
        public IEnumerable<ParkingDto> ListParkings()
        {
            List<Parking> Parking = db.Parkings.ToList();
            List<ParkingDto> ParkingDtos = new List<ParkingDto>();

            Parking.ForEach(a => ParkingDtos.Add(new ParkingDto()
            {
                ParkingId = a.ParkingId,
                ParkingPosition = a.ParkingPosition,
                ParkingWing = a.ParkingWing
            }));

            return ParkingDtos;
        }

        // GET: api/ParkingData/FindParking/5
        [ResponseType(typeof(Parking))]
        [HttpGet]
        public IHttpActionResult FindParking(int id)
        {
            Parking Parking = db.Parkings.Find(id);
            ParkingDto ParkingDtos = new ParkingDto()
            {
                ParkingId = Parking.ParkingId,
                ParkingPosition = Parking.ParkingPosition,
                ParkingWing = Parking.ParkingWing
            };
            if (Parking == null)
            {
                return NotFound();
            }

            return Ok(ParkingDtos);
        }

        // PUT: api/ParkingData/UpdateParking/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateParking(int id, Parking parking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parking.ParkingId)
            {
                return BadRequest();
            }

            db.Entry(parking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingExists(id))
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

        // POST: api/ParkingData/AddParking
        [ResponseType(typeof(Parking))]
        public IHttpActionResult AddParking(Parking parking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parkings.Add(parking);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parking.ParkingId }, parking);
        }

        // DELETE: api/ParkingData/DeleteParking/5
        [ResponseType(typeof(Parking))]
        [HttpPost]
        public IHttpActionResult DeleteParking(int id)
        {
            Parking parking = db.Parkings.Find(id);
            if (parking == null)
            {
                return NotFound();
            }

            db.Parkings.Remove(parking);
            db.SaveChanges();

            return Ok(parking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParkingExists(int id)
        {
            return db.Parkings.Count(e => e.ParkingId == id) > 0;
        }
    }
}