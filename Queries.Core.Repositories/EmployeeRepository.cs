using DataAccessEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Core.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(NorthwindEntities context):base(context)
        {
            
        }
        public IEnumerable<Employee> GetEmployeeWithProducts(int pageIndex, int pageSize)
        {
            return NorthwindContext.Employees
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Employee> GetTopEmployees(int count)
        {
           return NorthwindContext.Employees.OrderByDescending(e => e.EmployeeID).Take(count).ToList();
        }

        public NorthwindEntities NorthwindContext { get { return Context as NorthwindEntities; } }
    }
}
