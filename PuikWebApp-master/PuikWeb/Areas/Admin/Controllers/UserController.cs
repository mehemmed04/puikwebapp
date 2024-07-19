using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Areas.Admin.ViewModels;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
	[Area(nameof(Admin))]
	[Authorize]
	public class UserController : Controller
	{
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
		{
			var users = _userManager.Users.ToList();
			return View(users);
		}
		public async Task<IActionResult> AddRole(string userId)
		{
			if (userId == null) return NotFound();

			var checkUser = await _userManager.FindByIdAsync(userId);

			if (checkUser == null) return NotFound();

			var userRoles = (await _userManager.GetRolesAsync(checkUser)).ToList();
			var roles = _roleManager.Roles.Select(x => x.Name).ToList();

			UserRoleVM userRoleVM = new()
			{
				AppUser = checkUser,
				Roles = roles.Except(userRoles),
			};
			return View(userRoleVM);
		}

		[HttpPost]

		public async Task<IActionResult> AddRole(string userId, string role)
		{
			if (userId == null) return NotFound();

			var checkUser = await _userManager.FindByIdAsync(userId);

			if (checkUser == null) return NotFound();

			var userAddRole = await _userManager.AddToRoleAsync(checkUser, role);

			if (!userAddRole.Succeeded)
			{
				ViewData["Error"] = "Something went wrong!";
				return View();
			}
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> EditRole(string userId)
		{
			if (userId == null) return NotFound();

			var checkUser = await _userManager.FindByIdAsync(userId);

			if (checkUser == null) return NotFound();
			return View(checkUser);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string userId, string role)
		{
			if (userId == null || role == null) return NotFound();
			var checkUser = await _userManager.FindByIdAsync(userId);

			if (checkUser == null) return NotFound();

			IdentityResult result = await _userManager.RemoveFromRoleAsync(checkUser, role);

			if (!result.Succeeded)
			{
				ViewData["Error"] = "Something went wrong!";
				return View();
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> AddToWishList (int productId)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return Unauthorized();

			var product = await _context.Products.FindAsync(productId);
			if (product == null) return NotFound();	

			if(!user.Wishlist.Contains(product))
			{
				user.Wishlist.Add(product);
				await _userManager.UpdateAsync(user);
			}
			return RedirectToAction("Index");
        }


		public async Task<IActionResult> Wishlist()
		{

			var user = await _userManager.GetUserAsync(User);
			if (user == null) return Unauthorized();

			var wishlist = user.Wishlist.ToList();

			return View(wishlist);
		}




		[HttpPost]
		public async Task<IActionResult> addWishList(int id)
		{
			Product? product = await _context.Products.FindAsync(id);

			if (product == null) return NotFound();
			Category? category = await _context.Categories.FindAsync(product.CategoryId);
			if(category == null) return NotFound();
            product.Category = category;
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            if (user.Wishlist == null)
            {
                user.Wishlist = new List<Product>();
            }

            if (!user.Wishlist.Any(p => p.Id == product.Id))
            {
                user.Wishlist.Add(product);
            }
            await _userManager.UpdateAsync(user);
			return RedirectToAction("Index");

        }

		[HttpPost]
		public async Task<IActionResult> DeleteFromWishlist(int ProductId)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return Unauthorized();

			var product = user.Wishlist.FirstOrDefault( p => p.Id == ProductId );
			if (product != null)
			{
				user.Wishlist.Remove(product);
				await _userManager.UpdateAsync(user);
			}
			return RedirectToAction("Index");


		}
	}
}
