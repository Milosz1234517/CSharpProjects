using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProjektPEA
{
    class Test
    {
        public void BruteForceTest(int cities)
        {
            Data data = new Data();
            Alghoritm alg = new Alghoritm();
            double result = 0;

            for (int i = 0; i < 150; i++)
            {
                data.GenerateRandomMatrix(cities);
                if (i > 49)
                {
                    Stopwatch st = new Stopwatch();
                    st.Start();
                    alg.BruteForce(cities, data.tspMatrix);
                    st.Stop();
                    TimeSpan ts = st.Elapsed;
                    double elapsed = ts.TotalMilliseconds;
                    result = result + elapsed;
                }
                else
                {
                    alg.BruteForce(cities, data.tspMatrix);
                }
                Console.WriteLine(i);
            }
            result = result / 100;
            Console.WriteLine("Wynik: " + result);

        }

        public void HeldKarpTest(int cities)
        {
            Data data = new Data();
            Alghoritm alg = new Alghoritm();
            double result = 0;

            for (int i = 0; i < 150; i++)
            {
                data.GenerateRandomMatrix(cities);
                if (i > 49)
                {
                    Stopwatch st = new Stopwatch();
                    st.Start();
                    alg.HeldKarp(cities, data.tspMatrix);
                    st.Stop();
                    TimeSpan ts = st.Elapsed;
                    double elapsed = ts.TotalMilliseconds;
                    result = result + elapsed;
                }
                else
                {
                    alg.HeldKarp(cities, data.tspMatrix);
                }
                Console.WriteLine(i);
            }
            result = result / 100;
            Console.WriteLine("Wynik: " + result);

        }

        public void BandBTest(int cities)
        {
            Data data = new Data();
            Alghoritm alg = new Alghoritm();
            double result = 0;

            for (int i = 0; i < 150; i++)
            {
                data.GenerateRandomMatrix(cities);
                if (i > 49)
                {
                    Stopwatch st = new Stopwatch();
                    st.Start();
                    alg.BandB(cities, data.tspMatrix);
                    st.Stop();
                    TimeSpan ts = st.Elapsed;
                    double elapsed = ts.TotalMilliseconds;
                    result = result + elapsed;
                }
                else
                {
                    alg.BandB(cities, data.tspMatrix);
                }
                Console.WriteLine(i);
            }
            result = result / 100;
            Console.WriteLine("Wynik: " + result);

        }

        public void BandBDepthTest(int cities)
        {
            Data data = new Data();
            Alghoritm alg = new Alghoritm();
            double result = 0;

            for (int i = 0; i < 150; i++)
            {
                data.GenerateRandomMatrix(cities);
                if (i > 49)
                {
                    Stopwatch st = new Stopwatch();
                    st.Start();
                    alg.BandBDepth(cities, data.tspMatrix);
                    st.Stop();
                    TimeSpan ts = st.Elapsed;
                    double elapsed = ts.TotalMilliseconds;
                    result = result + elapsed;
                }
                else
                {
                    alg.BandBDepth(cities, data.tspMatrix);
                }
                Console.WriteLine(i);
            }
            result = result / 100;
            Console.WriteLine("Wynik: " + result);

        }
    }
}
