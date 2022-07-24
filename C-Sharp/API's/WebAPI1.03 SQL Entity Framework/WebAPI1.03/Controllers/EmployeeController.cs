using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI1._03.Models;

namespace WebAPI1._03.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class EmployeeController : ApiController
    {
        private ADOEmployeeEntities db = new ADOEmployeeEntities();

        // Create
        [Route("api/Employee/AddEmployee")]
        [HttpPost]
        public ResponseModel PostEmployee(EmployeeModel eM)
        {
            ResponseModel responseModel = new ResponseModel();
            Employee employee = new Employee();

            employee.EmployeeID = eM.EmployeeID;
            employee.Email = eM.Email;
            employee.Salary = eM.Salary;

            if (employee != null)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                responseModel.ResponseCode = "201";
                responseModel.ResponseMessage = "Employee Added";
            }
            else
            {
                responseModel.ResponseCode = "100";
                responseModel.ResponseMessage = "Employee Not Added";
            }

            return responseModel;
        }

        // Read
        [Route("api/Employee/GetEmployees")]
        [HttpPost]
        public ResponseModel GetEmployees()
        {
            ResponseModel responseModel = new ResponseModel();
            List<Employee> employeeList = new List<Employee>();
            Employee employee = new Employee();

            employeeList = db.Employees.ToList();
            responseModel.ResponseCode = "200";
            responseModel.ResponseMessage = "Employees Fetched";
            responseModel.employeeList = employeeList;

            return responseModel;
        }

        // Read
        [Route("api/Employee/GetEmployee")]
        [HttpPost]
        public ResponseModel GetEmployee(EmployeeModel eM)
        {
            ResponseModel responseModel = new ResponseModel();
            Employee employee = new Employee();

            if (eM != null && eM.EmployeeID > 0)
            {
                employee = db.Employees.FirstOrDefault(x => x.EmployeeID == eM.EmployeeID);
                responseModel.employee = employee;
            }
            else
            {
                responseModel.ResponseCode = "404";
                responseModel.ResponseMessage = "Employees Not Found";
            }

            return responseModel;
        }

        // Updated
        [Route("api/Employee/UpdateEmployee")]
        [HttpPost]
        public ResponseModel PutEmployee(EmployeeModel eM)
        {
            ResponseModel responseModel = new ResponseModel();
            Employee employee = new Employee();

            employee.EmployeeID = eM.EmployeeID;
            employee.Email = eM.Email;
            employee.Salary = eM.Salary;

            if (employee != null)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                responseModel.ResponseCode = "201";
                responseModel.ResponseMessage = "Employee Updated";
            }
            else
            {
                responseModel.ResponseCode = "304";
                responseModel.ResponseMessage = "Employee Not Modified";
            }

            return responseModel;
        }

        //Deleted
        [Route("api/Employee/DeleteEmployee")]
        [HttpPost]
        public ResponseModel DeleteEmployee(EmployeeModel eM)
        {
            ResponseModel responseModel = new ResponseModel();
            Employee employee = new Employee();

            employee.EmployeeID = eM.EmployeeID;
            employee.Email = eM.Email;
            employee.Salary = eM.Salary;

            if (employee != null)
            {
                db.Entry(employee).State = EntityState.Deleted;
                db.SaveChanges();
                responseModel.ResponseCode = "200";
                responseModel.ResponseMessage = "Employee Added";
            }
            else
            {
                responseModel.ResponseCode = "404";
                responseModel.ResponseMessage = "Employee Not Found";
            }

            return responseModel;
        }
    }
}
