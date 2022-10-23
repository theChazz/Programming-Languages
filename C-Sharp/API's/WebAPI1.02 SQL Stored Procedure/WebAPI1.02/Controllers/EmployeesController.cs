using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Additional Libraries
using WebAPI1._02.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Http.Cors;

namespace WebAPI1._02.Controllers
{
    // Source: https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class EmployeesController : ApiController
    {
        EmployeeModel eM = new EmployeeModel();

        // Connection to SQL Server Manually 
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);

        // GET api/<controller>
        [HttpGet]
        public List<EmployeeModel> Get()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("usp_SelectEmployees", connection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<EmployeeModel> listOfEmployees = new List<EmployeeModel>();

            if (dataTable.Rows.Count > 0)
            { 
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    EmployeeModel emp = new EmployeeModel();
                    emp.EmployeeID = Convert.ToInt32(dataTable.Rows[i]["EmployeeID"]);
                    emp.Email = dataTable.Rows[i]["Email"].ToString();
                    emp.Salary = dataTable.Rows[i]["Salary"].ToString();
                    listOfEmployees.Add(emp);

                }
                return listOfEmployees;
            }
            else
            {
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public EmployeeModel Get(int EmployeeID) //Test using parameter on postman
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("usp_SelectEmployee", connection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            EmployeeModel emp = new EmployeeModel();

            if (dataTable.Rows.Count > 0)
            {
                emp.EmployeeID = Convert.ToInt32(dataTable.Rows[0]["EmployeeID"]);
                emp.Email = dataTable.Rows[0]["Email"].ToString();
                emp.Salary = dataTable.Rows[0]["Salary"].ToString();
                 
                return emp;
            }
            else
            {
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post(EmployeeModel eM) //Test using raw Json data
        {
            if (eM != null)
            {
                SqlCommand cmd = new SqlCommand("usp_AddEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", eM.Email);
                cmd.Parameters.AddWithValue("@Salary", eM.Salary);

                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();

                if (i > 0)
                {
                    return "Record created";
                }
                else
                {
                    return "Error";
                }
            }
            else
            {
                return "Error Model";
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public string Put(EmployeeModel eM) //Test using raw Json data
        {
            string msg = "";
            SqlCommand cmd = new SqlCommand("usp_UpdateEmployee", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", eM.EmployeeID);
            cmd.Parameters.AddWithValue("@Email", eM.Email);
            cmd.Parameters.AddWithValue("@Salary", eM.Salary);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                msg = "Employee record deleted";
            }
            else
            {
                msg = "Error";
            }
            return msg;
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public string Delete(int EmployeeID) //Test using parameters on postman
        {
            string msg = "";
            SqlCommand cmd = new SqlCommand("usp_DeleteEmployee", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if ( i > 0 )
            {
                msg = "Employee record deleted";
            }
            else
            {
                msg = "Error";
            }
            return msg;
        }
    }
}