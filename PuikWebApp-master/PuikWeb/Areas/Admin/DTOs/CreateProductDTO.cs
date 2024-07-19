namespace WebUI.Areas.Admin.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
