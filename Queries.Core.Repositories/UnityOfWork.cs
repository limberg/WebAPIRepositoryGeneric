using DataAccessEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Core.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {

        protected readonly NorthwindEntities _context;
        public UnityOfWork(NorthwindEntities context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);

       
            //Products = new ProductRepository(_context);
        }

        public UnityOfWork()
        {
            _context = new NorthwindEntities();
            Employees = new EmployeeRepository(_context);
        }

        public IEmployeeRepository Employees { get; private set; }
        //public IProductRepository Products { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
