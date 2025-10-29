namespace _13_ORM_Geenid.Models
{
    public class Alleeli
    {
        public int Id { get; set; }
        public string Nimetus { get; set; }
        public bool Positiivne { get; set; }

        public Alleeli(string nimetus, bool positiivne)
        {
            Nimetus = nimetus;
            Positiivne = positiivne;
        }

    }
}
