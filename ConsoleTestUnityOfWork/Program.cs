using DataAccessEF;
using Queries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestUnityOfWork
{
    class Program
    {
        static void Main(string[] args)
        {

            using(UnityOfWork unityOfWork = new UnityOfWork(new NorthwindEntities()))
            {
                //Example 1
                var employee = unityOfWork.Employees.Get(1);

                Console.WriteLine($"Firs Name: {employee.FirstName}");

                //Example 2
                var employees = unityOfWork.Employees.GetTopEmployees(4);


                unityOfWork.Complete(); //Save alle änderungen in DB
            }
        }
    }
}
