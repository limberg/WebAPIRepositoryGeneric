using DataAccessEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Core.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetTopEmployees(int count);
        IEnumerable<Employee> GetEmployeeWithProducts(int pageIndex, int pageSize);
    }
}
