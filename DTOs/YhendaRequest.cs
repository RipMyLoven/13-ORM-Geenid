using System.ComponentModel.DataAnnotations;

namespace _13_ORM_Geenid.DTOs
{
    /// <summary>
    /// Request kahe geeni ühendamiseks
    /// </summary>
    public class YhendaRequest
    {
        [Required(ErrorMessage = "Esimene vanem on kohustuslik")]
        [Range(1, int.MaxValue, ErrorMessage = "Vanem1Id peab olema positiivne number")]
        public int Vanem1Id { get; set; }

        [Required(ErrorMessage = "Teine vanem on kohustuslik")]
        [Range(1, int.MaxValue, ErrorMessage = "Vanem2Id peab olema positiivne number")]
        public int Vanem2Id { get; set; }
    }
}
