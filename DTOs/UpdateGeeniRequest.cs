using System.ComponentModel.DataAnnotations;

namespace _13_ORM_Geenid.DTOs
{
    /// <summary>
    /// Request geeni uuendamiseks
    /// </summary>
    public class UpdateGeeniRequest
    {
        [Required(ErrorMessage = "Alleeli nimetus on kohustuslik")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nimetus peab olema 1-100 tähemärki pikk")]
        public string AlleeliNimetus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Esimese alleeli väärtus on kohustuslik")]
        public bool Alleel1Positiivne { get; set; }

        [Required(ErrorMessage = "Teise alleeli väärtus on kohustuslik")]
        public bool Alleel2Positiivne { get; set; }
    }
}
