using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Services;
using System.Diagnostics;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IData _data;

        public HomeController(ILogger<HomeController> logger, IData data)
        {
            //
            _logger = logger;
            _data = data;
        }

        public async Task<IActionResult> Index()
        {
            SearchModel data = new SearchModel();
            try
            {
                var response = await _data.GetAllMakes();
                if (response.IsSuccess && response.Data is not null)
                {
                    data.Makes = response.Data;
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to load car makes. Please try again.";
                }
                var currentYear = DateTime.Now.Year;
                data.Years = Enumerable.Range(1995, currentYear - 1995 + 2).Reverse().ToList();
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the page. Please try again.";
            }
            return View(data);

        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleTypeByMakeId(int makeId)
        {
            try
            {
                var data = new VehicleType();
                if (makeId is 0)
                    return BadRequest();
                var response = await _data.GetVehicleTypesForMake(makeId);
                if (response.IsSuccess && response.Data is not null)
                {
                    data = response.Data.FirstOrDefault();
                }
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while loading the page. Please try again.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(int makeId, int VehicleTypeId, int YearID)
        {
            try
            {
                var data = new CarModel();
                var response = await _data.GetModelsForMakeAndYear(makeId, YearID);
                if (response.IsSuccess && response.Data != null)
                {
                    data = response.Data.FirstOrDefault();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while loading the page. Please try again.");
            }
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
