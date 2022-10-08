using BookStore.Data.Context;
using BookStore.Data.Entities;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookStore.Web.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
 
        }

        public async Task <IActionResult>  Index()
        {
 
            return View();
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
}