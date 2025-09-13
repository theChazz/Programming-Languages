using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI1._03.Models
{
    public class ResponseModel
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public Employee employee { get; set; }
        public List<Employee> employeeList { get; set; }
    }
}