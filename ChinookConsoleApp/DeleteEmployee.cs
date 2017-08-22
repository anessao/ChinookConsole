using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChinookConsoleApp
{
    public class DeleteEmployee
    {
        public void Delete()
        {
            var employeeList = new ListEmployees();
            var firedEmployee = employeeList.List("Pick an employee to transition:");
        

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                connection.Open();

                try
                {
                    var rowsAffected = connection.Execute("Delete From Employee " +
                                                          "Where EmployeeId = @EmployeeId",
                                                            new { EmployeeId = firedEmployee });

                    Console.WriteLine(rowsAffected != 1 ? "Add Failed" : "Success!");

                    Console.WriteLine("Press enter to return to the menu");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
