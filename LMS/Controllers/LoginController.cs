using Microsoft.AspNetCore.Mvc;
using LMS.Models;
using Microsoft.AspNetCore.Http;
namespace LMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountrepo _accountRepo;

        public LoginController(IAccountrepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public IActionResult Index(string username,string password)
        {
           
            if (username != null || password != null)
            {
                HttpContext.Session.SetString("userName", username);
                var user = _accountRepo.getUserByName(username);
                if (user == null)
                {
                    ViewBag.Message = "Invalid Credentials!! Please Try Again";
                }
                else if (username.Equals("admin") && password.Equals("admin"))
                {
                    return RedirectToAction("Index","Admin");
                }
                else if (username.Equals(user.UserName) && password.Equals(user.Password))
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.Message = "Invalid Credentials!! Please Try Again";
                }
            }
            return View();
        }
    }
}
