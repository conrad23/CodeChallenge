
using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            //TODO: Internal Error upon saving

            // Arrange
            DateTime twoWeeksNotice = DateTime.Now.AddDays(14);

            var compensation = new Compensation()
            {
                EmployeeId = "8sa9ss97-s87a-s8ds-sine-8saf8s7a900s",
                Salary = 75000,
                EffectiveDate = twoWeeksNotice
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var createdCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(createdCompensation.EmployeeId);
            Assert.AreEqual(compensation.Salary, createdCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, createdCompensation.EffectiveDate);
        }

        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            var employeeId = "8sd7sas8-28jf-22sa-s9as-8sa75is5znis";
            var expectedSalary = 125000.00;
            var expectedEffectiveDate = DateTime.Parse("2024-04-22T08:30:00");

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedSalary, compensation.Salary);
            Assert.AreEqual(expectedEffectiveDate, compensation.EffectiveDate);
        }

    }
}
