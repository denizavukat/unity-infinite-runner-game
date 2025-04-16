using System.Collections.Generic;
using UnityEngine;

public class MatrixSpawnManager : MonoBehaviour
{
    public static MatrixSpawnManager instance;

    public float rowConstant = 100.0f;
    private int rowCount;          
    public float cellLength;

    public float spawnMagnetChance = 0.1f;



    public string[] obstacleTypes = new string[] { "Barrier", "RedBarrier", "ParkingBollard" };
    public float twoRowObstacleChance = 0.8f;

    public float roadLength = 180f;
    private float offsetDistance = 10f;
    private GameObject lastRoadTile;

    void Awake()
    {
        instance = this;
        rowCount = Mathf.CeilToInt(rowConstant / PlayerController.instance.forwardSpeed);
        cellLength = Mathf.CeilToInt(roadLength / rowCount);
    }

    public void SpawnRoad(Vector3 position)
    {
        GameObject roadTile = ObjectPooler.instance.GetFromPool("Road");
        roadTile.transform.position = position;
        roadTile.transform.rotation = Quaternion.identity;
        lastRoadTile = roadTile;

        SpawnMatrixOnRoad(roadTile);
    }
    
    public void SpawnMatrixOnRoad(GameObject roadTile)
    {
        Vector3 tilePos = roadTile.transform.position;
        Vector3 originPositionOnRoad = new Vector3(PlayerController.instance.startingLineXPosition, 0, tilePos.z - roadLength + cellLength);

        MatrixGenerator matrixGenerator = new MatrixGenerator(rowCount, PlayerController.instance.maxLineCount);
        OnMatrixPointPathGenerator pointPathGenerator = new OnMatrixPointPathGenerator(matrixGenerator);

        OnMatrixObstaclePathGenerator obstaclePathGenerator = new OnMatrixObstaclePathGenerator(matrixGenerator, twoRowObstacleChance);

        SpawnPoints(pointPathGenerator.pointPath, matrixGenerator.grid, originPositionOnRoad);

        SpawnObstacles(matrixGenerator.grid, originPositionOnRoad);

        /*
        if (Random.value < spawnMagnetChance)
        {
            SpawnMagnet(matrixGenerator.grid, originPositionOnRoad);
        }*/
    }


    void SpawnPoints(List<Vector2Int> pointPath, bool[,] grid, Vector3 originPositionOnRoad)
    {
        foreach (Vector2Int cell in pointPath)
        {
            Vector3 spawnPos = originPositionOnRoad + new Vector3(cell.y * PlayerController.instance.lineWidth, 0, cell.x * cellLength);
            GameObject obj = ObjectPooler.instance.GetFromPool("Point");
            obj.transform.position = spawnPos;
            obj.transform.rotation = Quaternion.identity;
            grid[cell.x, cell.y] = false;
        }
    }

    void SpawnObstacles(bool[,] grid, Vector3 originPositionOnRoad)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                
                if (grid[r, c])
                {
                    Vector3 spawnPos = originPositionOnRoad + new Vector3(c * PlayerController.instance.lineWidth, 0, r * cellLength);
                    string type = obstacleTypes[Random.Range(0, obstacleTypes.Length)];
                    GameObject obj = ObjectPooler.instance.GetFromPool(type);
                    obj.transform.position = spawnPos;
                    obj.transform.rotation = Quaternion.identity;
                }
            }
        }
    }

   
    /*
    void SpawnMagnet(bool[,] grid, Vector3 originPositionOnRoad)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {

                if (!grid[r, c])
                {
                    Vector3 spawnPos = originPositionOnRoad + new Vector3(c * PlayerController.instance.lineWidth, 0, r * cellLength);
                    GameObject obj = ObjectPooler.instance.GetFromPool("Magnet");
                    obj.transform.position = spawnPos;
                    obj.transform.rotation = Quaternion.identity;
                    return;

                }
            }
        }
    }
    */
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnRoad(new Vector3(0, 0, i * roadLength));
        }
    }

    void Update()
    {
        if (PlayerController.instance.transform.position.z > lastRoadTile.transform.position.z - 2 * roadLength + offsetDistance)
        {
            SpawnRoad(new Vector3(0, 0, lastRoadTile.transform.position.z + roadLength));
            
        }
    }
}
