using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPEA
{
    class Node
    {
        public int myNumber;
        public int[][] reducedMatrix;
        public int myCost;
        public int reducedMatrixCost;
        public int treeLvl;
        public Node parent;

        //funkcja redukcji macierzy - wypelnianie oo i szukanie minimow z wierszy i kolumn
        public void ReduceMatrix(int[][] parentMatrix, int cities, bool startup = false)
        {
            //tablice minimow
            int[] minRow = new int[cities];
            int[] minCol = new int[cities];
            reducedMatrix = new int[cities][];
            for (int j = 0; j < cities; j++)
            {
                //inicjalizacja poczatkowych minimow z kolumn jako oo - bedziemy porownywac pojedynczo i zapisywac najmniejsze 
                minCol[j] = int.MaxValue;
            }
            for (int i = 0; i < cities; i++)
            {
                reducedMatrix[i] = new int[cities];
                for (int j = 0; j < cities; j++)
                {
                    //odczytujemy wartosc z macierzy - matki i ustawiamy oo na odpowiednim wierszu i kolumnie
                    reducedMatrix[i][j] = parentMatrix[i][j];
                    if (startup == false)
                    {
                        if (i == parent.myNumber)
                            reducedMatrix[i][j] = int.MaxValue;
                        if (j == myNumber)
                            reducedMatrix[i][j] = int.MaxValue;
                        if (i == myNumber && j == parent.myNumber)
                            reducedMatrix[i][j] = int.MaxValue;
                    }
                    if (i == j)
                        reducedMatrix[i][j] = int.MaxValue;
                }
                //gdy wiersz zredukowanej macierzy jest pelny zapisujemy minimum z niego, jesli mni = oo to  min = 0
                minRow[i] = reducedMatrix[i].Min();
                if (minRow[i] == int.MaxValue)
                    minRow[i] = 0;
                //redukujemy wiersz o wyliczone minimum i zapisujemy min dla kolumn
                for (int j = 0; j < cities; j++)
                {
                    if (reducedMatrix[i][j] != int.MaxValue)
                        reducedMatrix[i][j] -= minRow[i];
                    if (minCol[j] > reducedMatrix[i][j])
                        minCol[j] = reducedMatrix[i][j];
                }

            }
            //ostatnia petla do odjecia min z kolumn od kazdej wartosci w kolumnie, dla min = oo -> min = 0
            for (int i = 0; i < cities; i++)
            {

                for (int j = 0; j < cities; j++)
                {
                    if (minCol[j] == int.MaxValue)
                        minCol[j] = 0;
                    if (reducedMatrix[i][j] != int.MaxValue)
                        reducedMatrix[i][j] -= minCol[j];
                }
            }
            //liczymy od razu koszt redukcji
            int columnCost = minCol.Sum();
            int rowCost = minRow.Sum();
            reducedMatrixCost = columnCost + rowCost;
            if (startup == true)
                myCost = reducedMatrixCost;
        }
    }
}
