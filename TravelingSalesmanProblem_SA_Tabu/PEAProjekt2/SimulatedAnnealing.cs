using System;
using System.Diagnostics;

namespace PEAProjekt2
{
    class SimulatedAnnealing
    {
        private int bestCost;
        private int[] bestRoute;
        private double time;
        private double tempEnd;

        public void GetTime()
        {
            Console.WriteLine("Czas: " + time/1000);
        }

        public void GetTemperature()
        {
            Console.WriteLine("Temperatura: " + tempEnd);
        }

        public SimulatedAnnealing(int cityNumber)
        {
            bestRoute = new int[cityNumber];
        }

        public void getBestRoute(int cityNumber)
        {
            Console.Write("Sciezka: ");
            for (int i = 0; i < cityNumber; i++)
            {
                Console.Write(bestRoute[i] + " ");
            }
        }

        public void getBestCost()
        {
            Console.WriteLine("Koszt: " + bestCost);
        }

        public void Search(int[][] tspMatrix, int cityNumber, double timeEnd, double a, bool test)
        {
            //obiekt do pomiaru czasu
            Stopwatch st = new Stopwatch();
            st.Start();

            //obiekt do operacji np. swap itd.
            Operations op = new Operations();

            //trasa poczatkowa
            int[] route = op.GenerateStart(tspMatrix, cityNumber);
            int routeCost = op.CalculateRouteCost(tspMatrix, cityNumber, route);

            //trasa poczatkowa jako najlepsza z najlepszych
            route.CopyTo(bestRoute, 0);
            bestCost = routeCost;

            //obliczenie temperatury
            double T = op.CalculateTemperature(tspMatrix, cityNumber);
            double setT = T;

            //najlepsza trasa - ustawiana jest w zaleznosci od prawdopodobienstwa lub kosztu sciezki przez co moze przyjmowac gorsze wartosci,
            //dlatego nie w niej zapisujemy najlepsze koszty a w bestCost"
            int[] bestLocalRoute = new int[cityNumber];
            route.CopyTo(bestLocalRoute, 0);
            int bestLocalCost = routeCost;

            Random random = new Random();
            int maxIJ = cityNumber - 1;

            double benchTime = 10000;

            while (true)
            {
                    //losowa tablica z indexami, inna dla kazdej temperatury
                    int[] indexes = op.GenerateRandom(cityNumber);

                    //petla szukajaca przez okreslona ilosc iteracji jakiegos rozwiazania ktore mozna przyjac
                    for (int i = 0; i < cityNumber; i++)
                    {
                        //wybierz dwa losowe indexy z tablicy, jak wybrano takie same to wybierz sasiedni 
                        int iI = random.Next(cityNumber);
                        int iJ = random.Next(maxIJ);
                        if (iI == iJ) iJ = iI + 1;

                        int sI = indexes[iI];
                        int sJ = indexes[iJ];

                        //zamiana wierzcholkow
                        op.Swap(sI, sJ, ref route);

                        routeCost = op.CalculateRouteCost(tspMatrix, cityNumber, route);

                        //roznica przyjetego rozwiazania oraz rozwiazania po zamianie
                        int difference = bestLocalCost - routeCost;

                        //jezeli roznica > 0 lub prawdopodobienstwo to przyjmij trase jako nastepny obszar do rozpatrzenia
                        if ((difference > 0) || (random.NextDouble() < op.ProbabilityCalculate(T, difference)))
                        {
                            route.CopyTo(bestLocalRoute, 0);
                            bestLocalCost = routeCost;

                            //jesli przyjeta trasa jest lepsza od najlepszej z najlepszych to ustaw ja
                            if (routeCost < bestCost)
                            {
                                route.CopyTo(bestRoute, 0);
                                bestCost = routeCost;
                                time = st.ElapsedMilliseconds;
                                tempEnd = T;
                            }
                            //skoro przyjelismy juz jakies rozwiazanie jako nastepne do rozpatzrenia to konczymy przeszukiwanie dla tego poziomu T
                            break;
                        }
                        else
                        {
                            //jak nie przyjeto to wycofaj ruch i pzrejrzyj nastepnego losowego sasiada
                            op.Swap(sI, sJ, ref route);
                        }
                        //test
                        if (test)
                        {
                            double stop = st.ElapsedMilliseconds;
                            if (stop >= benchTime)
                            {
                                benchTime += 10000;
                                //Console.WriteLine(bestCost + " " + stop / 1000 + "s");
                                Console.WriteLine(bestCost);
                            }
                        }
                        //warunek konca algorytmu
                        if (st.ElapsedMilliseconds >= timeEnd)
                        {
                            st.Stop();
                            return;
                        }
                    }
                    //zapamietana przyjeta trase kopiuj do route i rozpatrz
                    bestLocalRoute.CopyTo(route, 0);
                    //schladzanie
                    T = a * T;
          


            }

        }

    }
}
