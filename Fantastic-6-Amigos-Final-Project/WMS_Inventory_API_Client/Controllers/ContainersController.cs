using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Principal;
using Container = WMS_Inventory_API_Client.Models.Container;
using WMS_Inventory_API_Client.Services.Interfaces;
using Spire.Pdf;
using WMS_Inventory_API_Client.Models;
using System.Security.Cryptography.X509Certificates;
using Spire.Barcode;

namespace WebMVC_API_Client.Controllers
{
    public class ContainerController : Controller
    {
        private IContainerService? _service;
        private IStorageLocationService? _serviceStorageLocation;

        private static readonly HttpClient client = new HttpClient();

        private string requestUri = "https://localhost:7153/api/Containers/";

        public ContainerController(IContainerService service, IStorageLocationService serviceStorageLocation)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _serviceStorageLocation = serviceStorageLocation ?? throw new ArgumentNullException(nameof(serviceStorageLocation));

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("User-Agent", "Jim's API");
        }

        // Example: https://localhost:7153/api/Containers

        public async Task<IActionResult> Index()
        {

            var response = await _service.FindAll();


            return View(response);
        }

        public async Task<IActionResult> Account(int id)
        {
            var response = await _service.Account(id);

            //PdfDocument doc = new PdfDocument();
            //doc.LoadFromFile(@"C:/Users/willi/source/LoremIpsum.pdf");
            //doc.Print();

            return View(response);
        }
        
        
        // GET: Container/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var container = await _service.FindOne(id);

            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        public async Task<IActionResult> Barcode(int id)
        {

            var container = await _service.FindOne(id);
            TempData["containerId"] = id;
            //BarcodeSettings containerCode = new BarcodeSettings();
            //containerCode.Type = BarCodeType.Code39;
            //containerCode.Data = "*" + id.ToString() + "*";
            //BarCodeGenerator bg = new BarCodeGenerator(containerCode);
            //bg.GenerateImage().Save("barcode.png");

            return View(container);
        }


        // GET: Container/Create
        public async Task<IActionResult> Create()
        {
            var response = await _serviceStorageLocation.FindAll();
            ViewData["StorageLocationId"] = new SelectList(response, "Id", "LocationName");
            return View();
        }

        // POST: Container/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Type,Description,StorageLocationID")] Container container)
        public async Task<IActionResult> Create(Container container)
        {
            container.Id = null;
            var resultPost = await client.PostAsync<Container>(requestUri, container, new JsonMediaTypeFormatter());

            return RedirectToAction(nameof(Index));
        }

        // GET: Container/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var container = await _service.FindOne(id);
            if (container == null)
            {
                return NotFound();
            }

            int acctId = (int)container.StorageLocation.AccountId;
            var response = await _serviceStorageLocation.Account(acctId);
            ViewData["StorageLocationId"] = new SelectList(response, "Id", "LocationName");

            return View(container);
        }

        // POST: Container/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Description,StorageLocationID,StorageLocation")] Container container)
        public async Task<IActionResult> Edit(int id, Container container)
        {
            if (id != container.Id)
            {
                return NotFound();
            }

            var resultPut = await client.PutAsync<Container>(requestUri + container.Id.ToString(), container, new JsonMediaTypeFormatter());
            return RedirectToAction("Details", "Container", new { id = container.Id });
        }

        // GET: Container/Delete/5
        public async Task<IActionResult> Delete(int id)

        {
            var container = await _service.FindOne(id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // POST: Container/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultDelete = await client.DeleteAsync(requestUri + id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}