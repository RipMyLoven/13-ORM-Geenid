using System.ComponentModel.DataAnnotations;

namespace _13_ORM_Geenid.Models
{
    public class Alleeli
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nimetus on kohustuslik")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nimetus peab olema 1-100 tähemärki pikk")]
        public string Nimetus { get; set; } = string.Empty;
        
        public bool Positiivne { get; set; }

        public Alleeli(string nimetus, bool positiivne)
        {
            Nimetus = nimetus;
            Positiivne = positiivne;
        }
        
        public Alleeli() { }
    }
}
