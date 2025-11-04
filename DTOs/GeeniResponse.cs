namespace _13_ORM_Geenid.DTOs
{
    /// <summary>
    /// Response geeni kohta
    /// </summary>
    public class GeeniResponse
    {
        public int Id { get; set; }
        public AlleeliResponse Alleel1 { get; set; } = null!;
        public AlleeliResponse Alleel2 { get; set; } = null!;
        public bool OnPositiivne { get; set; }
    }
}
