using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Models;

namespace WebUI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			});

			builder.Services.AddDefaultIdentity<AppUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
			
			builder.Services.ConfigureApplicationCookie(option =>
			{
				option.LoginPath = "/Auth/Login";
			});

			builder.Services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = true;  // Password-da reqem 
				options.Password.RequireLowercase = true; // Password hamisi lowercase 
				options.Password.RequireNonAlphanumeric = false; // Password-da simvol 
				options.Password.RequireUppercase = false; // Password-da uppercase
				options.Password.RequiredLength = 8; // Minimum password lenght
				options.Lockout.MaxFailedAccessAttempts = 5; // Login-de xeta limiti
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // Xeta limiti kecdikce verilen ban
				options.User.RequireUniqueEmail = false; // tesdiqlenmis email
			});
			
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();



			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				  name: "areas",
				  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);
			});


			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}