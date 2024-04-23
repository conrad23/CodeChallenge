using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        ReportingStructure GetReportingStructureByEmployeeId(string id);
    }
}
