using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using DataAccessEF;
using Microsoft.Ajax.Utilities;
using Queries.Core.Repositories;
using WebApi_UnityOfWork.Common;
using WebApi_UnityOfWork.Models;

namespace WebApi_UnityOfWork.Controllers
{
    public class EmployeeController : ApiController
    {
        IUnityOfWork _unitOfWork;

        public EmployeeController(IUnityOfWork unityOfWork)
        {
            _unitOfWork = unityOfWork;
        }

        [HttpGet]
        [Route("api/GetAll", Name ="GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();

            if (employees == null)
                return NotFound();

            var mapper = GlobalAutoMapper.Mapper;

            IEnumerable<EmployeeDTO> employeeListDTO = mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return Ok(employeeListDTO);
        }

        [HttpGet]
        [Route("api/GetEmployeeByID/{Id}", Name ="GetEmployeeByID")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            var employee = _unitOfWork.Employees.Get(id);

            if (employee == null)
                return NotFound();

            EmployeeDTO employeeDTO = GlobalAutoMapper.Mapper.Map<EmployeeDTO>(employee);

            return Ok(employeeDTO);
        }

        [HttpPost]
        [Route("api/AddEmployee", Name = "AddEmployee")]
        public IHttpActionResult AddEmployee(EmployeeDTO employeeDTO)
        {
            Employee employee = GlobalAutoMapper.Mapper.Map<Employee>(employeeDTO);

            _unitOfWork.Employees.Add(employee);
            _unitOfWork.Complete();

            EmployeeDTO employeeDTORes = GlobalAutoMapper.Mapper.Map<EmployeeDTO>(employee);

            return CreatedAtRoute(nameof(GetEmployeeById), new { id = employeeDTORes.EmployeeID }, employeeDTORes);

        }

        [HttpPut]
        [Route("api/UpdateEmployee/{id}", Name = "UpdateEmployee")]
        public IHttpActionResult UpdateEmployee(int id, EmployeeUpdateDTO employeeUpdateDTO)
        {
            Employee employeeFromRepo = _unitOfWork.Employees.Get(id);

            if (employeeFromRepo == null)
                return NotFound();

            GlobalAutoMapper.Mapper.Map(employeeUpdateDTO, employeeFromRepo); //Update gemacht 

            _unitOfWork.Employees.Update(employeeFromRepo);
            _unitOfWork.Complete();

            var response = new HttpResponseMessage(HttpStatusCode.NoContent);
            //response.ReasonPhrase = "Die Daten wurden aktualiziert";

            return ResponseMessage(response);
        }
    }
}
