using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Meditrack.Models.ViewModels
{
    public class RegisterVM
    {
        public Register Register { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> LocationList { get; set; }
    }
}

