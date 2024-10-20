using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorbikeStore.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MotorbikeStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context; // Thêm ApplicationDbContext

        // Constructor nhận ApplicationDbContext
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action Index để hiển thị danh sách xe máy
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách xe máy từ cơ sở dữ liệu
            var motorbikes = await _context.Motorbikes.ToListAsync();

            // Truyền danh sách xe máy vào view
            return View(motorbikes);
        }

        // Action Details để hiển thị chi tiết một xe máy
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorbike = await _context.Motorbikes
                .FirstOrDefaultAsync(m => m.MotorbikeId == id);

            if (motorbike == null)
            {
                return NotFound();
            }

            return View(motorbike);
        }

        public IActionResult Privacy()
        {
            return View();
        }

 


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
