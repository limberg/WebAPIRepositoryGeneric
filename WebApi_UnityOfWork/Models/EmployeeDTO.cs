using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_UnityOfWork.Models
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}