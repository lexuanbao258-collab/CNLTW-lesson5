using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lesson5.Models;
using Npgsql;

namespace Lesson5.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Weekend()
    {
        return View();
    }

    public IActionResult Products()
    {
        var products = new List<Product>();
        var connectionString = _configuration.GetConnectionString("PostgreSQL");
        var sql = "SELECT id, name, price FROM product ORDER BY id;";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (var command = new NpgsqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    };

                    products.Add(product);
                }
            }
        }

        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
