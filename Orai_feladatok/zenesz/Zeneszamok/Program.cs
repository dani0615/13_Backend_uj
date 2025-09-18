using MySql.Data.MySqlClient;
using Zeneszamok.Controllers;
using Zeneszamok.Models;

namespace Zeneszamok
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Menü kiiras és választása (CRUD MŰVELETEK)
            bool folytat = true;
            do
            {
                string valasztottTabla = FoMenu();

                switch (valasztottTabla)
                {
                    case "1": // Lemez tábla kezelése
                        string lemezValasztas;
                        lemezValasztas = LemezMenu();
                        switch (lemezValasztas)
                        {
                            case "1": // Lemezek listázása
                                List<Lemez> lemezLista = new LemezController().LemezekListaja();
                                foreach (var lemez in lemezLista)
                                {
                                    Console.WriteLine(lemez);
                                }

                                Console.WriteLine("");
                                break;

                            case "2":
                                Lemez rogzitendoLemez = LemezBekerese();
                                string uzenetUJLemez = new LemezController().LemezFelvitele(rogzitendoLemez);
                                Console.WriteLine(uzenetUJLemez);

                                Console.WriteLine("Lemez felvétel");
                                break;
                            case "3": // Lemez módosítása
                                Lemez modositando = LemezBekerese();
                                string uzenetModositLemez = new LemezController().LemezModositas(modositando);
                                Console.WriteLine(uzenetModositLemez);


                                Console.WriteLine("");
                                break;
                            case "4": // Lemez törlése
                                Console.WriteLine("Kérem adja meg a törlendő lemez Id-ját:");
                                int torlendoLemez = int.Parse(Console.ReadLine());
                                string uzenetTorolLemez = new LemezController().LemezTorlese(torlendoLemez);
                                Console.WriteLine(uzenetTorolLemez);
                                Console.WriteLine("");
                                break;
                            case "5": // Vissza a főmenübe
                                break;
                            default:
                                Console.WriteLine("Nincs ilyen menüpont");
                                break;
                        }
                        Console.WriteLine("Enterre tovább");
                        Console.ReadLine();

                        break;

                    case "2": // Előadó tábla kezelése
                        string eloadoValasztas;
                        eloadoValasztas = EloadoMenu();
                        switch (eloadoValasztas)
                        {
                            case "1": // Előadók listázása
                                List<Eloado> eloadoLista = new EloadoController().EloadokListaja();
                                foreach (var eloado in eloadoLista)
                                {
                                    Console.WriteLine(eloado);
                                }
                                break;
                            case "2": // Előadó felvétele
                                Eloado rogzitendo = EloadoBekerese();
                                string uzenetUJ = new EloadoController().EloadoFelvitele(rogzitendo);
                                Console.WriteLine(uzenetUJ);
                                break;
                            case "3": // Előadó módosítása
                                Eloado modositando = EloadoBekerese();
                                string uzenetModosit = new EloadoController().EloadoModositas(modositando);
                                Console.WriteLine(uzenetModosit);
                                break;
                            case "4": // Előadó törlése
                                Console.WriteLine("Kérem adja meg a törlendő előadó Id-ját:");
                                int torlendo = int.Parse(Console.ReadLine());
                                string uzenetTorol = new EloadoController().EloadoTorlese(torlendo);
                                Console.WriteLine(uzenetTorol);
                                break;
                            case "5": // Vissza a főmenübe
                                break;
                            default:
                                Console.WriteLine("Nincs ilyen menüpont");
                                break;
                        }
                        Console.WriteLine("Enterre tovább");
                        Console.ReadLine();
                        break;

                    case "3": // Kilépés
                        folytat = false;
                        break;

                    default:
                        Console.WriteLine("Nincs ilyen menüpont");
                        Console.WriteLine("Enterre tovább");
                        Console.ReadLine();
                        break;
                }
            } while (folytat);
        }



        static string FoMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Lemez tábla kezelése");
            Console.WriteLine("2 - Előadó tábla kezelése");
            Console.WriteLine("3 - Kilépés a programból \n");
            return Console.ReadLine();
        }

        static string LemezMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Lemezek Listája");
            Console.WriteLine("2 - Lemez felvétele");
            Console.WriteLine("3 - Lemez módosítása");
            Console.WriteLine("4 - Lemez törlése");
            Console.WriteLine("5 - Vissza a főmenübe \n");
            return Console.ReadLine();
        }

        static string EloadoMenu()
        {
            string valasztott;
            Console.Clear();
            Console.WriteLine("1 - Előadók Listája");
            Console.WriteLine("2 - Előadó felvétele");
            Console.WriteLine("3 - Előadó módosítása");
            Console.WriteLine("4 - Előadó törlése");
            Console.WriteLine("5 - Kilépés \n");

            valasztott = Console.ReadLine();
            return valasztott;
        }

        static Eloado EloadoBekerese()
        {
            Eloado ujEloado = new Eloado();
            Console.WriteLine("Kérem az előadó azonosítóját");
            ujEloado.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Kérem az előadó nevét: ");
            ujEloado.Nev = Console.ReadLine();
            Console.WriteLine("Kérem az előadó nemzetiségét: ");
            ujEloado.Nemzetiseg = Console.ReadLine();
            Console.WriteLine("Kérem adja meg, hogy szóló előadó-e (Igen/Nem): ");
            ujEloado.Szolo = (Console.ReadLine().ToLower() == "igen") ? true : false;
            return ujEloado;
        }

        static Lemez LemezBekerese()
        {
            Lemez ujLemez = new Lemez();
            Console.WriteLine("Kérem a lemez azonosítóját");
            ujLemez.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Kérem a lemez címét: ");
            ujLemez.Cim = Console.ReadLine();
            Console.WriteLine("Kérem a lemez kiadásának évét: ");
            ujLemez.KiadasEve = int.Parse(Console.ReadLine());
            Console.WriteLine("Kérem a lemez kiadóját: ");
            ujLemez.Kiado = Console.ReadLine();
            return ujLemez;
        }




    }
}

