using Microsoft.AspNetCore.Mvc;

namespace UserPage.Controllers
{
    [Route("{controller}/{action}")]
    public class UpdateViewController : Controller
    {
        
        public IActionResult UpdateView()
        {
            return View();
        }
    }
}
