using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RockPub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Request(string request)
        {
            string connectionString = $"Server=(localdb)\\mssqllocaldb;Database=RockPub;Trusted_Connection=True;";
            // string connectionString = $"Server=DESKTOP-I75L3P7;Database=RockPub;Trusted_Connection=True;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(request, connection);
                    var result = new RequestViewModel();
                    var reader = command.ExecuteReader();
                    result.Displays = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result.Displays[i] = reader.GetName(i);
                    }

                    while (reader.Read())
                    {
                        string[] value = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            value[i] = reader.GetValue(i).ToString();
                        }

                        result.Result.Add(value);
                    }

                    return View(result);
                }
                catch (Exception)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
    }
}
