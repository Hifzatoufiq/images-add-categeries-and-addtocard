using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication37.Data;
using WebApplication37.Models;

namespace WebApplication37.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Mycontext _mycontext;
        public HomeController(ILogger<HomeController> logger,Mycontext db)
        {
            _mycontext = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _mycontext.product.ToListAsync();
            return View(products);
        } 
        public async Task<IActionResult> logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("signin");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult signup(Register register)
        {
            if (ModelState.IsValid)
            {
                if (register.password == register.confrimpassword)
                {
                    var users = new Users();
                    users.Email = register.Email;
                    users.Role = "user";
                    users.password = register.password;
                    _mycontext.user.Add(users);

                    _mycontext.SaveChanges();
                    return RedirectToAction("Index");

                }
            }

                {

                    return View();
                }
            }
      
        public IActionResult signin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult signin(login Login)
        {


            var user = _mycontext.user.FirstOrDefault(x => x.Email == Login.Email && x.password == Login.password);
            if (user != null)
            {
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Role", user.Role);
                if (user.Role == "user")
                {
                    return RedirectToAction("Index");
                }
                else if (user.Role == "Admin")
                {
                    return RedirectToAction("privacy");
                }

            }
            else
            {
                ViewBag.InvalidEmailPass = "invalid email or password";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
