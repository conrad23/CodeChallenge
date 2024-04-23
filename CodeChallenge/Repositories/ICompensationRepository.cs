using CodeChallenge.Models;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation Create(Compensation compensation);
        Compensation GetByEmployeeId(string id);
        Task SaveAsync();
    }
}
