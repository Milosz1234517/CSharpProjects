using System;
using System.Diagnostics;

namespace PEAProjekt2
{
    class TabuSearch
    {
        private int bestCost;
        private int[] bestRoute;
        private double time;

        public void GetTime()
        {
            Console.WriteLine("Czas: " + time / 1000);
        }

        public TabuSearch(int cityNumber)
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

        public void Search(int[][] tspMatrix, int cityNumber, double timeEnd, bool test)
        {
            //poiar czasu
            Stopwatch st = new Stopwatch();
            st.Start();

            //obiekt klasy wykonujacej operacje np. swap itd.
            Operations op = new Operations();
            Random random = new Random();

            //sciezka poczatkowa
            int[] route = op.GenerateStart(tspMatrix, cityNumber);
            int routeCost = op.CalculateRouteCost(tspMatrix, cityNumber, route);

            //ustawienie poczatkowej sciezki jako najlepszej
            route.CopyTo(bestRoute, 0);
            bestCost = routeCost;

            int[] bestLocalRoute = new int[cityNumber];

            //kadencja oraz parametr "zycia" wywołujący generowanie nowego rozwiazania
            int cadency = cityNumber * 10;
            int newLife = cadency * 3;
            int lifeEnd = newLife;

            //lista tabu inicjalizacja
            int[][] tabuList = new int[cityNumber][];
            for (int i = 0; i < cityNumber; i++)
            {
                tabuList[i] = new int[cityNumber];
            }

            //wszystkie kadencje na 0
            op.ClearTabu(cityNumber, ref tabuList);

            //tablica losowych miast, uzywana pod koniec kodu do wyboru metody generowania nowego rozwiazania 
            int[] indexes = op.GenerateRandom(cityNumber);
            int idx = 0;

            //zmienna do testow, ustawiona na 10s
            double benchTime = 10000;

            //petla glowna - koniec gdy uplynie czas dzialania
            while (st.ElapsedMilliseconds <= timeEnd)
            {
                int bestLocalCost = int.MaxValue;

                //zmienne na ruch ktory doprowadzi do najlepszego rozwiazania
                int tabuCity1 = 0;
                int tabuCity2 = 0;

                //if do testow, slozy wyswietlaniu co 10s najlepszego rozwiazania
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

                //petle do permutacji sasiadow, generuje dokladnie n(n - 1)/2 sasiadow
                for (int i = 0; i < cityNumber; i++)
                {
                    for (int j = i + 1; j < cityNumber; j++)
                    {
                        //aktualizacja tabu, skoro i tak przegladamy wszystkie mozliwe ruchy zamiany, to od razu aktulizujemy liste tabu
                        if (tabuList[i][j] > 0)
                            tabuList[i][j]--;

                        //operacja zamiany miast
                        op.Swap(i, j, ref route);

                        //obliczenie nowego kosztu
                        routeCost = op.CalculateRouteCost(tspMatrix, cityNumber, route);

                        //aspiracja oraz sprawdzenie ruchu czy jest w tabu
                        if ((routeCost < bestLocalCost && tabuList[i][j] == 0) || routeCost < bestCost)
                        {
                            //zapamietaj miasta dla naj ruchu i ustaw trase jako najlepsza lokalna
                            tabuCity1 = i;
                            tabuCity2 = j;
                            route.CopyTo(bestLocalRoute, 0);
                            bestLocalCost = routeCost;
                        }
                        else
                        {
                            //aktualizacja "zycia"
                            lifeEnd--;
                        }
                        op.Swap(i, j, ref route);
                    }
                }

                //najlepsza lokalna trasa jako nowa trasa dla ktorej szukamy sasiadow
                bestLocalRoute.CopyTo(route, 0);

                //jesli najlepsza lokalna trasa jest lepsza od najlepszej to nowy najlepszy
                if (bestLocalCost < bestCost)
                {
                    bestLocalRoute.CopyTo(bestRoute, 0);
                    bestCost = bestLocalCost;
                    time = st.ElapsedMilliseconds;
                }

                //op.UpdateTabu(cityNumber, ref tabuList);
                //dodanie najlepszego lokalnego rozw do tabu(ruchu ktory do niego doprowadzil)
                tabuList[tabuCity1][tabuCity2] = random.Next(10, cadency);


                //jesli wykonalo sie zadana ilosc iteracji generuj nowe rozwiazanie
                if (lifeEnd == 0)
                {
                    route = op.GenerateRandom(cityNumber);

                    //sprawdzenie czy przypadkiem nowa trasa nie jest najlepsza
                    routeCost = op.CalculateRouteCost(tspMatrix, cityNumber, route);
                    if (routeCost < bestCost)
                    {
                        route.CopyTo(bestRoute, 0);
                        bestCost = routeCost;
                        time = st.ElapsedMilliseconds;
                    }
                    lifeEnd = newLife;
                }
            }
            st.Stop();
        }
    }
}
