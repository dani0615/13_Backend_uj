using VersenyNyilvantarto.Models;

namespace VersenyNyilvantarto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool fut = true;

            do
            {
               
                Console.WriteLine("1.Feladatok listázása");
                Console.WriteLine("2.Feladatok módosítása");
                Console.WriteLine("3.Feladatok törlése");
                Console.WriteLine("4.Feladatok felvétele");
                Console.WriteLine("5. Kilépés");
                int valasztott = int.Parse(Console.ReadLine());


                switch (valasztott)
                {
                    case 1:
                        List<Feladat> feladatLista = new FeladatController().FeladatokListaja();
                        foreach (var item in feladatLista)
                        {
                            Console.WriteLine($"{item.Id} {item.Szoveg} {item.Valasz1} {item.Valasz2} {item.Valasz3} {item.Valasz4} {item.HelyesValasz} {item.Pont}");
                        }
                        
                        break;
                        case 2:
                        Console.WriteLine("Kérem adja meg a módosítandó feladat azonosítóját:");
                        int id = int.Parse(Console.ReadLine());
                        Feladat modositando = new Feladat();
                        modositando.Id = id;
                        Console.WriteLine("Kérem adja meg a feladat szövegét:");
                        modositando.Szoveg = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg az 1. válaszlehetőséget:");
                        modositando.Valasz1 = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg a 2. válaszlehetőséget:");
                        modositando.Valasz2 = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg a 3. válaszlehetőséget:");
                        modositando.Valasz3 = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg a 4. válaszlehetőséget:");
                        modositando.Valasz4 = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg a helyes válasz számát (1-4):");
                        modositando.HelyesValasz = int.Parse(Console.ReadLine());
                        Console.WriteLine("Kérem adja meg a feladat pontszámát (0-10):");
                        modositando.Pont = int.Parse(Console.ReadLine());
                        string modositEredmeny = new FeladatController().FeladatModositasa(modositando);
                        Console.WriteLine(modositEredmeny);


                        
                            
                            break;
                        case 3:
                        Console.WriteLine("Kérem adja meg a törlendő feladat azonosítóját:");
                        int torlendoId = int.Parse(Console.ReadLine());
                        string torlesEredmeny = new FeladatController().FeladatTorlese(torlendoId);
                        Console.WriteLine(torlesEredmeny);

                            
                            break;
                        case 4:
                            //FeladatController.Felvetel();
                            Feladat ujFeladat = new Feladat();
                        Console.WriteLine("Kérem adja meg a feladat azonosítóját:");
                        ujFeladat.Id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Kérem adja meg a feladat szövegét:");
                        ujFeladat.Szoveg = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg az 1. válaszlehetőséget:");
                        ujFeladat.Valasz1 = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg a 2. válaszlehetőséget:");
                        ujFeladat.Valasz2 = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg a 3. válaszlehetőséget:");
                        ujFeladat.Valasz3 = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg a 4. válaszlehetőséget:");
                        ujFeladat.Valasz4 = Console.ReadLine();
                        Console.WriteLine("Kérem adja meg a helyes válasz számát (1-4):");
                        ujFeladat.HelyesValasz = int.Parse(Console.ReadLine());
                        Console.WriteLine("Kérem adja meg a feladat pontszámát (0-10):");
                        ujFeladat.Pont = int.Parse(Console.ReadLine());
                        string felvetelEredmeny = new FeladatController().FeladatFelvitele(ujFeladat);
                        Console.WriteLine(felvetelEredmeny);

                            break;
                        case 5:
                            fut = false;
                        break;
                    default:
                        break;
                }

            } while (fut);
        }
    }
}
