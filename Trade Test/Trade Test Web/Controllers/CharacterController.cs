﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Trade_Test.Models;
using Trade_Test.Services.Interfaces;

using X.PagedList;

namespace Trade_Test.Controllers {

    [Authorize(Roles = "Admin")]
    public class CharacterController : Controller {

        private readonly ICharacterService _characterService;

        public CharacterController(
            ICharacterService characterService
            ) {
            _characterService = characterService;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.PageName = "Disney Character List";
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CreatedDateSortParam = sortOrder == "CreatedDate" ? "createddate_desc" : "CreatedDate";
            ViewBag.ModifiedDateSortParam = sortOrder == "ModifiedDate" ? "modifieddate_desc" : "ModifiedDate";

            var charactersList = _characterService.GetCharacters();

            if (searchString != null) {
                page = 1;
            }
            else {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!string.IsNullOrEmpty(searchString)) {
                charactersList = charactersList.Where(s => s.Name.Contains(searchString)
                                       || s.Vote.ToString().Contains(searchString)
                                       || s.CreatedDateTime.ToString().Contains(searchString)
                                       || s.ModifiedDateTime.ToString().Contains(searchString)
                                       ).ToList();
            }

            charactersList = sortOrder switch {
                "name_desc" => charactersList.OrderByDescending(s => s.Name).ToList(),
                "CreatedDate" => charactersList.OrderBy(s => s.CreatedDateTime).ToList(),
                "createddate_desc" => charactersList.OrderByDescending(s => s.CreatedDateTime).ToList(),
                "ModifiedDate" => charactersList.OrderBy(s => s.ModifiedDateTime).ToList(),
                "modifieddate_desc" => charactersList.OrderByDescending(s => s.ModifiedDateTime).ToList(),
                _ => charactersList.OrderBy(s => s.Name).ToList(),
            };

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(charactersList.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult CharacterDetail(int id) {

            ViewBag.PageName = "Character Detail";

            var character = _characterService.GetCharacter(id);

            return View(character);
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
                    _characterService.AddCharacter(character);
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
        public ActionResult UpdateCharacter(int id, [Bind("Id,Name,Vote")] Character character) {

            if (ModelState.IsValid) {
                try {
                    _characterService.UpdateCharacter(character);
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
