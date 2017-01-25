using System.Net.Http;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tangent.Models
{
    public class Project
    {
        static HttpClient client = new HttpClient();

        public int pk { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("Start Date")]
        public string start_date { get; set; }

        [DisplayName("End Date")]
        public string end_date { get; set; }

        [DisplayName("Billable")]
        public bool is_billable { get; set; }

        [DisplayName("Active")]
        public bool is_active { get; set; }
        public List<TaskSet> task_set { get; set; }
        public List<object> resource_set { get; set; }

        public async Task<Project> GetProductAsync(string path)
        {
            Project product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Project>();
            }

            return product;
        }
    }

    public class ProjectData
    {
        public int pk { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("Start Date")]
        public string start_date { get; set; }

        [DisplayName("End Date")]
        public object end_date { get; set; }

        [DisplayName("Billable")]
        public bool is_billable { get; set; }

        [DisplayName("Active")]
        public bool is_active { get; set; }
    }

    public class TaskSet
    {
        public int id { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("Due")]
        public string due_date { get; set; }

        [DisplayName("Estimated Hours")]
        public string estimated_hours { get; set; }

        [DisplayName("Project")]
        public int project { get; set; }
        public ProjectData project_data { get; set; }
    }
}