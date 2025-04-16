using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMatrixObstaclePathGenerator
{
    float twoRowObstacleChance;
    //public List<Vector2Int> obstaclePath;

    public OnMatrixObstaclePathGenerator(MatrixGenerator matrixGenerator, float twoRowObstacleChance)
    {
        this.twoRowObstacleChance = twoRowObstacleChance;
        AddObstacles(matrixGenerator);
        
    }

    public void AddObstacles(MatrixGenerator matrixGenerator)
    {
        //obstaclePath = new List<Vector2Int>();
        int rowCount = matrixGenerator.grid.GetLength(0);
        int laneCount = matrixGenerator.grid.GetLength(1);

        for (int r = 0; r < rowCount; r++)
        {
            List<int> availableLanes = new List<int>();
            for (int c = 0; c < laneCount; c++)
            {
                if (!matrixGenerator.grid[r, c])
                    availableLanes.Add(c);
            }

            int obstaclesInARow = Random.Range(0, laneCount-1); // 0-1-2

            for (int i = 0; i < obstaclesInARow; i++) // i: 0-1 
            {

                int chosenLane = availableLanes[i];
                matrixGenerator.grid[r, chosenLane] = true;
                //obstaclePath.Add(new Vector2Int(r, chosenLane));
                if (r < rowCount - 1 && Random.value < twoRowObstacleChance)
                {
                    matrixGenerator.grid[r + 1, chosenLane] = true;
                    //obstaclePath.Add(new Vector2Int(r+1, chosenLane));
                }
            }
        }
    }
}
