using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using XtendChallenge.Models;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services;
using XtendChallenge.Services.Interfaces;
using XtendChallenge.Services.Stubs;

namespace XtendChallenge.Controllers
{
    public class HomeController : Controller
    {
        private IClientService ClientService;
        private IFileService FileService;

        public HomeController()
        {
            var facilityRepository = new FacilityRepositoryInMem();
            IFacilityService FacilityService = new FacilityService(facilityRepository);
            IClientRepository clientRepository = new ClientRepositoryInMem();
            IAccountRepository accountRepository = new AccountRepositoryInMem(clientRepository, FacilityService);
            IAccountService accountService = new AccountService(accountRepository);
            FileService = new FileService(accountService, FacilityService);
            ClientService = new ClientService(clientRepository);
        }

        public IActionResult Index()
        {
            ViewData["clientList"] = ClientService.GetAllClients();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetReportList(int id)
        {
            var client = ClientService.GetClient(id);
            var files = FileService.GetExportFile(client);

            ViewData["Client"] = client;
            ViewData["files"] = files;
            return View();
        }

        public IActionResult GetReport(int clientId, string fileName)
        {
            var client = ClientService.GetClient(clientId);
            var files = FileService.GetExportFile(client);
            var stream = new MemoryStream();

            foreach (var file in files)
            {
                if (file.FileName == fileName)
                {
                    byte[] byteArray = Encoding.ASCII.GetBytes(file.Content);
                    stream.Write(byteArray, 0, byteArray.Count());
                    break;
                }
            }
            return File(stream.GetBuffer(), "application/txt", fileName);
        }
    }
}
