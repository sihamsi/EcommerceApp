using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Models.ViewModels
{
    public class ProduitVM
    {
        public Produit produit { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> categorieListe { get; set; }
    }
}