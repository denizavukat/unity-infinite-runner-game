using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGenerator
{
    public bool[,] grid;

    private int rowCount;
    private int laneCount;

    public MatrixGenerator(int rowCount, int laneCount )
    {
        this.rowCount = rowCount;
        this.laneCount = laneCount;
        grid = new bool[rowCount, laneCount];
        for (int r = 0; r < rowCount; r++)
        {
            for (int c = 0; c < laneCount; c++)
            {
                grid[r, c] = false;
            }
        }
        
    }

    
}
