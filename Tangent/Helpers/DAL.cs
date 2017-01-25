using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Tangent.Models;

namespace Tangent.Helpers
{
    public class DAL
    {
        public static List<Project> GetProjects(string token)
        {
            string jsonResponse;

            using (var client = new WebClient())
            {
                try
                {
                    client.BaseAddress = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/";
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.Authorization, "Token " + token);

                    client.UseDefaultCredentials = true;
                    jsonResponse = client.DownloadString(client.BaseAddress);

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Project>));
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonResponse));
                    stream.Position = 0;
                    List<Project> dataContractDetail = (List<Project>)jsonSerializer.ReadObject(stream);

                    return dataContractDetail;
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

        public static Project GetProjectById(string token, int projectId)
        {
            string jsonResponse;

            using (var client = new WebClient())
            {
                try
                {
                    client.BaseAddress = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + projectId + "/";
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.Authorization, "Token " + token);

                    client.UseDefaultCredentials = true;
                    jsonResponse = client.DownloadString(client.BaseAddress);

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Project));
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonResponse));
                    stream.Position = 0;
                    Project dataContractDetail = (Project)jsonSerializer.ReadObject(stream);

                    return dataContractDetail;
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

        public static void Create(string token, Project project)
        {

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("title", project.title);
            nameValueCollection.Add("description", project.description);
            nameValueCollection.Add("start_date", project.start_date);
            nameValueCollection.Add("end_date", project.end_date);
            nameValueCollection.Add("is_billable", project.is_billable.ToString());
            nameValueCollection.Add("is_active", project.is_active.ToString());

            using (var client = new WebClient())
            {
                try
                {
                    client.BaseAddress = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/";
                    client.Headers.Add(HttpRequestHeader.Authorization, "Token " + token);

                    client.UseDefaultCredentials = true;
                    client.UploadValues(client.BaseAddress, nameValueCollection);
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

        public static object Edit(string token, Project project)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("title", project.title);
            nameValueCollection.Add("description", project.description);
            nameValueCollection.Add("start_date", project.start_date);
            nameValueCollection.Add("end_date", project.end_date);
            nameValueCollection.Add("is_billable", project.is_billable.ToString());
            nameValueCollection.Add("is_active", project.is_active.ToString());
            //string jsonResponse;

            byte[] jsonResponse;

            using (var client = new WebClient())
            {
                try
                {
                    client.BaseAddress = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.pk + "/";
                    //client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.Authorization, "Token " + token);

                    client.UseDefaultCredentials = true;
                    jsonResponse = client.UploadValues(client.BaseAddress, "PUT", nameValueCollection);

                    string result = Encoding.UTF8.GetString(jsonResponse);
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(User));
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
                    stream.Position = 0;
                    User dataContractDetail = (User)jsonSerializer.ReadObject(stream);

                    return dataContractDetail;
                }
                catch (WebException ex)
                {
                    // Http Error
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse wrsp = (HttpWebResponse)ex.Response;
                        var statusCode = (int)wrsp.StatusCode;
                        var msg = wrsp.StatusDescription;
                        return msg;
                    }
                    else
                    {
                        return ex.Message;
                    }
                }
            }
        }

        public static void Delete(string token, int projectId)
        {
            string jsonResponse;

            using (var client = new WebClient())
            {
                try
                {
                    client.BaseAddress = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + projectId + "/";
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.Authorization, "Token " + token);

                    client.UseDefaultCredentials = true;
                    MemoryStream stream1 = new MemoryStream();
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Project));
                    stream1.Position = 0;
                    StreamReader sr = new StreamReader(stream1);
                    ser.WriteObject(stream1, projectId);
                    string data = sr.ReadToEnd();

                    jsonResponse = client.UploadString(client.BaseAddress, "DELETE", data);
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

        public static User Login(User user)
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
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(User));
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
                    stream.Position = 0;
                    User dataContractDetail = (User)jsonSerializer.ReadObject(stream);

                    return dataContractDetail;
                }
                catch (WebException ex)
                {
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
    }
}