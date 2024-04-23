using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        public ReportingStructure GetReportingStructureByEmployeeId(string id)
        {
            var employee = _employeeService.GetById(id);

            if (employee != null)
            {
                var reportingStructure = new ReportingStructure();

                reportingStructure.Employee = employee;
                reportingStructure.NumberOfReports = CountReports(employee);

                return reportingStructure;
            }

            return null;
        }

        private int CountReports(Employee employee)
        {
            int reportingEmployees = 0;

            // Immediately return 0 if employee has no direct reports
            if (employee.DirectReports == null)
            {
                return reportingEmployees;
            }

            List<Employee> currentLevelOfEmployees = new List<Employee>();

            // Load first level of employees
            foreach (Employee reportingEmployee in employee.DirectReports)
            {
                currentLevelOfEmployees.Add(reportingEmployee);
            }

            // Continuously check employees until there are no more levels
            while (currentLevelOfEmployees.Count > 0)
            {
                reportingEmployees += currentLevelOfEmployees.Count;

                List<Employee> nextLevelOfEmployees = new List<Employee>();

                // Check DirectReports for each employee on current level
                foreach (Employee currentEmployee in currentLevelOfEmployees)
                {
                    // Update employee information to ensure employee information
                    Employee populatedCurrentEmployee = new Employee();
                    populatedCurrentEmployee = _employeeService.GetById(currentEmployee.EmployeeId);

                    // If a deeper level for the employee exists, add it to list to check
                    if (currentEmployee.DirectReports != null)
                    {
                        nextLevelOfEmployees.AddRange(populatedCurrentEmployee.DirectReports);
                    }

                    currentLevelOfEmployees = nextLevelOfEmployees;
                }
            }

            return reportingEmployees;
        }
    }
}
