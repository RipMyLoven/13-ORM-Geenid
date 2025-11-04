namespace _13_ORM_Geenid.Models
{
    public class Geeni
    {
        public int Id { get; set; }
        public int Alleel1Id { get; set; }
        public int Alleel2Id { get; set; }

        public Alleeli Alleel1 { get; set; } = null!;
        public Alleeli Alleel2 { get; set; } = null!;

        public bool OnPositiivne => Alleel1?.Positiivne == true || Alleel2?.Positiivne == true;

        public Geeni(Alleeli alleel1, Alleeli alleel2)
        {
            if (alleel1.Nimetus != alleel2.Nimetus)
                throw new ArgumentException("Alleelid peavad olema sama nimetusega");

            Alleel1 = alleel1;
            Alleel2 = alleel2;
        }

        public Geeni() { }

        public Alleeli VotaJuhuslikAlleel()
        {
            return Random.Shared.Next(2) == 0 ? Alleel1 : Alleel2;
        }

        public static Geeni Yhenda(Geeni vanem1, Geeni vanem2)
        {
            var alleel1 = vanem1.VotaJuhuslikAlleel();
            var alleel2 = vanem2.VotaJuhuslikAlleel();

            return new Geeni(
                new Alleeli(alleel1.Nimetus, alleel1.Positiivne),
                new Alleeli(alleel2.Nimetus, alleel2.Positiivne)
            );
        }
    }
}
