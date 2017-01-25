using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tangent.Helpers;
using Tangent.Models;

namespace Tangent.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (Request.QueryString.Count > 0)
            {
                ViewBag.Message = Uri.UnescapeDataString(Request.QueryString["message"]);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("username", user.username);
            nameValueCollection.Add("password", user.password);
            byte[] jsonResponse;

            using (var client = new WebClient())
            {
                try
                {
                    client.BaseAddress = "http://userservice.staging.tangentmicroservices.com/api-token-auth/";

                    client.UseDefaultCredentials = true;
                    jsonResponse = client.UploadValues(client.BaseAddress, nameValueCollection);

                    string result = Encoding.UTF8.GetString(jsonResponse);
                    if (result != null)
                    {
                        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(User));
                        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
                        stream.Position = 0;
                        User dataContractDetail = (User)jsonSerializer.ReadObject(stream);

                        Session["User"] = dataContractDetail;
                        return RedirectToAction("Index", "Project");
                    }
                    else
                    {
                        ViewBag.Message = "The username or password is incorrect";
                        return View();
                    }

                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse wrsp = (HttpWebResponse)ex.Response;
                        var statusCode = (int)wrsp.StatusCode;
                        var msg = wrsp.StatusDescription;
                        ViewBag.Message = "The username or password is incorrect";
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "The username or password is incorrect";
                        return View();
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Logout() {
            Session["User"] = null;

            return RedirectToAction("Login", "User");
        }
    }
}