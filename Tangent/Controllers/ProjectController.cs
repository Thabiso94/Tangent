using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using System.Net.Http;
using System.Net.Http.Headers;
using Tangent.Models;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Tangent.Helpers;

namespace Tangent.Controllers
{
    public class ProjectController : Controller
    {
        private const string URL = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/";

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Project> projects = DAL.GetProjects().OrderBy(p => p.pk);

            return View(projects);
        }

        [HttpGet]
        public ActionResult ProjectTask(int projectId)
        {
            List<TaskSet> tasks = DAL.GetProjectById(projectId).task_set;

            return View(tasks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            //TODO: RUN THE CREATION METHOD
            DAL.Create(project);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int projectId)
        {
            //TODO: RUN THE DELETION METHOD
            Project project = DAL.GetProjectById(projectId);

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int projectId)
        {
            //TODO: RUN THE DELETION METHOD
            DAL.Delete(projectId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [HandleError]
        public ActionResult Edit(int projectId)
        {
            Project project = DAL.GetProjectById(projectId);

            return View(project);
        }

        [HttpPost]
        [HandleError]
        public ActionResult Edit(Project project)
        {
            //TODO: RUN THE DELETE METHOD
            object operation = DAL.Edit(project);

            if (operation.GetType() == typeof(string))
            {
                return RedirectToAction("Error", "Error", new { message = operation });
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}