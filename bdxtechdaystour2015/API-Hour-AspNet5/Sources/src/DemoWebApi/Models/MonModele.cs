using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoWebApi.Models
{
    public class Houblon
    {
        public string Nom { get; set; }
        public TypeHoublon TypeHoublon { get; set; }
        public string AcideAlpha { get; set; }
    }
    public class Malt
    {
        public string Nom { get; set; }
        public TypeMalt TypeMalt { get; set; }
        public string EBC { get; set; }
    }
    public enum TypeMalt
    {
        DeBase,
        Caramel,
        Torrefie,
        Special,
        Flocon
    }
    public enum TypeHoublon
    {
        Amerisant,
        Aromatique,
        Double
    }
    public interface IRepository
    {
        IEnumerable<Houblon> GetAllHoublons();
        IEnumerable<Houblon> GetAllHoublonsSlow();
        IEnumerable<Malt> GetAllMalts();
        Malt FindMaltByName(string nom);
        Houblon FindHoublonByName(string nom);
    }
    public class Repository : IRepository
    {
        private static readonly ConcurrentBag<Houblon> Houblons = new ConcurrentBag<Houblon>();
        private static readonly ConcurrentBag<Malt> Malts = new ConcurrentBag<Malt>();

        static Repository()
        {
            Houblons.Add(new Houblon { Nom = "Admiral", TypeHoublon = TypeHoublon.Amerisant, AcideAlpha = "11,5 - 16 %" });
            Houblons.Add(new Houblon { Nom = "Ahtanum", TypeHoublon = TypeHoublon.Aromatique, AcideAlpha = "3,5 - 6,3 %" });
            Houblons.Add(new Houblon { Nom = "Amarillo", TypeHoublon = TypeHoublon.Aromatique, AcideAlpha = "8 - 11 %" });
            Houblons.Add(new Houblon { Nom = "Apollo", TypeHoublon = TypeHoublon.Amerisant, AcideAlpha = "15 - 19 %" });
            Houblons.Add(new Houblon { Nom = "Aramis", TypeHoublon = TypeHoublon.Aromatique, AcideAlpha = "7 - 8,50 %" });
            Houblons.Add(new Houblon { Nom = "Atlas", TypeHoublon = TypeHoublon.Amerisant, AcideAlpha = "5 - 9 %" });
            Houblons.Add(new Houblon { Nom = "Aurora", TypeHoublon = TypeHoublon.Aromatique, AcideAlpha = "5 - 10 %" });
            Houblons.Add(new Houblon { Nom = "Beata", TypeHoublon = TypeHoublon.Aromatique, AcideAlpha = "6 - 7 %" });
            Houblons.Add(new Houblon { Nom = "Boadicea", TypeHoublon = TypeHoublon.Double, AcideAlpha = "9 - 11 %" });
            Houblons.Add(new Houblon { Nom = "Bobek", TypeHoublon = TypeHoublon.Aromatique, AcideAlpha = "3 - 8 %" });
            Houblons.Add(new Houblon { Nom = "Brambling Cross", TypeHoublon = TypeHoublon.Aromatique, AcideAlpha = "5 - 7 %" });
            Houblons.Add(new Houblon { Nom = "Bravo", TypeHoublon = TypeHoublon.Amerisant, AcideAlpha = "14 - 17 %" });
            Houblons.Add(new Houblon { Nom = "Brewer", TypeHoublon = TypeHoublon.Amerisant, AcideAlpha = "5,5 - 10%" });
            Houblons.Add(new Houblon { Nom = "Bullion", TypeHoublon = TypeHoublon.Amerisant, AcideAlpha = "6,5 - 9%" });
            Houblons.Add(new Houblon { Nom = "Cascade", TypeHoublon = TypeHoublon.Double, AcideAlpha = "4,5 - 7 %" });
            Houblons.Add(new Houblon { Nom = "Centennial", TypeHoublon = TypeHoublon.Double, AcideAlpha = "8,0 - 11,5 %" });
            Houblons.Add(new Houblon { Nom = "Challenger", TypeHoublon = TypeHoublon.Double, AcideAlpha = "6,5 - 8,5 %" });
            Houblons.Add(new Houblon { Nom = "Chinook", TypeHoublon = TypeHoublon.Double, AcideAlpha = "10 - 14 %" });
            Houblons.Add(new Houblon { Nom = "Citra", TypeHoublon = TypeHoublon.Aromatique, AcideAlpha = "11 - 13 %" });
            Houblons.Add(new Houblon { Nom = "Cluster", TypeHoublon = TypeHoublon.Double, AcideAlpha = "5,5 - 8,5 %" });
            Houblons.Add(new Houblon { Nom = "Columbus", TypeHoublon = TypeHoublon.Amerisant, AcideAlpha = "11 - 18 %" });
            Houblons.Add(new Houblon { Nom = "Crystal", TypeHoublon = TypeHoublon.Aromatique, AcideAlpha = "2,0 - 5,5 %" });

            Malts.Add(new Malt { Nom = "Extra pale premium pilsner 2-3 EBC", TypeMalt = TypeMalt.DeBase, EBC = "2 à 3" });
            Malts.Add(new Malt { Nom = "Bohemian pilsner 3-4 EBC", TypeMalt = TypeMalt.DeBase, EBC = "3 à 4" });
            Malts.Add(new Malt { Nom = "Pils 3 EBC", TypeMalt = TypeMalt.DeBase, EBC = "3" });
            Malts.Add(new Malt { Nom = "Blé ou froment 3 EBC", TypeMalt = TypeMalt.DeBase, EBC = "3" });
            Malts.Add(new Malt { Nom = "Pale 7 EBC", TypeMalt = TypeMalt.DeBase, EBC = "7" });
            Malts.Add(new Malt { Nom = "Cara-pils", TypeMalt = TypeMalt.Caramel, EBC = "5" });
            Malts.Add(new Malt { Nom = "Cara-Hell", TypeMalt = TypeMalt.Caramel, EBC = "25" });
            Malts.Add(new Malt { Nom = "Malt Cara Amber", TypeMalt = TypeMalt.Caramel, EBC = "60" });
            Malts.Add(new Malt { Nom = "Malt crystal 20L", TypeMalt = TypeMalt.Caramel, EBC = "40" });
            Malts.Add(new Malt { Nom = "Malt crystal 40L", TypeMalt = TypeMalt.Caramel, EBC = "80" });
            Malts.Add(new Malt { Nom = "Special type1 800 à 1000 EBC", TypeMalt = TypeMalt.Torrefie, EBC = "800 à 1000" });
            Malts.Add(new Malt { Nom = "Biscuit", TypeMalt = TypeMalt.Torrefie, EBC = "50" });
            Malts.Add(new Malt { Nom = "Chocolat 900 EBC", TypeMalt = TypeMalt.Torrefie, EBC = "900" });
            Malts.Add(new Malt { Nom = "Sauermalz", TypeMalt = TypeMalt.Special, EBC = "6" });
            Malts.Add(new Malt { Nom = "Whisky", TypeMalt = TypeMalt.Special, EBC = "" });
            Malts.Add(new Malt { Nom = "Melanoidin", TypeMalt = TypeMalt.Special, EBC = "70" });
            Malts.Add(new Malt { Nom = "Flocons de maïs", TypeMalt = TypeMalt.Flocon, EBC = "" });
            Malts.Add(new Malt { Nom = "Flocons d'orge", TypeMalt = TypeMalt.Flocon, EBC = "" });
        }

        public IEnumerable<Houblon> GetAllHoublons()
        {
            return Houblons;
        }

        public IEnumerable<Houblon> GetAllHoublonsSlow()
        {
            foreach (Houblon houblon in Houblons)
            {
                Thread.Sleep(400);
                yield return houblon;
            }
        }

        public IEnumerable<Malt> GetAllMalts()
        {
            return Malts;
        }

        public Malt FindMaltByName(string nom)
        {
            return Malts.FirstOrDefault(m => string.Compare(m.Nom, nom, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public Houblon FindHoublonByName(string nom)
        {
            return Houblons.FirstOrDefault(m => string.Compare(m.Nom, nom, StringComparison.CurrentCultureIgnoreCase) == 0);
        }
    }
}
