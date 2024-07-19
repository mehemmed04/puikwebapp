using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Areas.Admin.DTOs;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Select(c => new CategoryDTO
                {
                    CategoryId = c.Id,
                    CategoryName = c.CategoryName
                }
            ).ToListAsync();
            return View(categories);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    CategoryName = categoryDTO.CategoryName,
                };
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            CreateCategoryDTO viewCategoryDTO = new()
            {
                CategoryName = categoryDTO.CategoryName
            };
            return View(viewCategoryDTO);
        }

        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new UpdateCategoryDTO
            {
                Id = category.Id,
                Name = category.CategoryName,
            };
            return View(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                category.CategoryName = categoryDto.CategoryName;
                _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var viewCategoryDto = new UpdateCategoryDTO
            {
                Id = id,
                Name = categoryDto.CategoryName,
            };
            return View(viewCategoryDto);
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDTO
            {
                CategoryId = category.Id,
                CategoryName = category.CategoryName
            };
            return View(categoryDto);
        }
    }
}