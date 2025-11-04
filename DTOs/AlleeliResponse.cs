namespace _13_ORM_Geenid.DTOs
{
    /// <summary>
    /// Response alleeli kohta
    /// </summary>
    public class AlleeliResponse
    {
        public int Id { get; set; }
        public string Nimetus { get; set; } = string.Empty;
        public bool Positiivne { get; set; }
    }
}
