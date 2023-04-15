using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebFileExplorer.Enums;
using WebFileExplorer.Models;
using WebFileExplorer.Services;
using WebFileExplorer.Utility;

namespace WebFileExplorer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFileSystemStructure(ExcludedOptions excludedOptions, UnitOfInformation unitForDirs, UnitOfInformation unitForFiles)
        {
            ViewData["unitForDirs"] = unitForDirs;
            ViewData["unitForFiles"] = unitForFiles;
            DriveContentService driveContentService = new DriveContentService(excludedOptions);
            return View(driveContentService.GetAllDrivesContent());
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