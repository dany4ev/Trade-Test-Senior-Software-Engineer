using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Trade_Test.Models;
using Trade_Test.Services;
using Trade_Test.Services.Interfaces;

using X.PagedList;

namespace Trade_Test_Web.Controllers {
    public class AdminController : Controller {

        private readonly IAdminService _adminService;

        public AdminController(
            IAdminService adminService
            )
        {
            _adminService = adminService;    
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.PageName = "Users List";

            List<User>? usersList = _adminService.GetUsers();

            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

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
                _ => usersList.OrderBy(s => s.UserName).ToList(),
            };
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(usersList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UserDetail(int id) {

            ViewBag.PageName = "User Detail";

            var character = _adminService.GetUser(id);

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
