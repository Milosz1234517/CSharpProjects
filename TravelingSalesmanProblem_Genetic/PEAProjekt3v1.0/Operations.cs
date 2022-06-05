using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEAProjekt3v1._0
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

    }
}
