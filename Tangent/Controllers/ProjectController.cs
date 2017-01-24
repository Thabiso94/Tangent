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

namespace Tangent.Controllers
{
    public class ProjectController : Controller
    {
        private const string URL = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/";

        // GET: Project
        public ActionResult Index()
        {
            string jsonResponse;

            List<object> list = new List<object>();
            using (var client = new WebClient())
            {
                try
                {
                    client.BaseAddress = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/";
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.Authorization, "Token b7ec34e136bb6d28a4421e422e852b99cc834d17");
                    // HTTP GET
                    client.UseDefaultCredentials = true;
                    jsonResponse = client.DownloadString(client.BaseAddress);

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Project>));
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonResponse));
                    stream.Position = 0;
                    List<Project> dataContractDetail = (List<Project>)jsonSerializer.ReadObject(stream);

                    return View(dataContractDetail);
                }
                catch (WebException ex)
                {
                    // Http Error
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse wrsp = (HttpWebResponse)ex.Response;
                        var statusCode = (int)wrsp.StatusCode;
                        var msg = wrsp.StatusDescription;
                        throw new HttpException(statusCode, msg);
                    }
                    else
                    {
                        throw new HttpException(500, ex.Message);
                    }
                }
            }
        }

        public ActionResult ProjectTask(int projectId) {

        }
    }
}