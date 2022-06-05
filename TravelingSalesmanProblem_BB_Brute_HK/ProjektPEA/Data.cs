using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPEA
{
    class Data
    {
        public int[][] tspMatrix;
        public int cityNumber;

        public void ReadMatrix(string name)
        {
            string fileContent = File.ReadAllText(name);
            string[] integerStrings = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] integers = new int[integerStrings.Length];
            for (int n = 0; n < integerStrings.Length; n++)
                integers[n] = int.Parse(integerStrings[n]);
            cityNumber = integers[0];
            tspMatrix = new int[cityNumber][];
            int index = 1;
            for (int i = 0; i < cityNumber; i++)
            {
                tspMatrix[i] = new int[cityNumber];
                for (int j = 0; j < cityNumber; j++)
                {
                    tspMatrix[i][j] = integers[index];
                    index++;
                }
            }

        }

        public void GenerateRandomMatrix(int cities)
        {
            cityNumber = cities;
            Random rnd = new Random();
            tspMatrix = new int[cityNumber][];
            for (int i = 0; i < cityNumber; i++)
            {
                tspMatrix[i] = new int[cityNumber];
            }

            for (int i = 0; i < cityNumber; i++)
            {
                for (int j = 0; j < cityNumber; j++)
                {
                    if (i == j)
                        tspMatrix[i][j] = -1;
                    else
                        tspMatrix[i][j] = rnd.Next(0, 100);
                }
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < cityNumber; i++)
            {
                for (int j = 0; j < cityNumber; j++)
                {
                    Console.Write(tspMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }

        }
    }
}
