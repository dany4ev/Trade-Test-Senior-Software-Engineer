using Microsoft.AspNetCore.Mvc;

namespace Trade_Test_Web.Controllers {
    public class ReportsController : Controller {
        public IActionResult Index() {
            ViewBag.PageName = "Reports";
            return View();
        }
    }
}
