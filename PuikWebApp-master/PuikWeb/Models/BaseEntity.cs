namespace WebUI.Models
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public bool IsDeleted { get; set; }
	}
}
