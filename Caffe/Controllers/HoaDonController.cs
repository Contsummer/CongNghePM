using Caffe.Data;
using Caffe.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caffe.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        public HoaDonController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(int id)
        {

            var drink = _context.Drinks.FirstOrDefault(d => d.DrinkId == id);
            if (drink == null)
            {
                return RedirectToAction("Error");
            }
            var hoadon = _context.HoaDons.OrderByDescending(p => p.MaHoaDon).FirstOrDefault();
            if (hoadon == null)
            {
                hoadon = new HoaDon();
                hoadon.NgayTao = DateTime.Now;
                _context.HoaDons.Add(hoadon);
            }
            ChiTietHoaDon cthoadon = new ChiTietHoaDon();
            cthoadon.DrinkId = id;
            cthoadon.Drink = drink;
            cthoadon.MaHoaDon = hoadon.MaHoaDon;
            cthoadon.Soluong = 1;
            _context.ChiTietHoaDons.Add(cthoadon);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
