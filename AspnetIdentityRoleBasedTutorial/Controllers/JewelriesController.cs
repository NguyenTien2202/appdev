
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspnetIdentityRoleBasedTutorial.Data;
using AspnetIdentityRoleBasedTutorial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspnetIdentityRoleBasedTutorial.Controllers
{
    public class JewelriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public JewelriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Jewelries/AdminIndex
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex(string jewelryType, string category, string jewelryName, int? page)
        {
            ViewData["JewelryTypes"] = new SelectList(_context.JewelryTypes, "TypeName", "TypeName");
            ViewData["Categories"] = new SelectList(_context.Category, "CategoryName", "CategoryName");

            const int pageSize = 12;

            var query = _context.Jewelries
                .Include(j => j.Category)
                .Include(j => j.JewelryType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(jewelryType))
            {
                query = query.Where(j => j.JewelryType.TypeName.Contains(jewelryType));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(j => j.Category.CategoryName.Contains(category));
            }

            if (!string.IsNullOrEmpty(jewelryName))
            {
                query = query.Where(j => j.JewelryName.Contains(jewelryName));
            }

            // Sắp xếp theo thứ tự giảm dần của trường Id
            query = query.OrderByDescending(j => j.Id);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            page ??= 1;
            page = Math.Min(Math.Max(1, (int)page), totalPages);

            var jewelries = await query.Skip((page.Value - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["SelectedJewelryType"] = jewelryType;
            ViewData["SelectedCategory"] = category;
            ViewData["SelectedJewelryName"] = jewelryName;

            return View(jewelries);
        }


        public async Task<IActionResult> Index(string jewelryType, string category, string jewelryName, int? page)
        {
            ViewData["JewelryTypes"] = new SelectList(_context.JewelryTypes, "TypeName", "TypeName");
            ViewData["Categories"] = new SelectList(_context.Category, "CategoryName", "CategoryName");

            const int pageSize = 12;

            var query = _context.Jewelries
                .Include(j => j.Category)
                .Include(j => j.JewelryType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(jewelryType))
            {
                query = query.Where(j => j.JewelryType.TypeName.Contains(jewelryType));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(j => j.Category.CategoryName.Contains(category));
            }

            if (!string.IsNullOrEmpty(jewelryName))
            {
                query = query.Where(j => j.JewelryName.Contains(jewelryName));
            }

            // Sắp xếp theo thứ tự giảm dần của trường Id
            query = query.OrderByDescending(j => j.Id);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            page ??= 1;
            page = Math.Min(Math.Max(1, (int)page), totalPages);

            var jewelries = await query.Skip((page.Value - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["SelectedJewelryType"] = jewelryType;
            ViewData["SelectedCategory"] = category;
            ViewData["SelectedJewelryName"] = jewelryName;

            return View(jewelries);
        }


        // GET: Jewelries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jewelries == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelries
                .Include(j => j.Category)
                .Include(j => j.JewelryType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jewelry == null)
            {
                return NotFound();
            }

            return View(jewelry);
        }

        // GET: Jewelries/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName");
            ViewData["JewelryTypeId"] = new SelectList(_context.JewelryTypes, "Id", "TypeName");
            return View();
        }

        // POST: Jewelries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JewelryName,Description,Price,Image,CategoryId,JewelryTypeId")] Jewelry jewelry, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Nếu có ảnh mới, lưu nó và cập nhật tên tệp ảnh
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", ImageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    jewelry.Image = ImageFile.FileName;
                }

                _context.Add(jewelry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminIndex));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", jewelry.CategoryId);
            ViewData["JewelryTypeId"] = new SelectList(_context.JewelryTypes, "Id", "TypeName", jewelry.JewelryTypeId);
            return View(jewelry);
        }

        // GET: Jewelries/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jewelries == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelries.FindAsync(id);
            if (jewelry == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", jewelry.CategoryId);
            ViewData["JewelryTypeId"] = new SelectList(_context.JewelryTypes, "Id", "TypeName", jewelry.JewelryTypeId);

            // Lưu trữ tên tệp ảnh hiện tại để sử dụng trong phương thức POST
            TempData["CurrentImage"] = jewelry.Image;

            return View(jewelry);
        }

        // POST: Jewelries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JewelryName,Description,Price,Image,CategoryId,JewelryTypeId")] Jewelry jewelry, IFormFile ImageFile)
        {
            if (id != jewelry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra xem người dùng đã chọn ảnh mới hay không
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        // Nếu có ảnh mới, lưu nó và cập nhật tên tệp ảnh
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", ImageFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }
                        jewelry.Image = ImageFile.FileName;
                    }
                    else
                    {
                        // Nếu không có ảnh mới, giữ nguyên tên tệp ảnh hiện tại
                        jewelry.Image = TempData["CurrentImage"] as string;
                    }

                    _context.Update(jewelry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JewelryExists(jewelry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminIndex));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", jewelry.CategoryId);
            ViewData["JewelryTypeId"] = new SelectList(_context.JewelryTypes, "Id", "TypeName", jewelry.JewelryTypeId);
            return View(jewelry);
        }

        // GET: Jewelries/AdminDetails/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDetails(int? id)
        {
            if (id == null || _context.Jewelries == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelries
                .Include(j => j.Category)
                .Include(j => j.JewelryType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (jewelry == null)
            {
                return NotFound();
            }

            return View(jewelry);
        }


        // GET: Jewelries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jewelries == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelries
                .Include(j => j.JewelryType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jewelry == null)
            {
                return NotFound();
            }

            return View(jewelry);
        }

        // POST: Jewelries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jewelries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jewelries'  is null.");
            }
            var jewelry = await _context.Jewelries.FindAsync(id);
            if (jewelry != null)
            {
                _context.Jewelries.Remove(jewelry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminIndex));
        }

        private bool JewelryExists(int id)
        {
          return (_context.Jewelries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
