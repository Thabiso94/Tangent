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
using System.Web.Routing;

namespace Tangent.Controllers
{
    public class ProjectController : Controller
    {
        private const string URL = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/";

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User", new RouteValueDictionary(new { message = "Please login first" }));
            }
            else
            {
                User user = (User)Session["User"];

                IEnumerable<Project> projects = DAL.GetProjects(user.token).OrderBy(p => p.pk);

                return View(projects);
            }
        }

        [HttpGet]
        public ActionResult ProjectTask(int projectId)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User", new RouteValueDictionary(new { message = "Please login first" }));
            }
            else
            {
                User user = (User)Session["User"];

                List<TaskSet> tasks = DAL.GetProjectById(user.token, projectId).task_set;

                return View(tasks);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User", new RouteValueDictionary(new { message = "Please login first" }));
            }
            else
            {
                User user = (User)Session["User"];

                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User", new RouteValueDictionary(new { message = "Please login first" }));
            }
            else
            {
                User user = (User)Session["User"];

                //TODO: RUN THE CREATION METHOD
                DAL.Create(user.token, project);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Delete(int projectId)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User", new RouteValueDictionary(new { message = "Please login first" }));
            }
            else
            {
                User user = (User)Session["User"];

                //TODO: RUN THE DELETION METHOD
                Project project = DAL.GetProjectById(user.token, projectId);

                return View(project);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int projectId)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User", new RouteValueDictionary(new { message = "Please login first" }));
            }
            else
            {
                User user = (User)Session["User"];

                //TODO: RUN THE DELETION METHOD
                DAL.Delete(user.token, projectId);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(int projectId)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User", new RouteValueDictionary(new { message = "Please login first" }));
            }
            else
            {
                User user = (User)Session["User"];

                Project project = DAL.GetProjectById(user.token, projectId);

                return View(project);
            }
        }

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User", new RouteValueDictionary(new { message = "Please login first" }));
            }
            else
            {
                User user = (User)Session["User"];

                //TODO: RUN THE DELETE METHOD
                object operation = DAL.Edit(user.token, project);

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
}