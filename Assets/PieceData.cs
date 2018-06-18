using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "Piece_Data", menuName = "Eldoir/Piece")]
public class PieceData : MonoBehaviour
{
    private const int defaultGridSize = 5;

    [Range(1, 5)]
    public int gridSize = defaultGridSize;

    public CellRow[] cells = new CellRow[defaultGridSize];


    public InputField[,] GetCells()
    {
        InputField[,] ret = new InputField[gridSize, gridSize];

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                ret[i, j] = cells[i].row[j];
            }
        }

        return ret;
    }

    public int GetCountActiveCells()
    {
        int count = 0;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (cells[i].row[j]) count++;
            }
        }

        return count;
    }

    [System.Serializable]
    public class CellRow
    {
        public InputField[] row = new InputField[defaultGridSize];
    }
}



