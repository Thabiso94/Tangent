using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tangent.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangent.Models;

namespace Tangent.Helpers.Tests
{
    [TestClass()]
    public class DALTests
    {
        public static string token = "b7ec34e136bb6d28a4421e422e852b99cc834d17";

        [TestMethod()]
        public void GetProjectsTest()
        {
            IEnumerable<Project> projects = DAL.GetProjects(token);

            Assert.IsNotNull(projects);
        }


        [TestMethod()]
        public void GetProjectByIdTest()
        {
            string projectTItle = "Stark Industries CRM";

            Project project = DAL.GetProjectById(token, 61);

            Assert.AreEqual(projectTItle, project.title);
        }

        [TestMethod()]
        public void LoginTest_Success()
        {
            string actual = DAL.Login(new User() { username = "admin1", password = "admin1" }).token;
            string expected = "b7ec34e136bb6d28a4421e422e852b99cc834d17";
            Assert.AreEqual(expected, actual);
        }
    }
}