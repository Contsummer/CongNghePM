using Caffe.Data;
using Caffe.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;
using Caffe.Models.DTO;
using Microsoft.AspNetCore.Hosting;

namespace Caffe.Controllers
{
    public class MenuController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        public MenuController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            var drink = _context.Drinks.ToList();
            return View(drink);
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(DrinkDTO data)
        {
            if (data.ImageFile != null && data.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Image");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + data.ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await data.ImageFile.CopyToAsync(fileStream);
                }
                Drink nuoc = new Drink();
                nuoc.Name = data.Name;
                nuoc.Price = data.Price;
                nuoc.descripton = data.Description;
                nuoc.imagePath = uniqueFileName;
                _context.Drinks.Add(nuoc);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }
       
        public IActionResult Edit(int id)
        {
            var drink = _context.Drinks.FirstOrDefault(d => d.DrinkId == id);
            DrinkDTO drinkDto = new DrinkDTO();
            drinkDto.id = drink.DrinkId;
            drinkDto.Description = drink.descripton;
            drinkDto.Price=drink.Price;
            drinkDto.Name=drink.Name;
            return View(drinkDto);


        }

        [HttpPost]
        public async Task<ActionResult> Edit(DrinkDTO data)
        {
            var drink = _context.Drinks.FirstOrDefault(d => d.DrinkId == data.id);
            if (drink == null)
            {
                return RedirectToAction("Error");

            }
            else
            {
                if (data.ImageFile != null)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Image");

                    string imagePath = Path.Combine(uploadsFolder, drink.imagePath);
                    System.IO.File.Delete(imagePath);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + data.ImageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.ImageFile.CopyToAsync(fileStream);
                    }

                    drink.imagePath = uniqueFileName;

                }

                drink.Name=data.Name;
                drink.Price=data.Price;
                drink.descripton = data.Description;

                _context.SaveChanges();
                return RedirectToAction("Index");

            }

        }
        public IActionResult Delete(int id)
        {
            var drink = _context.Drinks.FirstOrDefault(d => d.DrinkId == id);
            if (drink == null)
            {
               return  RedirectToAction("Error");
            }
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Image");
            string imagePath = Path.Combine(uploadsFolder, drink.imagePath);
            System.IO.File.Delete(imagePath);
            _context.Drinks.Remove(drink);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}
