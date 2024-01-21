using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Trade_Test.Models;
using Trade_Test.Services.Interfaces;

namespace Trade_Test_Web.Controllers {
    public class AdminController : Controller {

        private readonly IAdminService _adminService;

        public AdminController(
            IAdminService adminService
            )
        {
            _adminService = adminService;    
        }

        public ActionResult Index() {

            var usersList = _adminService.GetUsers();

            return View(usersList);
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
                    _adminService.AddUserAsync(user);
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
                    _adminService.UpdateUserAsync(user);
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
