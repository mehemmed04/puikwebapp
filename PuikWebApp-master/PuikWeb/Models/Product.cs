using Microsoft.EntityFrameworkCore;

namespace WebUI.Models
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string ImgUrl { get; set; }
		public double Price { get; set; }
		public DateTime CreatedAt { get; set; }
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
	}
}
