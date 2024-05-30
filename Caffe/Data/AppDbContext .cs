using Caffe.Models;
using Microsoft.EntityFrameworkCore;

namespace Caffe.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        } 
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
