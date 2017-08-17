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
            new ListEmployees().List();
            Console.WriteLine("********************************");
            Console.WriteLine("Choose an employee by number:");
            var employeeId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Update Last Name to:");
            var employeeNewLastName = Console.ReadLine();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                var employeeUpdate = connection.CreateCommand();
                employeeUpdate.CommandText = "UPDATE Employee" +
                                          " SET LastName = @newLastName" +
                                          " Where EmployeeId = @employeeId";

                var employeeIdParameter = employeeUpdate.Parameters.Add("@employeeId", SqlDbType.Int);
                employeeIdParameter.Value = employeeId;

                var lastNameParameter = employeeUpdate.Parameters.Add("@newLastName", SqlDbType.VarChar);
                lastNameParameter.Value = employeeNewLastName;

                try
                {
                    connection.Open();
                    var rowsAffected = employeeUpdate.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected != 1 ? "Add Failed" : "Success!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }


                Console.WriteLine("Press enter to return to the menu.");
                Console.ReadLine();
            }
        }
    }
}
