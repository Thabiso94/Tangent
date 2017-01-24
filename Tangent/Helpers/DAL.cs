using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Tangent.Models;

namespace Tangent.Helpers
{
    public class DAL
    {
        public static List<Project> GetProjects() {
            string jsonResponse;

            using (var client = new WebClient())
            {
                try
                {
                    client.BaseAddress = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/";
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.Authorization, "Token b7ec34e136bb6d28a4421e422e852b99cc834d17");

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

        public static Project GetProjectById(int projectId)
        {
            string jsonResponse;

            using (var client = new WebClient())
            {
                try
                {
                    client.BaseAddress = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + projectId + "/";
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.Authorization, "Token b7ec34e136bb6d28a4421e422e852b99cc834d17");

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
    }
}