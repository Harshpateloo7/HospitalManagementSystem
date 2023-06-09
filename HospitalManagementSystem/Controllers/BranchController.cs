﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Models.ViewModels;
using System.Web.Script.Serialization;

namespace HospitalManagementSystem.Controllers
{
    public class BranchController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static BranchController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/api/");
        }
        // GET: Branch/List
        public ActionResult List()
        {
            //objective: communicate with our branch data api to retrive a list of branches
            //curl https://localhost:44313/api/BrancheData/ListBranches

            string url = "BrancheData/ListBranches";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<BranchDto> branches = response.Content.ReadAsAsync<IEnumerable<BranchDto>>().Result;
            Debug.WriteLine("Number of branches received: ");
            Debug.WriteLine(branches.Count());
            
            return View(branches);
        }

        // GET: Branch/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our branch data api to reterive one branch
            //curl https://localhost:44313/api/BrancheData/FindBranch/{id} 
            DetailsBranch ViewModel = new DetailsBranch();

            string url = "BrancheData/FindBranch/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            BranchDto SelectedBranch = response.Content.ReadAsAsync<BranchDto>().Result;
            Debug.WriteLine("branch receievd: ");
            Debug.WriteLine(SelectedBranch.BranchName);

            ViewModel.SelectedBranch = SelectedBranch;
            //Showcase information about Patient related to this Branch
            // Send a request to gather information about Patient related to particular Branch Id
            url = "PatientData/ListPatientsForBranch/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<PatientDto> RelatedPatients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;

            ViewModel.RelatedPatients = RelatedPatients;


            return View(ViewModel);
        }
        public ActionResult Error()
        {
            return View();
        }

        // GET: Branch/New
        public ActionResult New()
        {
            
            return View();
        }

        // POST: Branch/Create
        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            Debug.WriteLine("The json playload is: ");
            Debug.WriteLine(branch.BranchName);

            //objective: add a new branch into our system using the API
            //curl -H "Content-type:application/json" -d @branch.json https://localhoast:44313/api/BranchData/AddBranch

            string url = "BrancheData/AddBranch";
            
           
            string jsonpayload = jss.Serialize(branch);

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

        // GET: Branch/Edit/5
        public ActionResult Edit(int id)
        {
            // the existing Branch Information
            string url = "BrancheData/FindBranch/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            BranchDto selectedbranch = response.Content.ReadAsAsync<BranchDto>().Result;
            return View(selectedbranch);
        }

        // POST: Branch/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Branch branch)
        {
            string url = "BrancheData/UpdateBranch/" + id;
            string jsonpayload = jss.Serialize(branch);
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

        // GET: Branch/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "BrancheData/FindBranch/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            BranchDto selectedbranch = response.Content.ReadAsAsync<BranchDto>().Result;
            return View(selectedbranch);
        }

        // POST: Branch/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "BrancheData/DeleteBranch/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json"; 
            HttpResponseMessage response = client.PostAsync(url,content).Result;
            
            if(response.IsSuccessStatusCode)
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
