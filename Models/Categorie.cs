using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nom de la catégorie")]
        [Required(ErrorMessage = "Veuillez saisir un nom")]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("Ordre d'affichage")]
        [Range(1, 10, ErrorMessage = "La valeur doit etre comprise entre 1 et 1")]
        public string OrdreAffichage { get; set; } 
    }
}

