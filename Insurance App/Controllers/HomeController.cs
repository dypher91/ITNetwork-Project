using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PojisteniApp.Data;
using PojisteniApp.Models;
using System.Diagnostics;

namespace PojisteniApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var insurances = _context.InsuranceInfo.ToList();
            var data = _context.InsuracePersonData.ToList();

            var pieChartData = insurances.Select(i => new { label = i.DescriptionOfInsurance, value = _context.Insurance.Where(p => p.DescriptionId == i.DescriptionId).Count() });
            var barChartData = data.Select(i => new { label = i.City, data = _context.PersonInsurance.Where(p => p.PersonId == i.PersonId).Select(p => p.ValueOfInsurance) });


            ViewBag.PieChartData = JsonConvert.SerializeObject(pieChartData);
            ViewBag.BarChartData = JsonConvert.SerializeObject(barChartData);


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