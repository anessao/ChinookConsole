using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookConsoleApp
{
    class UpdateEmployee
    {
        public void Update()
        {
            var employeeList = new ListEmployees();
            var selectedEmployee = employeeList.List("Pick an employee to update:");
            Console.WriteLine("New Last Name:");
            var newLastName = Console.ReadLine();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {

                try
                {
                    connection.Open();

                    var rowsAffected = connection.Execute("Update Employee " +
                                                            "SET LastName = @NewLastName " +
                                                          "Where EmployeeId = @EmployeeId",
                        new { EmployeeId = selectedEmployee, NewLastName = newLastName });

                    Console.WriteLine(rowsAffected != 1 ? "Add Failed" : "Success!");

                    Console.WriteLine("Press enter to return to the menu");
                    Console.ReadLine();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
