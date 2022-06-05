using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEAProjekt2
{
    class Operations
    {
        public void Swap(int i, int j, ref int[] tab)
        {
            int tmp = tab[i];
            tab[i] = tab[j];
            tab[j] = tmp;
        }

        public int CalculateRouteCost(int[][] tspMatrix, int cityNumber, int[] tab)
        {
            int cost = 0;
            int iterateEnd = cityNumber - 1;
            for (int i = 0; i < iterateEnd; i++)
            {
                cost += tspMatrix[tab[i]][tab[i + 1]];
            }
            cost += tspMatrix[tab[cityNumber - 1]][tab[0]];
            return cost;
        }

        public int[] GenerateRandom(int cityNumber)
        {
            int[] tab = new int[cityNumber];
            for (int i = 0; i < cityNumber; i++)
            {
                tab[i] = i;
            }
            Random random = new Random();
            tab = tab.OrderBy(x => random.Next()).ToArray();
            return tab;
        }

        public int[] GenerateStart(int[][] tspMatrix, int cityNumber)
        {
            int[] route = GenerateRandom(cityNumber);
            int[] bestLocalRoute = new int[cityNumber];
            while (true)
            {
                int bestNeighbourCost = int.MaxValue;
                int resultCost = CalculateRouteCost(tspMatrix, cityNumber, route);

                for (int i = 0; i < cityNumber; i++)
                {
                    for (int j = i + 1; j < cityNumber; j++)
                    {
                        Swap(i, j, ref route);
                        int neighbourCost = CalculateRouteCost(tspMatrix, cityNumber, route);
                        if (neighbourCost < bestNeighbourCost)
                        {
                            bestNeighbourCost = neighbourCost;
                            route.CopyTo(bestLocalRoute, 0);
                        }
                        Swap(i, j, ref route);
                    }
                }

                if (bestNeighbourCost < resultCost)
                {
                    bestLocalRoute.CopyTo(route, 0);
                }
                else return route;
            }

        }

        public double CalculateTemperature(int[][] tspMatrix, int cityNumber)
        {
            Random random = new Random();

            int[] route;

            int delta;
            int sum = 0;

            int[] indexes = GenerateRandom(cityNumber);
            int maxIJ = cityNumber - 1;

            for (int i = 0; i < 10000; i++)
            {
                if(i % 5 == 0)
                    indexes = GenerateRandom(cityNumber);

                int iI = random.Next(cityNumber);
                int iJ = random.Next(maxIJ);
                if (iI == iJ) iJ = iI + 1;

                int sI = indexes[iI];
                int sJ = indexes[iJ];

                route = GenerateRandom(cityNumber);
                int[] neighbour = new int[cityNumber];

                route.CopyTo(neighbour, 0);

                Swap(sI, sJ, ref neighbour);

                delta = Math.Abs(CalculateRouteCost(tspMatrix, cityNumber, route) - CalculateRouteCost(tspMatrix, cityNumber, neighbour));
                sum += delta;

            }

            sum = sum / 10000;

            return (-1 * sum) / Math.Log(0.99);
        }

        public int[] GenerateRoute(int[][] tspMatrix, int cityNumber, int vertice)
        {
            //Random r = new Random();
            //int startNode = r.Next(cityNumber);
            int startNode = vertice;
            int minimum = int.MaxValue;
            int minimumNode = startNode;
            int[] route = new int[cityNumber];
            bool[] usedNodes = new bool[cityNumber];

            for (int i = 0; i < cityNumber; i++)
            {
                usedNodes[i] = false;
            }

            usedNodes[startNode] = true;
            route[0] = startNode;

            for (int j = 1; j < cityNumber; j++)
            {
                for (int i = 0; i < cityNumber; i++)
                {
                    if (usedNodes[i] == true || startNode == i) continue;

                    if (tspMatrix[startNode][i] < minimum)
                    {
                        minimum = tspMatrix[startNode][i];
                        minimumNode = i;
                    }
                }
                startNode = minimumNode;
                route[j] = minimumNode;
                minimum = int.MaxValue;
                usedNodes[minimumNode] = true;
            }

            return route;
        }

        public double ProbabilityCalculate(double T, int diference)
        {
            return Math.Exp(diference / T);
        }

        public void UpdateTabu(int cityNumber, ref int[][] tabuList)
        {
            for (int i = 0; i < cityNumber; i++)
            {
                for (int j = 0; j < cityNumber; j++)
                {
                    if (tabuList[i][j] > 0)
                        tabuList[i][j] = tabuList[i][j] - 1;
                }
            }
        }

        public void ClearTabu(int cityNumber, ref int[][] tabuList)
        {
            for (int i = 0; i < cityNumber; i++)
            {
                for (int j = 0; j < cityNumber; j++)
                {
                    tabuList[i][j] = 0;
                }
            }
        }

    }
}
