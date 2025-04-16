using System.Collections.Generic;
using UnityEngine;

public class OnMatrixPointPathGenerator
{
    public List<Vector2Int> pointPath;

    public OnMatrixPointPathGenerator(MatrixGenerator matrixGenerator)
    {
        GeneratePointPath(matrixGenerator);
    }

    void GeneratePointPath(MatrixGenerator matrixGenerator)
    {
        
        pointPath = new List<Vector2Int>();

        int rowCount = matrixGenerator.grid.GetLength(0);
        int laneCount = matrixGenerator.grid.GetLength(1);


        int startCol = Random.Range(0,laneCount);
        

        pointPath.Add(new Vector2Int(0, startCol));
        matrixGenerator.grid[0, startCol] = true;
        int currentCol = startCol;

        for (int r = 1; r < rowCount; r++)
        {
            List<int> possibleCols = new List<int>();  
            possibleCols.Add(currentCol);
            if (currentCol > 0)
                possibleCols.Add(currentCol - 1);
            if (currentCol < laneCount - 1)
                possibleCols.Add(currentCol + 1);
            

            int chosenCol = possibleCols[Random.Range(0, possibleCols.Count)];
            pointPath.Add(new Vector2Int(r, chosenCol));

            matrixGenerator.grid[r, chosenCol] = true;
            currentCol = chosenCol;
        }
    }
}
