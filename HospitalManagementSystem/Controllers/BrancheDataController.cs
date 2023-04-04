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
    public class BrancheDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BrancheData/ListBranches
        //curl https://localhost:44313/api/BrancheData/ListBranches
        [HttpGet]
        public IEnumerable<BranchDto> ListBranches()
        {
            List<Branch> Branches = db.Branches.ToList();
            List<BranchDto> BranchDtos  = new List<BranchDto>();

            Branches.ForEach(b => BranchDtos.Add(new BranchDto()
            {
                BranchId = b.BranchId,
                BranchName = b.BranchName,
                BranchEmail = b.BranchEmail,
                BranchPhone = b.BranchPhone,
                BranchAddress = b.BranchAddress
            }));
            return BranchDtos;
        }

        // GET: api/BrancheData/FindBranch/5
        //curl https://localhost:44313/api/BrancheData/FindBranch/{id}
        [ResponseType(typeof(Branch))]
        [HttpGet]
        public IHttpActionResult FindBranch(int id)
        {
            Branch Branch = db.Branches.Find(id);
            BranchDto BranchDto = new BranchDto()
            {
                BranchId = Branch.BranchId,
                BranchName = Branch.BranchName,
                BranchEmail = Branch.BranchEmail,
                BranchPhone = Branch.BranchPhone,
                BranchAddress = Branch.BranchAddress
            };
            if (Branch == null)
            {
                return NotFound();
            }

            return Ok(BranchDto);
        }

        // POST: api/BrancheData/UpdateBranch/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBranch(int id, Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branch.BranchId)
            {
                return BadRequest();
            }

            db.Entry(branch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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

        // POST: api/BrancheData/AddBranch
        // curl -d @branch.json -H "Content-type:application/json" https://localhost:44313/api/BrancheData/AddBranch
        [ResponseType(typeof(Branch))]
        [HttpPost]
        public IHttpActionResult AddBranch(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Branches.Add(branch);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = branch.BranchId }, branch);
        }

        // POST: api/BrancheData/DeleteBranch/5
        //curl -d "" https://localhost:44313/api/BrancheData/DeleteBranch/2
        [ResponseType(typeof(Branch))]
        [HttpPost]
        public IHttpActionResult DeleteBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            db.Branches.Remove(branch);
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

        private bool BranchExists(int id)
        {
            return db.Branches.Count(e => e.BranchId == id) > 0;
        }
    }
}