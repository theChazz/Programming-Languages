using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//Manaully Created API using the following libraries
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace WebAPI1._01.Controllers
{
    //Test using postmen parameters as displayed by each method
    public class EmployeesController : ApiController
    {
        // Connection to SQL Server 
        SqlConnection connection = new SqlConnection(@"server=.; database=Angular1.00_DB; Integrated Security=true;");

        // GET api/Employees
        public string Get()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Employees;", connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dataTable);
            }
            else
            {
                return "No records found.";
            }
        }

        // GET api/Employees/5
        public string Get(int EmployeeID)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Employees WHERE EmployeeID = "+ EmployeeID +";", connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dataTable);
            }
            else
            {
                return "No record found.";
            }
        }

        // POST api/Employees
        [HttpPost]
        public string Post(string Email, string Salary)
        {   
            SqlCommand cmd = new SqlCommand("INSERT INTO Employees VALUES ('"+Email+"', '"+Salary+"')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            
            if (i == 1)
            {
                return $"Record added successfully. {Email} and {Salary}";
            }
            else
            {
                return "An error record.";
            }
        }

        // PUT api/values/5
        [HttpPut]
        public string Put(int EmployeeID, string Email, string Salary)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Employees SET Email = "+Email+", Salary = "+Salary+" WHERE EmployeeID = " + EmployeeID + ";", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i == 1)
            {
                return $"Record was updated successfully. {Email} and {Salary} ";
            }
            else
            {
                return "An error record.";
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public string Delete(int EmployeeID)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeID = " + EmployeeID + ";", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i == 1)
            {
                return $"Record was deleted successfully. {EmployeeID} ";
            }
            else
            {
                return "An error record.";
            }
        }
    }
}