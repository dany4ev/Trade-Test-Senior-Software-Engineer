using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Trade_Test.Models;
using Trade_Test.Services;
using Trade_Test.Services.Interfaces;

namespace Trade_Test.Controllers {
    public class CharacterController : Controller {
        private readonly ICharacterService _characterService;

        public CharacterController(
            ICharacterService characterService
            ) {
            _characterService = characterService;
        }

        public ActionResult Index() {

            var charactersList = _characterService.GetCharacters();

            return View(charactersList);
        }


        public ActionResult AddCharacter() {

            ViewBag.PageName = "Add Character";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCharacter([Bind("Id,Name,Vote")] Character character) {

            if (ModelState.IsValid) {
                try {
                    _characterService.AddCharacterAsync(character);
                }
                catch (DbUpdateConcurrencyException) {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(character);
        }

        public ActionResult UpdateCharacter(int id) {

            ViewBag.PageName = "Update Character";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser([Bind("Id,Name,Vote")] Character character) {

            if (ModelState.IsValid) {
                try {
                    _characterService.UpdateCharacterAsync(character);
                }
                catch (DbUpdateConcurrencyException) {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(character);
        }
                

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VoteForCharacter([Bind("Id,Vote")] Character character) {

            if (ModelState.IsValid) {
                try {
                    _characterService.VoteForCharacterAsync(character);
                }
                catch (DbUpdateConcurrencyException) {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(character);
        }
    }
}
