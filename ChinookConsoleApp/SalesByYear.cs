using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChinookConsoleApp

{
    public class SalesByYear
    {
        public class SalesResultsList
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public int SalesTotal { get; set; }
        }
        public void ShowSales()
        {
            Console.Clear();
            Console.WriteLine("Type the year for employee sales report:");
            var year = Convert.ToInt32(Console.ReadLine());

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var result = connection.Query<SalesResultsList>("Select e.EmployeeId as Id, year(i.InvoiceDate), e.FirstName + e.LastName as FullName, Sum(Total) as SalesTotal " +
                                                                      "From Invoice as i " +
                                                                      "Join Customer as c on i.InvoiceId = c.CustomerId " +
                                                                      "Join Employee as e on c.SupportRepId = e.EmployeeId " +
                                                                      "Where year(i.InvoiceDate) = @ChosenYear " +
                                                                      "Group By e.FirstName, e.LastName, year(i.InvoiceDate), e.EmployeeId"
                                                                      new {ChosenYear = year });
                    Console.WriteLine("Waaa?");
                    foreach (var employee in result)
                    {
                        Console.WriteLine($"{employee.Id}.) {employee.FullName}: {employee.SalesTotal}");
                    }
                    Console.ReadLine();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.ReadLine();
                }
            }
        }
    }
}
