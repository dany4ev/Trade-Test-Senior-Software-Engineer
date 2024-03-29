﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Trade_Test.Models;
using Trade_Test.Services.Interfaces;
using Trade_Test.Utilities.Extensions;

using X.PagedList;

namespace Trade_Test_Web.Controllers {

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller {

        private readonly IAdminService _adminService;

        public AdminController(
            IAdminService adminService
            ) {
            _adminService = adminService;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.PageName = "Users List";
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EmailSortParam = sortOrder == "Email" ? "email_desc" : "email";
            ViewBag.PhoneNumberSortParam = sortOrder == "PhoneNumber" ? "phonenumber_desc" : "phonenumber";

            var usersList = _adminService.GetUsers();

            if (searchString != null) {
                page = 1;
            }
            else {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!string.IsNullOrEmpty(searchString)) {
                usersList = usersList.Where(s => s.UserName.Contains(searchString)
                                       || s.PhoneNumber.Contains(searchString)
                                       || s.Email.Contains(searchString)
                                       ).ToList();
            }

            usersList = sortOrder switch {
                "name_desc" => usersList.OrderByDescending(s => s.UserName).ToList(),
                "email" => usersList.OrderBy(s => s.Email).ToList(),
                "email_desc" => usersList.OrderByDescending(s => s.Email).ToList(),
                "phonenumber" => usersList.OrderBy(s => s.PhoneNumber).ToList(),
                "phonenumber_desc" => usersList.OrderByDescending(s => s.PhoneNumber).ToList(),
                _ => usersList.OrderBy(s => s.UserName).ToList(),
            };

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(usersList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UserDetail(string id) {

            ViewBag.PageName = "User Detail";

            var character = _adminService.GetUser(id.AsGuid());

            return View(character);
        }

        public ActionResult AddUser() {

            ViewBag.PageName = "Add User";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser([Bind("Id,UserName,Email,PhoneNumber")] User user) {

            if (ModelState.IsValid) {
                try {
                    _adminService.AddUser(user);
                }
                catch (DbUpdateConcurrencyException) {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        public ActionResult UpdateUser(int id) {

            ViewBag.PageName = "Update User";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser([Bind("Id,UserName,Email,PhoneNumber")] User user) {

            if (ModelState.IsValid) {
                try {
                    _adminService.UpdateUser(user);
                }
                catch (DbUpdateConcurrencyException) {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }
    }
}
