using Xunit;
using _13_ORM_Geenid.Models;

namespace _13_ORM_Geenid.Tests
{
    public class GeeniTests
    {
        [Fact]
        public void Geen_KahePlusAlleeligaOnPositiivne()
        {
            var alleel1 = new Alleeli("reesus", true);
            var alleel2 = new Alleeli("reesus", true);
            var geen = new Geeni(alleel1, alleel2);

            Assert.True(geen.OnPositiivne);
        }

        [Fact]
        public void Geen_YheMinusYhePlusOnPositiivne()
        {
            var alleel1 = new Alleeli("reesus", false);
            var alleel2 = new Alleeli("reesus", true);
            var geen = new Geeni(alleel1, alleel2);

            Assert.True(geen.OnPositiivne);
        }

        [Fact]
        public void Geen_KaheMinusAlleeligaEiOlePositiivne()
        {
            var alleel1 = new Alleeli("reesus", false);
            var alleel2 = new Alleeli("reesus", false);
            var geen = new Geeni(alleel1, alleel2);

            Assert.False(geen.OnPositiivne);
        }

        [Fact]
        public void VotaJuhuslikAlleel_TagastabYheKahestAlleelist()
        {
            var alleel1 = new Alleeli("reesus", true);
            var alleel2 = new Alleeli("reesus", false);
            var geen = new Geeni(alleel1, alleel2);

            var juhuslik = geen.VotaJuhuslikAlleel();

            Assert.True(juhuslik == alleel1 || juhuslik == alleel2);
        }

        [Fact]
        public void Yhendamine_AnnabSamaLiikiGeeni()
        {
            var vanem1 = new Geeni(
                new Alleeli("reesus", true),
                new Alleeli("reesus", false)
            );
            var vanem2 = new Geeni(
                new Alleeli("reesus", true),
                new Alleeli("reesus", true)
            );

            var laps = Geeni.Yhenda(vanem1, vanem2);

            Assert.Equal("reesus", laps.Alleel1.Nimetus);
            Assert.Equal("reesus", laps.Alleel2.Nimetus);
        }

        [Fact]
        public void EriNimetusegaAlleelidViskavadErindi()
        {
            var alleel1 = new Alleeli("reesus", true);
            var alleel2 = new Alleeli("kell", false);

            Assert.Throws<ArgumentException>(() => new Geeni(alleel1, alleel2));
        }
    }
}