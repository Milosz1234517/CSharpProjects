using System;

namespace ProjektPEA
{
    class Program
    {
        static void Menu()
        {
            Console.WriteLine(" 1. Wczytaj macierz\n 2. Wygeneruj macierz\n 3. Wyswietl macierz\n 4. Brute Force\n 5. Held-Karp\n 6. B and B\n 61. B and B DFS\n 7. Zakoncz\n 8. Held-Karp TEST\n " +
                "9. B and B TEST\n 10. Brute Force TEST\n 11. B and B DFS TEST\n");
            bool run = true;
            Data d = new Data();
            Alghoritm a = new Alghoritm();
            Test t = new Test();
            while (run)
            {
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Podaj nazwe pliku:\n");
                            string name = Console.ReadLine();
                            d.ReadMatrix(name);
                            break;
                        case 2:
                            Console.WriteLine("Podaj ilosc miast:\n");
                            int cities = Convert.ToInt32(Console.ReadLine());
                            if (cities > 1)
                                d.GenerateRandomMatrix(cities);
                            else
                                Console.WriteLine("Za malo miast");
                            break;
                        case 3:
                                d.PrintMatrix();
                            break;
                        case 4:
                            if (d.tspMatrix != null)
                            {
                                a.BruteForce(d.cityNumber, d.tspMatrix);
                                a.BruteForcePrintResult();
                            }
                            break;
                        case 5:
                            if (d.tspMatrix != null)
                            {
                                a.HeldKarp(d.cityNumber, d.tspMatrix);
                                a.HeldKarpPrintResult();
                            }
                            break;
                        case 6:
                            if (d.tspMatrix != null)
                            {
                                a.BandB(d.cityNumber, d.tspMatrix);
                                a.BandBPrintResult();
                            }
                            break;
                        case 7:
                            run = false;
                            break;
                        case 8:
                            Console.WriteLine("Podaj ilosc miast:\n");
                            int city = Convert.ToInt32(Console.ReadLine());
                            if (city > 1)
                                t.HeldKarpTest(city);
                            else
                                Console.WriteLine("Za malo miast");
                            break;
                        case 9:
                            Console.WriteLine("Podaj ilosc miast:\n");
                            int city1 = Convert.ToInt32(Console.ReadLine());
                            if (city1 > 1)
                                t.BandBTest(city1);
                            else
                                Console.WriteLine("Za malo miast");
                            break;
                        case 10:
                            Console.WriteLine("Podaj ilosc miast:\n");
                            int city2 = Convert.ToInt32(Console.ReadLine());
                            if (city2 > 1)
                                t.BruteForceTest(city2);
                            else
                                Console.WriteLine("Za malo miast");
                            break;
                        case 11:
                            Console.WriteLine("Podaj ilosc miast:\n");
                            int city3 = Convert.ToInt32(Console.ReadLine());
                            if (city3 > 1)
                                t.BandBDepthTest(city3);
                            else
                                Console.WriteLine("Za malo miast");
                            break;
                        case 61:
                            if (d.tspMatrix != null)
                            {
                                a.BandBDepth(d.cityNumber, d.tspMatrix);
                                a.BandBDepthPrintResult();
                            }
                            break;

                    }
                }
                catch (Exception)

                {
                    Console.WriteLine("Blad");
                }

            }
        }
        static void Main(string[] args)
        {
            Program.Menu();
            
        }
    }
}
