using System;
using System.IO;

namespace PEAProjekt2
{
    class Data
    {
        private int[][] tspMatrix;
        private int cityNumber;
        private string file;

        public string getFileName()
        {
            return file;
        }

        public int[][] getTspMatrix()
        {
            return tspMatrix;
        }

        public int getCityNumber()
        {
            return cityNumber;
        }

        public void ReadMatrix(string name)
        {
            string fileContent = File.ReadAllText(name);
            string[] integerStrings = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] integers = new int[integerStrings.Length];
            int number;
            int idx = 0;
            for (int n = 0; n < integerStrings.Length; n++)
            {
                //integers[n] = int.Parse(integerStrings[n]);
                if (int.TryParse(integerStrings[n], out number))
                {
                    integers[idx] = number;
                    idx++;
                }

            }
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
            file = name;

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
