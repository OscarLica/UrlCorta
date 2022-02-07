using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NETChallenge.Models;
using NETChallenge.Service;
using NETChallenge.ViewModels;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETChallenge.Controllers
{
    [Authorize]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private IServiceUrl ServiceUrl;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IServiceUrl serviceUrl)
        {
            this.browserDetector = browserDetector;
            _logger = logger;

            ServiceUrl = serviceUrl;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Urls = await ServiceUrl.GetAll(),
                NewUrl = new Url { Usuario = User.Identity.Name, Fecha = DateTime.Now },
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Visit(string url)
        {

            // obtenemos la url por su token
            var urlResult = await ServiceUrl.GetByToken(url);
            if (urlResult is null) return NotFound();


            // registramos el click a la url
            await ServiceUrl.CountClick(urlResult);

            return Redirect(urlResult.OriginalUrl);

        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeViewModel homeViewModel)
        {
            await ServiceUrl.Create(homeViewModel.NewUrl);
            return RedirectToAction(nameof(HomeController.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Show(string url)
        {

            var urlResult = await ServiceUrl.GetByToken(url);

            return View(new ShowViewModel
            {
                Url = urlResult,
                DailyClicks = await ServiceUrl.GetDailyClick(urlResult.Id),
                BrowseClicks = await ServiceUrl.GetByBrowser(urlResult.Id),
                PlatformClicks = await ServiceUrl.GetByPlatforms(urlResult.Id),
            });

        }

    }
}
