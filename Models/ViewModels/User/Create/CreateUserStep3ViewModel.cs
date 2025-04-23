using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestorViajes.Models.ViewModels.User.Create
{
    public class CreateUserStep3ViewModel
    {
        public string SelectedRole { get; set; } = string.Empty;
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
    }
}
