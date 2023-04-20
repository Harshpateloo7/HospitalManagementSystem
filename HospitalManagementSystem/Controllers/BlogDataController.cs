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
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace HospitalManagementSystem.Controllers
{
    public class BlogDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BlogData/ListBlogs
        [HttpGet]
        public IEnumerable<BlogDto> ListBlogs()
        {
            List<Blog> Blogs = db.Blog.ToList();
            List<BlogDto> BlogDtos = new List<BlogDto>();

            Blogs.ForEach(p => BlogDtos.Add(new BlogDto()
            {
                BlogId = p.BlogId,
                Title = p. Title,
                Content = p. Content,
                CreatedDate = p.CreatedDate,
                DoctorName = p.Doctor.DoctorName
            }));
            return BlogDtos;
        }

        [HttpGet]
        public IEnumerable<BlogDto> ListDoctorsForBlog(int id)
        {
            List<Blog> blogs = db.Blog.Where(p => p.DoctorId == id).ToList();
            List<BlogDto> blogDtos = new List<BlogDto>();

            blogs.ForEach(b => blogDtos.Add(new BlogDto()
            {
                BlogId = b.BlogId,
                Title = b.Title,
                Content = b.Content,
                CreatedDate = b.CreatedDate,
                DoctorName = b.Doctor.DoctorName,
            }));
            return blogDtos;
        }

        // GET: api/BlogData/FindBlog/5
        [ResponseType(typeof(Blog))]
        [HttpGet]
        public IHttpActionResult FindBlog(int id)
        {
            Blog blog = db.Blog.Find(id);
            BlogDto BlogDto = new BlogDto()
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Content = blog.Content,
                CreatedDate = blog.CreatedDate,
                DoctorId = blog.DoctorId,
            };
            if (blog == null)
            {
                return NotFound();
            }

            return Ok(BlogDto);
        }

        // POST: api/BlogData/UpdateBlog/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBlog(int id, Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            db.Entry(blog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
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

        // POST: api/BlogData/AddBlog
        [ResponseType(typeof(Blog))]
        [HttpPost]
        public IHttpActionResult AddBlog(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Blog.Add(blog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = blog.BlogId }, blog);
        }

        // POST: api/BlogData/BlogData/5
        [ResponseType(typeof(Blog))]
        [HttpPost]
        public IHttpActionResult DeleteBlog(int id)
        {
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return NotFound();
            }

            db.Blog.Remove(blog);
            db.SaveChanges();

            return Ok(blog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlogExists(int id)
        {
            return db.Blog.Count(e => e.BlogId == id) > 0;
        }
    }
}