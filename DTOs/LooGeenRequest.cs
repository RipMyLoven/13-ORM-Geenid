using System.ComponentModel.DataAnnotations;

namespace _13_ORM_Geenid.DTOs
{
    /// <summary>
    /// Request geeni loomiseks vanemate alleelide väärtuste põhjal
    /// </summary>
    public class LooGeenRequest
    {
        [Required(ErrorMessage = "Alleeli nimetus on kohustuslik")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nimetus peab olema 1-100 tähemärki pikk")]
        public string AlleeliNimetus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Esimese vanema väärtus on kohustuslik")]
        public bool Vanem1Positiivne { get; set; }

        [Required(ErrorMessage = "Teise vanema väärtus on kohustuslik")]
        public bool Vanem2Positiivne { get; set; }
    }
}
