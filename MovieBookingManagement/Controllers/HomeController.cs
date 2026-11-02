using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using MovieBookingManagement.Models;

namespace MovieBookingManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string message;

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    message = "Database Connection Successful!";
                }
            }
            catch (Exception ex)
            {
                message = "Database Connection Failed: " + ex.Message;
            }

            ViewBag.Message = message;

            return View();
        }
    }
}