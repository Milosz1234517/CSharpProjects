using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEAProjekt2
{
    class Menu
    {
        private double ChooseA(int option)
        {
            switch (option)
            {
                case 1:
                    return 0.9999;
                case 2:
                    return 0.999999;
                case 3:
                    return 0.99999;
                default:
                    return 0;
            }
        }

        public void MainMenu()
        {
            TabuSearch tabu;
            SimulatedAnnealing simulatedAnnealing;
            Data m = new Data();
            double workTime = 0;
            double a = 0;
            bool stop = false;
            while (stop == false)
            {
                Console.WriteLine();
                Console.WriteLine("1. Wczytaj dane\n2. Podaj czas pracy\n3. Wybierz wspolczynnik a\n4. Tabu Search\n5. Symulowane wyzarzanie\n6. Koniec");
                Console.WriteLine();
                Console.WriteLine("Plik: " + m.getFileName() + "\n" + "a = " + a + "\n" + "Wybrany czas: " + workTime/1000 + "[s]\n");
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
                        Console.WriteLine("Wybierz a: 1 - 0.9999, 2 - 0.999999, 3 - 0.99999");
                        int op = int.Parse(Console.ReadLine());
                        a = ChooseA(op);
                        Console.WriteLine("Wczytano a");
                        Console.WriteLine();
                        break;
                    case 4:
                        if (m.getTspMatrix() != null && workTime != 0)
                        {
                            tabu = new TabuSearch(m.getCityNumber());
                            tabu.Search(m.getTspMatrix(), m.getCityNumber(), workTime, false);
                            Console.WriteLine();
                            tabu.getBestRoute(m.getCityNumber());
                            Console.WriteLine();
                            Console.WriteLine();
                            tabu.getBestCost();
                            Console.WriteLine();
                            tabu.GetTime();
                            Console.WriteLine();
                        }
                        break;
                    case 5:
                        if (m.getTspMatrix() != null && workTime != 0 && a != 0)
                        {
                            simulatedAnnealing = new SimulatedAnnealing(m.getCityNumber());
                            simulatedAnnealing.Search(m.getTspMatrix(), m.getCityNumber(), workTime, a, false);
                            Console.WriteLine();
                            simulatedAnnealing.getBestRoute(m.getCityNumber());
                            Console.WriteLine();
                            Console.WriteLine();
                            simulatedAnnealing.getBestCost();
                            Console.WriteLine();
                            simulatedAnnealing.GetTime();
                            Console.WriteLine();
                            simulatedAnnealing.GetTemperature();
                            Console.WriteLine();
                        }
                        break;
                    case 6:
                        stop = true;
                        break;
                    case 7:
                        for (int i = 0; i < 10; i++)
                        {
                            tabu = new TabuSearch(m.getCityNumber());
                            tabu.Search(m.getTspMatrix(), m.getCityNumber(), workTime, true);
                            Console.WriteLine();
                            tabu.getBestRoute(m.getCityNumber());
                            Console.WriteLine();
                            Console.WriteLine();
                            tabu.getBestCost();
                            Console.WriteLine();
                            tabu.GetTime();
                            Console.WriteLine();
                        }
                        
                        break;
                    case 8:
                        for (int i = 0; i < 10; i++)
                        {
                            simulatedAnnealing = new SimulatedAnnealing(m.getCityNumber());
                            simulatedAnnealing.Search(m.getTspMatrix(), m.getCityNumber(), workTime, a, true);
                            Console.WriteLine();
                            simulatedAnnealing.getBestRoute(m.getCityNumber());
                            Console.WriteLine();
                            Console.WriteLine();
                            simulatedAnnealing.getBestCost();
                            Console.WriteLine();
                            simulatedAnnealing.GetTime();
                            Console.WriteLine();
                            simulatedAnnealing.GetTemperature();
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
