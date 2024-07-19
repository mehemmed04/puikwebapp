namespace WebUI.Areas.Admin.DTOs
{
    public class UpdateProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public double Price { get; set; }

        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; } 
    }
}
