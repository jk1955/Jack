using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Diagnostics;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WMS_Inventory_API_Client.Models;
using WMS_Inventory_API_Client.Services.Interfaces;

namespace WMS_Inventory_API_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAccountService? _service;
        private static readonly HttpClient client = new HttpClient();
        private string requestUri = "https://localhost:7153/api/Accounts/";



        public HomeController(ILogger<HomeController> logger, IAccountService service)
        {

            _service = service ?? throw new ArgumentNullException(nameof(service));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Jim's API");

            _logger = logger;
        }

        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public int? acctId;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email)
        {
            TempData["OK"] = false;
            TempData["message"] = "";

            if (email != null)
            {
                var account = await _service.FindEmail(email);
                if (account == null)
                {
                    TempData["message"] = "Email Not Found";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["OK"] = true;
                    acctId = account.Id;

                    return RedirectToAction(nameof(MainPage));
                }
            }
            else
            {
                TempData["message"] = "Email Not Provided";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}