using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppAPICo.Models;

namespace WpfAppAPICo.APIServices
{
    public interface IAPIHelper
    {
        Task<Token> Authentication(string username, string password);
        Task<List<Employee>> GetAllEmployees();
    }
}