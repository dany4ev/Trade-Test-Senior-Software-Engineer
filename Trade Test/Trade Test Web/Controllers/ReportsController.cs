using Microsoft.AspNetCore.Mvc;

using Trade_Test.Services.Interfaces;

namespace Trade_Test_Web.Controllers {
    public class ReportsController : Controller {

        private readonly ICharacterService _characterService;

        public ReportsController(ICharacterService characterService) {
            _characterService = characterService;
        }

        public IActionResult Index() {
            ViewBag.PageName = "Reports";

            var charactersList = _characterService.GetCharacters();

            return View(charactersList);
        }
    }
}
