using System.Numerics;
using WebUI.Models;

namespace WebUI.Areas.Admin.ViewModels
{
	public class UserRoleVM
	{
		public AppUser AppUser { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}
