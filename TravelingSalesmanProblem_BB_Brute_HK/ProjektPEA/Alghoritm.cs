using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPEA
{
    class Alghoritm
    {
        //BB upper - wynik,  BBroute - trasa wynikowa
        int[] BBroute;
        int upper;

        int[] BBDepthRoute;
        public Node upperBoundD;

        //Brute force wyniki i tablica kolorow zapisujaca odwiedziny wierzcholkow
        int bruteForceCost;
        int[] route;
        private char[] colors;

        //Wyniki karp oraz mapy przechowujace sciezki i koszta dla podzbiorow(do szybkich wstawien zamiast wywolan rekurencji)
        int heldKarpCost;
        string trasa;
        private Dictionary<string, int> setCollection;
        private Dictionary<string, string> path;

        public void BandBPrintResult()
        {
            Console.WriteLine();
            Console.WriteLine("Koszt Branch and Bound: " + upper);
            Console.WriteLine();
            for (int i = BBroute.Length - 1; i > -1; i--)
            {
                Console.Write(BBroute[i] + " ");
            }
            Console.Write(0);
            Console.WriteLine();
        }

        public void BandBDepthPrintResult()
        {
            Console.WriteLine();
            Console.WriteLine("Koszt Branch and Bound Depth: " + upperBoundD.myCost);
            Console.WriteLine();
            for (int i = BBDepthRoute.Length - 1; i > -1; i--)
            {
                Console.Write(BBDepthRoute[i] + " ");
            }
            Console.Write(0);
            Console.WriteLine();
        }

        public void BruteForcePrintResult()
        {
            Console.WriteLine();
            Console.WriteLine("Koszt Brute Force: " + bruteForceCost);
            Console.WriteLine();
            for (int i = 0; i < route.Length; i++)
            {
                Console.Write(route[i] + " ");
            }
            Console.Write(0);
            Console.WriteLine();
        }

        public void HeldKarpPrintResult()
        {
            Console.WriteLine();
            Console.WriteLine("Koszt Held-Karp: " + heldKarpCost);
            Console.WriteLine();
            char[] reverse = trasa.ToCharArray();
            Array.Reverse(reverse);
            Console.Write(0);
            for (int i = 0; i < reverse.Length; i++)
            {
                Console.Write(reverse[i]);
            }
            Console.Write(" " + 0);
            Console.WriteLine();
        }

        public void BandBDepth(int cityNumber, int[][] tspMatrix)
        {
            Node start = new Node();
            start.myNumber = 0;
            start.ReduceMatrix(tspMatrix, cityNumber, true);
            start.treeLvl = 0;
            start.parent = null;

            upperBoundD = new Node();
            upperBoundD.myCost = int.MaxValue;

            BBDepthRoute = new int[cityNumber];

            BandBDepthRecursive(start, cityNumber, 0);

        }

        private void BandBDepthRecursive(Node parent, int cityNumber, int myTreeLvl)
        {
            if (upperBoundD.myCost <= parent.myCost)
                return;
            if (myTreeLvl == cityNumber - 1)
            {
                upperBoundD = parent;
                Node tmp = parent;
                for (int i = 0; i < cityNumber; i++)
                {
                    BBDepthRoute[i] = tmp.myNumber;
                    tmp = tmp.parent;
                }

                return;
            }
            for (int i = 1; i < cityNumber; i++)
            {
                if (parent.reducedMatrix[parent.myNumber][i] == int.MaxValue)
                    continue;
                Node child = new Node();
                child.myNumber = i;
                child.parent = parent;
                child.treeLvl = myTreeLvl + 1;
                child.ReduceMatrix(parent.reducedMatrix, cityNumber);
                child.myCost = parent.reducedMatrix[parent.myNumber][i] + parent.myCost + child.reducedMatrixCost;
                BandBDepthRecursive(child, cityNumber, child.treeLvl);
            }
        }

        public void BandB(int cityNumber, int[][] tspMatrix)
        {
            //kolejka priorytetowa do przechowywania wezlow drzewa w kolejnosci od najmniejszych
            PriorityQueue<Node, int> nodeQueue = new PriorityQueue<Node, int>();
            //wierzcholek 0 i jego parametry
            Node start = new Node();
            BBroute = new int[cityNumber];
            upper = int.MaxValue;
            start.myNumber = 0;
            //poczatkowa redukcja macierzy
            start.ReduceMatrix(tspMatrix, cityNumber, true);
            start.treeLvl = 0;
            start.parent = null;
            BBroute[start.treeLvl] = start.myNumber;
            Node parentN = start;
            Node upperBound = new Node();
            upperBound.myCost = int.MaxValue;
            //petla wykonywana do poki nie znajdziemy trasy
            while (true)
            {
                //ustawiamy poziom drzewa na nizszy od rodzica - przegladamy dzieci dzieci i dodajemy je do kolejki
                int myTreeLvl = parentN.treeLvl + 1;
                for (int i = 1; i < cityNumber; i++)
                {
                    //jesli dziecko ma w macierzy wartosc 2147483647 to znaczy ze albo je odwiedzilismy wczesniej albo jest to nasz numer
                    if (parentN.reducedMatrix[parentN.myNumber][i] == int.MaxValue)
                        continue;
                    //tworzymy obiekt wezla dziecka i inicjalizujemy go
                    Node child = new Node();
                    child.parent = parentN;
                    child.myNumber = i;
                    child.treeLvl = myTreeLvl;
                    //redukcja macierzy
                    child.ReduceMatrix(parentN.reducedMatrix, cityNumber);
                    child.myCost = parentN.reducedMatrix[parentN.myNumber][i] + parentN.myCost + child.reducedMatrixCost;
                    //istotne - dziecko pamieta wezel swojego rodzica a rodzic bedzie pamietal swojego rodzica ipt.
                    //dodajemy wezel dziecko do kolejki
                    if (child.treeLvl == cityNumber - 1)
                    {
                        Node first = nodeQueue.Peek();
                        if (first.myCost == child.myCost)
                            upperBound = child;
                    }
                    nodeQueue.Enqueue(child, child.myCost);
                }
                //wyciagamy wezel z kolejki - otymistycznie bedzie to jeden z wezlow dzieci dodanych wyzej
                Node minChild = nodeQueue.Dequeue();
                if (minChild.myCost >= upperBound.myCost)
                    minChild = upperBound;
                //ustawiamy ten wezel jako rodzica i dla kolejnego obiego bedziemy przegladac jego dzieci
                parentN = minChild;
                if (minChild.treeLvl == cityNumber - 1)
                {
                    //jesli tu jestesmy znaczy ze jestesmy na lisciu dla ktorego koszt sciezki byl minimalny, wiec zapisujemy i konczymy
                    upper = minChild.myCost;
                    Node tmp = minChild;
                    int counter = 0;
                    while (tmp.parent != null)
                    {
                        BBroute[counter] = tmp.myNumber;
                        counter = counter + 1;
                        tmp = tmp.parent;
                    }
                    break;

                }
            }
        }

        private void BruteForceRecursive(int start, int path, int tmpCost, int[] tmpRoute, int cityNumber, int[][] tspMatrix)
        {
            //zapisujemy wierzcholek do tymczasowej sciezki
            tmpRoute[path] = start;
            //zwiekszamy dlugosc sciezki(poziom drzewa)
            path++;
            //kolorujemy wierzcholek jako odwiedzony
            colors[start] = 'W';
            //jesli jestesmy lisciem
            if (path == cityNumber)
            {
                //dodajemy sotatni koszt N - 0
                tmpCost += tspMatrix[start][0];
                //jesli sciezka lepsza przepisujemy koszt i sciezke z wartosci tymczasowych do glownych
                if (bruteForceCost > tmpCost)
                {
                    bruteForceCost = tmpCost;
                    for (int i = 0; i < cityNumber; i++)
                    {
                        route[i] = tmpRoute[i];
                    }
                }
                //teraz bedziemy sie cofac do pierwszego wolnego sasiada wierzcholkow wyzej 
                //wiec cofamy naliczenie kosztu 
                tmpCost -= tspMatrix[start][0];

            }
            else
            {
                //jesli jestesmy tutaj znaczy ze eksplorujemy drzewo gdzies w srodku, szukamy pierwszego wolnego sasiada
                for (int i = 0; i < cityNumber; i++)
                {
                    if (colors[i] != 'W')
                    {
                        //gdy znajdzie sie sasiad to rekurencyjnie wywolujemy funkcje i naliczamy koszt dojscia do tego sasiada
                        tmpCost += tspMatrix[start][i];
                        BruteForceRecursive(i, path, tmpCost, tmpRoute, cityNumber, tspMatrix);
                        //tutaj bedziemy sie cofac i szukac kolejnego wolnego sasiada, jesli nie ma to cofamy sie poziom wyzej
                        tmpCost -= tspMatrix[start][i];
                    }

                }

            }
            //w tym  miejscu jestesmy gdy odwiedzimy lisc lub gdy sprawdzilismy wszystkich sasiadow
            //ustawiamy wierzcholek spowrotem na wolny aby gdy sie cofniemy byl on znow dostepny
            //zmniejszmy path czyli idziemy w gore drzewa o 1 poziom
            colors[start] = 'B';
            path--;
        }

        public void BruteForce(int cityNumber, int[][] tspMatrix)
        {
            //inicjalizacja dlugosci sciezki(przydatna do odtwarzania jej)
            int pathLenght = 0;
            //tymczasowe koszt i sciezka do zapisu aktualnych wartosci i do porownania z glownymi i ew zamiany
            int tmpCost = 0;
            int[] tmpRoute = new int[cityNumber];
            //glowny koszt i sciezka oraz tablica kolorow
            route = new int[cityNumber];
            colors = new char[cityNumber];
            bruteForceCost = int.MaxValue;
            for (int i = 0; i < cityNumber; i++)
            {
                colors[i] = 'B';
            }
            //1 wywolanie dla poczatkowych wartosci
            BruteForceRecursive(0, pathLenght, tmpCost, tmpRoute, cityNumber, tspMatrix);
        }

        int HeldKarpRecursive(int start, HashSet<int> subSet, int cityNumber, int[][] tspMatrix)
        {
            //tworzymy pomocnicza zmienna lokalna i na poczatku ustawiamy na nieskonczonosc
            int minimum = int.MaxValue;
            //budujemy klucz do map na zasadzie dla zbioru np 1{234} bedzie to 1 2 3 4,
            //w jednej mapie pod tym kluczem znajdziemy trase 1 - 2 - 3 - 4 - 0 (wierzcholki w kolejnosci minimalnej sciezki),
            //w drugiej wartosc minimalnej sciezki dla tej trasy
            string key = "";
            key = key + start + " ";
            string kopiaZapasowa;
            foreach (var keyPart in subSet)
            {
                key = key + keyPart + " ";
            }
            //sprawdzamy mapy, jesli w mapie posiadamy klucz oznacza to ze juz taka sciezke odwiedzilismy i po prostu odczytujemy trase oraz wartosc sciezki 
            //i wracamy z funkcji zwracajac zapamietana wartosc
            if (setCollection.ContainsKey(key))
            {
                trasa = path[key];
                return setCollection[key];
            }
            //jesli jestesmy na lisciu, aktualizujemy kolekcje i zwracamy odleglosc wierzcholek - 0
            else if (!subSet.Any())
            {
                setCollection.Add(key, tspMatrix[start][0]);
                path.Add(key, trasa);
                return tspMatrix[start][0];
            }
            //w tym przypadku eksplorujemy nowa sciezke i jestesmy gdzies w srodku drzewa 
            else
            {
                //dla kazdego wierzcholka z podzbioru np
                //w wywolaniu funkcji podajemy 0 i zbior {1,2,3}
                //to w pierwszym  obiegu petli tworzymy 1 {2,3} oraz d = 0-1, w drugim 2{1,3} d = 0-2  i ostatnim 3{1,2} d = 0-3
                foreach (var vertice in subSet)
                {
                    //czyscimy trase i zapamietujemy jej kopie
                    kopiaZapasowa = trasa;
                    trasa = "";
                    //tworzymy podzbior dla wierzcholka 
                    HashSet<int> newSubSet = new HashSet<int>(subSet);
                    newSubSet.Remove(vertice);
                    //obliczamy krawedz wierzcholek z wejscia - wierzcholek z podzbioru 
                    int d = tspMatrix[start][vertice];
                    //obliczamy koszt sciezki dla vierzcholkow z podzbioru rekurencyjnie
                    //przy eksploracji nowej trasy rekurencje dojda az do liscia drzewa i wruca tutaj 
                    int subSetValue = HeldKarpRecursive(vertice, newSubSet, cityNumber, tspMatrix);
                    //poniewaz trase zapamietujemy w string i zapamietujemy ja od liscia w gore to zapisujemy odwrocony juz wierzcholek
                    //na zasadzie dla 12 zapiszemy 21 aby przy finalnym odczycie trasy po odwroceniu stringa bylo spowrotem 12
                    string V = vertice + "";
                    char[] reverseV = V.ToCharArray();
                    Array.Reverse(reverseV);
                    V = "";
                    for (int s = 0; s < reverseV.Length; s++)
                    {
                        V = V + reverseV[s];
                    }
                    //dodajemy do trasy wierzcholek  
                    trasa = trasa + V + " ";
                    //wyliczamy wartosc dla zbioru z wywolania funkcji i krawedzi wywolanie-podzbior
                    int result = subSetValue + d;
                    //ustawiamy ta wartosc jako minimum, dlatego na poczatku wartosc minimum = oo
                    if (minimum > result)
                    {
                        minimum = result;
                    }
                    //jesli wyznaczony result nie okazal sie minimum przywracamy trase z kopii zapasowej
                    else
                        trasa = kopiaZapasowa;
                }
                //uwzgledniamy eksploracje w mapach i wychodzimy z funkcji zwracajac minimum 
                setCollection.Add(key, minimum);
                path.Add(key, trasa);
                return minimum;
            }
        }

        public void HeldKarp(int cityNumber, int[][] tspMatrix)
        {
            //utworzenie pierwszego zbioru 0 {1,2,3..N}
            HashSet<int> beginign = new HashSet<int>();
            for (int i = 1; i < cityNumber; i++)
            {
                beginign.Add(i);
            }
            //inicjalizacja potrzebnych zmiennych i kolekcji
            trasa = "";
            heldKarpCost = 0;
            setCollection = new Dictionary<string, int>();
            path = new Dictionary<string, string>();
            //pierwsze wywolanie funkcji rekurencyjnej dla 0 {1,2,3...N}
            heldKarpCost = HeldKarpRecursive(0, beginign, cityNumber, tspMatrix);
        }
    }
}
