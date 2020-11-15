﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Core.Repositories
{
    public interface IUnityOfWork:IDisposable
    {
        IEmployeeRepository Employees { get; }
        //IProductRepository Products { get; }

        int Complete();
    }
}
