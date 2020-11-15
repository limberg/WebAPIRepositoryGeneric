using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DataAccessEF;
using WebApi_UnityOfWork.Models;

namespace WebApi_UnityOfWork.Common
{
    public static class GlobalAutoMapper
    {

        public static IMapper Mapper = null;

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDTO>(); //Get
                cfg.CreateMap<EmployeeDTO, Employee>(); //POST

                cfg.CreateMap<EmployeeUpdateDTO, Employee>();
            });

            Mapper = config.CreateMapper();
        }
    }
}