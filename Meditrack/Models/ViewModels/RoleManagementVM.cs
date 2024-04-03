using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Models.ViewModels
{
    public class RoleManagementVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList{ get; set; }
        public IEnumerable<SelectListItem> LocationList { get; set; }

    }
}
