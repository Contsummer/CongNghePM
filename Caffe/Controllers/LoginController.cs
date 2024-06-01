using Caffe.Data;
using Caffe.Models;
using Caffe.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Caffe.Controllers
{
    public class LoginController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
       
        

        public LoginController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult edit()
        {
            return View();
        }
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            var userrole = HttpContext.Session.GetString("role");
            ViewBag.Username = username;
            ViewBag.Userrole = userrole;
            return View();
        }
        public IActionResult signup()
        {
            var user = _context.Users.ToList();
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(User data)
        {


                User user = new User
                {
                    Username = data.Username,
                    Password = data.Password,
                    Name = data.Name,
                    role = "user",
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Signup");
            
        }
        [HttpGet]
        public JsonResult CheckUsername(string username)
        {
            bool exists = _context.Users.Any(u => u.Username == username);
            return Json(!exists); 
        }



        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
           
            var user = await _context.Users
                                     .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password  == request.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("role", user.role);

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            var userData = new User
            {
                Name = user.Name,
                role = user.role
            };

            return View(userData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User updatedData)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
            {
               
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
               
                return NotFound();
            }

           
            user.Name = updatedData.Name;
            user.Password = updatedData.Password;

          
            await _context.SaveChangesAsync();

           
            HttpContext.Session.SetString("Name", updatedData.Name);

            
            return RedirectToAction("Index", "Home");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }



}
