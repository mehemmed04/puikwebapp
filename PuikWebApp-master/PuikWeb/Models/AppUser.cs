using Microsoft.AspNetCore.Identity;

namespace WebUI.Models
{
	public class AppUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual List<Product> Wishlist { get; set; }
	}	
}
