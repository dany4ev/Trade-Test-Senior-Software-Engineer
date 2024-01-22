using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using Trade_Test.Services.Interfaces;

using Trade_Test_Web.Models;

namespace Trade_Test_Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly ICharacterService _characterService;

        public HomeController(
            ILogger<HomeController> logger,
            ICharacterService characterService) {
            _logger = logger;
            _characterService = characterService;
        }

        public IActionResult Index() {
            var charactersList = _characterService.GetCharacters();
            return View(charactersList);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
