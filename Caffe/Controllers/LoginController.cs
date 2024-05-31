﻿using Caffe.Data;
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
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
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
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Username = data.Username,
                    Password = data.Password,
                    Name = data.Name,
                    role = "user"
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Signup");
            }
            return View(data);
        }
        [HttpGet]
        public JsonResult CheckUsername(string username)
        {
            bool exists = _context.Users.Any(u => u.Username == username);
            return Json(!exists); // Return true if username does not exist
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                                     .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

            if (user != null)
            {
                // Store user information in session
                HttpContext.Session.SetString("Name", user.Name);
                // You can store additional user info as needed

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
            // Clear the session
            HttpContext.Session.Clear();
            // Redirect to the login page or home page
            return RedirectToAction("Index", "Home");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }



}
