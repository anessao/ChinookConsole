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
    class DeleteEmployee
    {

        public void DeleteSelection()
        {
            new ListEmployees().List();
            Console.WriteLine("********************************");
            Console.WriteLine("Choose an employee by number:");
            var employeeId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Are you sure you want to delete? y or n");
            var verifiedDelete = Console.ReadLine();

            if (verifiedDelete == "y")
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
                {
                    var employeeDelete = connection.CreateCommand();
                    employeeDelete.CommandText = "DELETE FROM Employee" +
                                              " Where EmployeeId = @employeeId";

                    var employeeIdParameter = employeeDelete.Parameters.Add("@employeeId", SqlDbType.Int);
                    employeeIdParameter.Value = employeeId;

                    try
                    {
                        connection.Open();
                        var rowsAffected = employeeDelete.ExecuteNonQuery();
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
            else
            {
                Console.Clear();
                Console.WriteLine("Press enter to return to the menu.");
                Console.ReadLine();
            }
        }
    }
}
