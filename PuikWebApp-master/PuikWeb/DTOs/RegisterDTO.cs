using System.ComponentModel.DataAnnotations;

namespace WebUI.DTOs
{
	public class RegisterDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public string Password { get; set; }
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}
