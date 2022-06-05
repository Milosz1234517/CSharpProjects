using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEAProjekt3v1._0
{
    class GeneticAlghoritm
    {

        private int[] bestRoute;
        private int bestCost;

        private int[][] tspMatrix;
        private int cityNumber;

        public int[] BestRoute { get => bestRoute; set => bestRoute = value; }
        public int BestCost { get => bestCost; set => bestCost = value; }

        public GeneticAlghoritm(int[][] tspMatrix, int cityNumber)
        {
            this.tspMatrix = tspMatrix;
            this.cityNumber = cityNumber;
        }

        public void runAlghoritm(double time, int populationSize, bool PMX, double cross, double mut, bool test)
        {
            Stopwatch st = new Stopwatch();
            st.Start();

            double benchTime = 0;

            Random r = new Random();
            Operations op = new Operations();

            BestRoute = new int[cityNumber];
            BestCost = int.MaxValue;

            int[][] population = new int[populationSize][];
            int[][] parents = new int[populationSize][];


            for (int i = 0; i < populationSize; i++)
            {
                population[i] = op.GenerateRandom(cityNumber);
                parents[i] = new int[cityNumber];
            }

            while (true)
            {
                if (st.ElapsedMilliseconds >= time)
                {
                    st.Stop();
                    return;
                }

                for (int i = 0; i < populationSize; i++)
                {
                    PriorityQueue<int[], int> tournament = new PriorityQueue<int[], int>();
                    for (int j = 0; j < 4; j++)
                    {
                        int index = r.Next(populationSize);
                        int cost = op.CalculateRouteCost(tspMatrix, cityNumber, population[index]);
                        tournament.Enqueue(population[index], cost);

                    }
                    tournament.Dequeue().CopyTo(parents[i], 0);

                    int best = op.CalculateRouteCost(tspMatrix, cityNumber, population[i]);
                    if (best < BestCost)
                    {
                        BestCost = best;
                        population[i].CopyTo(BestRoute, 0);
                    }
                }

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

                parents.CopyTo(population, 0);

                for (int i = 0; i < cross * populationSize; i+=2)
                {
                    if (PMX == true)
                    {
                        population[i] = CrossPMX(population[i + 1], population[i]);
                        population[i + 1] = CrossPMX(population[i], population[i + 1]);
                    }
                    else
                    {
                        population[i] = CrossOX(population[i + 1], population[i]);
                        population[i + 1] = CrossOX(population[i], population[i + 1]);
                    }
                }

                for (int i = 0; i < mut * populationSize; i++)
                {
                    int index = r.Next(populationSize);
                    int bestBeforeMut = op.CalculateRouteCost(tspMatrix, cityNumber, population[index]);
                    if (bestBeforeMut < BestCost)
                    {
                        BestCost = bestBeforeMut;
                        population[index].CopyTo(BestRoute, 0);
                    }
                    Mutation(ref population[index]);
                }

            }
        }

        private void Mutation(ref int[] specimen)
        {
            Operations op = new Operations();
            Random r = new Random();

            int p1;
            int p2;
            do
            {
                p1 = r.Next(cityNumber);
                p2 = r.Next(cityNumber);
            } while (p1 == p2);

            op.Swap(p1, p2, ref specimen);
        }

        private int[] CrossPMX(int[] parent1, int[] parent2)
        {
            Random rand = new Random();
            Operations op = new Operations();
            int[] child = new int[cityNumber];

            int position1 = rand.Next(1, cityNumber - 3);
            int position2 = rand.Next(position1 + 1, cityNumber - 1);

            bool[] childUsed = new bool[cityNumber];
            int[] rewriteTab = new int[cityNumber];

            for (int i = 0; i < cityNumber; i++)
            {
                childUsed[i] = false;
                rewriteTab[i] = i;
            }

            for (int i = position1; i <= position2; i++)
            {
                child[i] = parent1[i];
                rewriteTab[parent1[i]] = parent2[i];
                childUsed[parent1[i]] = true;
            }

            for(int i = 0; i < cityNumber; i++)
            {
                if (i < position1 || i > position2)
                {
                    child[i] = rewriteTab[parent2[i]];
                    while (rewriteTab[child[i]] != child[i])
                        child[i] = rewriteTab[child[i]];
                }
            }

            return child;
        }

        private int[] CrossOX(int[] parent1, int[] parent2)
        {
            Random rand = new Random();
            Operations op = new Operations();
            int[] child = new int[cityNumber];

            int position1 = rand.Next(1, cityNumber - 3);
            int position2 = rand.Next(position1 + 1, cityNumber - 1);

            bool[] childUsed = new bool[cityNumber];

            for (int i = 0; i < cityNumber; i++)
            {
                childUsed[i] = false;
            }

            for (int i = position1; i <= position2; i++)
            {
                child[i] = parent1[i];
                childUsed[parent1[i]] = true;

            }

            int childIndex = 0;
            int parentIndex = 0;
            while (parentIndex < cityNumber)
            {
                if (childUsed[parent2[parentIndex]] == false)
                {
                    child[childIndex] = parent2[parentIndex];
                    childIndex++;
                    if (childIndex == position1)
                        childIndex = position2 + 1;
                }
                parentIndex++;
            }

            return child;
        }
    }
}
