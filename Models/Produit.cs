using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication2.Models
{
    public class Produit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public  string Name { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        [DisplayName("Prix du produit")]
        [Range(1, 100000, ErrorMessage = "La valeur doit etre comprise entre 1 et 1")]
        public int PrixProduit { get; set; }

        public string Description { get; set; }

        [ForeignKey("categorieId")]
        public int categorieId { get; set; }

        [ValidateNever]
        public Categorie categorie { get; set; }
        [ValidateNever]
        public string imageURL { get; set; } 
    }
}
