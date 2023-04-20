using HospitalManagementSystem.Models;
using HospitalManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HospitalManagementSystem.Controllers
{
    public class BlogController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static BlogController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/");
        }
            // GET: Blog/List
            public ActionResult List()
            {
            //objective: communicate with our Blog data api to retrieve a list of Blogs
            //curl https://localhost:44313/api/BlogData/ListBlogs

            string url = "BlogData/ListBlogs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            // Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<BlogDto> Blogs = response.Content.ReadAsAsync<IEnumerable<BlogDto>>().Result;
            Debug.WriteLine("Number of Blog received : ");
            Debug.WriteLine(Blogs.Count());


            return View(Blogs);
        }

        // GET: Blog/Details/5
        public ActionResult Details(int id)
        {
            //objective:  communicate with our Blog data api to reteieve one Blog
            // curl https://localhost:44313/api/BlogData/FindBlog/{id}


            string url = "BlogData/FindBlog/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine(" The response code is ");
            //Debug.WriteLine(response.StatusCode);

            BlogDto selectedBlog = response.Content.ReadAsAsync<BlogDto>().Result;
            //Debug.WriteLine("Blog received: ");
            //Debug.WriteLine(selectedBlog.BlogName);



            return View(selectedBlog);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Blog/New
        public ActionResult New()
        {
            // information about all Doctor in the system
            // GET: api/DoctorData/ListDoctors

            string url = "DoctorData/ListDoctors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> DoctorsOptions = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            return View(DoctorsOptions);
        }

        // POST: Blog/Create
        [HttpPost]
        public ActionResult Create(Blog Blog)
        {
            Debug.WriteLine("The json payload is : ");
            //objective: add a new Blog into our system using the API
            //curl -H "Content-type:application/json" -d @Blog.json  https://localhost:44313/api/BlogData/AddBlog
            string url = "BlogData/AddBlog";


            string jsonpayload = jss.Serialize(Blog);

            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateBlog ViewModel = new UpdateBlog();
            // the existing Blog information
            string url = "BlogData/FindBlog/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            BlogDto SelectedBlog = response.Content.ReadAsAsync<BlogDto>().Result;
            ViewModel.SelectedBlog = SelectedBlog;

            //All Doctor to choose from when updating this Blog
            url = "DoctorData/ListDoctors/";
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> DoctorOptions = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;

            ViewModel.DoctorOptions = DoctorOptions;

            return View(ViewModel);
        }

        // POST: Blog/Update/5
        [HttpPost]
        public ActionResult Update(int id, Blog Blog)
        {
            string url = "BlogData/UpdateBlog/" + id;
            string jsonpayload = jss.Serialize(Blog);
            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Blog/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "BlogData/FindBlog/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            BlogDto selectedBlog = response.Content?.ReadAsAsync<BlogDto>().Result;
            return View(selectedBlog);
        }

        // POST: Blog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "BlogData/DeleteBlog/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }
    }
}
