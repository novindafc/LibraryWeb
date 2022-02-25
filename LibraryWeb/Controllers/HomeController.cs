using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryWeb.Models;
using LibraryWeb.Service;

namespace LibraryWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookLogRepositoryService _bookLogRepository;

        public HomeController(ILogger<HomeController> logger, IBookLogRepositoryService bookLogRepository)
        {
            _logger = logger;
            _bookLogRepository = bookLogRepository;
        }

        public async Task<IActionResult> Index()
        {
            var booklog = await  _bookLogRepository.GetBookLog();
            return View(booklog);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        
    }
}