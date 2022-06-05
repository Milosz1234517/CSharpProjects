using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEAProjekt3v1._0
{
    class Menu
    {
        private bool ChooseA(int option)
        {
            switch (option)
            {
                case 1:
                    return false;
                case 2:
                    return true;
                default:
                    return false;
            }
        }
        public void MainMenu()
        {
            GeneticAlghoritm g;
            Data m = new Data();
            double workTime = 0;
            int pop = 0;
            double mut = 0;
            double cross = 0;
            bool stop = false;
            bool crossMethod = false;
            string method = "OX";
            while (stop == false)
            {
                Console.WriteLine();
                Console.WriteLine("1. Wczytaj dane\n2. Podaj czas pracy\n3. Wybierz wielkosci populacji\n4. Wybierz wspolczynnik mutacji\n5. Wybierz wspolczynnik krzyzowania\n6. Wybierz metode krzyzowania\n7.Algorytm Genetyczny\n8.Koniec");
                Console.WriteLine();
                if (crossMethod) method = "PMX";
                else method = "OX";
                Console.WriteLine("Plik: " + m.getFileName() + "\n" + "populacja = " + pop + "\n" + "wsp. mutacji = " + mut + "\n" + "wsp. krzyzowania = " + cross + "\n" + "metoda krzyzowania = " + method + "\n" + "Wybrany czas: " + workTime / 1000 + "[s]\n");
                Console.WriteLine();
                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine();
                        Console.WriteLine("Podaj nazwe pliku");
                        string filename = Console.ReadLine();
                        m.ReadMatrix(filename);
                        Console.WriteLine("Wczytano macierz");
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.WriteLine();
                        Console.WriteLine("Podaj czas[s]");
                        workTime = double.Parse(Console.ReadLine()) * 1000;
                        Console.WriteLine("Wczytano czas");
                        Console.WriteLine();
                        break;
                    case 3:
                        Console.WriteLine();
                        Console.WriteLine("Podaj wielkosc populacji");
                        pop = int.Parse(Console.ReadLine());
                        Console.WriteLine("Wczytano populacje");
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.WriteLine("Podaj wspolczynnik mutacji");
                        mut = double.Parse(Console.ReadLine());
                        Console.WriteLine("Wczytano mutacje");
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.WriteLine();
                        Console.WriteLine("Podaj wspolczynnik krzyzowania");
                        cross = double.Parse(Console.ReadLine());
                        Console.WriteLine("Wczytano krzyzowanie");
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.WriteLine();
                        Console.WriteLine("Wybierz a: 1 - OX, 2 - PMX");
                        int op = int.Parse(Console.ReadLine());
                        crossMethod = ChooseA(op);
                        Console.WriteLine("Wybrano metode krzyzowania");
                        Console.WriteLine();
                        break;
                    case 7:
                        if (m.getTspMatrix() != null && workTime != 0 && mut != 0 && cross != 0)
                        {
                            g = new GeneticAlghoritm(m.getTspMatrix(), m.getCityNumber());
                            g.runAlghoritm(workTime, pop, crossMethod, cross, mut, false);
                            Console.WriteLine();

                            for (int j = 0; j < m.getCityNumber(); j++)
                            {
                                Console.Write(g.BestRoute[j] + " ");
                            }
                            Console.WriteLine();

                            Console.WriteLine();
                            Console.WriteLine(g.BestCost);
                            Console.WriteLine();

                        }
                        break;
                    case 8:
                        stop = true;
                        break;
                    case 9:
                        for (int i = 0; i < 10; i++)
                        {
                            g = new GeneticAlghoritm(m.getTspMatrix(), m.getCityNumber());
                            g.runAlghoritm(workTime, pop, crossMethod, cross, mut, true);
                            Console.WriteLine();

                            for (int j = 0; j < m.getCityNumber(); j++)
                            {
                                Console.Write(g.BestRoute[j] + " ");
                            }
                            Console.WriteLine();

                            Console.WriteLine();
                            Console.WriteLine(g.BestCost);
                            Console.WriteLine();
                        }
                        break;
                    default:
                        Console.WriteLine("Nie ma takiej opcji");
                        break;
                }
            }
        }
    }
}
