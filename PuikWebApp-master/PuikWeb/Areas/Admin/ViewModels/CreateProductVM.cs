using WebUI.Areas.Admin.DTOs;
using WebUI.Models;

namespace WebUI.Areas.Admin.ViewModels
{
    public class CreateProductVM
    {
        public CreateProductDTO Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
