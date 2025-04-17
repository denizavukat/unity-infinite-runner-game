using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    [SerializeField] float roadLength = 180.0f;
    [SerializeField] float cityLength = 50.0f;
    
    private float offsetDistance = 10.0f;
    private GameObject lastRoadTile;
    private GameObject lastCityTile;
    public static SpawnManager instance;

    public float numberObstaclesOnRoad = 20;
    string[] obstacleTypes = new string[] { "Barrier", "RedBarrier", "ParkingBollard","Point" };

    public float exitPositionObstacle;

    

 
    
    private void Awake()
    {
        instance = this;
    }

    public void SpawnRoad( Vector3 position)
    {
        GameObject roadTile = ObjectPooler.instance.GetFromPool("Road");
        roadTile.transform.position = position;
        roadTile.transform.rotation = Quaternion.identity;
        lastRoadTile = roadTile;
        
        SpawnObstacleOnRoad(roadTile);
        exitPositionObstacle = 0.0f;

    }

    public void SpawnCity(Vector3 position)
    {
        GameObject cityTile = ObjectPooler.instance.GetFromPool("City");
        cityTile.transform.position = position;
        cityTile.transform.rotation = Quaternion.identity;
        lastCityTile = cityTile;
        Debug.Log("citye girdi");


    }

    public void SpawnObstacleOnRoad(GameObject roadTile)
    {
        Vector3 tilePosition = roadTile.transform.position;
        for (int i = 0; i< numberObstaclesOnRoad; i++)
        {
            int randomLine = Random.Range(0, PlayerController.instance.maxLineCount);
            float startPositionObstacle = exitPositionObstacle + Random.Range(7,15);

            float positionZ = tilePosition.z - roadLength + startPositionObstacle;

            Vector3 obstaclePosition = new Vector3(PlayerController.instance.startingLineXPosition + randomLine * PlayerController.instance.lineWidth,0,positionZ);

            string obstacleType = obstacleTypes[Random.Range(0, obstacleTypes.Length)];
            GameObject road = ObjectPooler.instance.GetFromPool(obstacleType);
            road.transform.position = obstaclePosition;
            road.transform.rotation = Quaternion.identity;

            exitPositionObstacle = startPositionObstacle;
        }
    }

  




    void Start()
    {
        exitPositionObstacle= PlayerController.instance.transform.position.z + 50;
        lastCityTile = ObjectPooler.instance.GetFromPool("City");
       

        for (int i = 0; i < 5; i++)

        {
            Debug.Log("city spawnlama denemekte");
            SpawnCity(new Vector3(0, 0, i * cityLength));
            
        }

        lastRoadTile = ObjectPooler.instance.GetFromPool("Road");
        
        for (int i = 0; i<3;i++) SpawnRoad(new Vector3(0, 0, i * roadLength));

        

    }

    void Update()
    {
        Debug.Log(lastCityTile.transform.position);
        if (PlayerController.instance.transform.position.z > lastCityTile.transform.position.z)
        {
            SpawnCity(new Vector3(0, 0, lastCityTile.transform.position.z + cityLength));
            
        }
        if (PlayerController.instance.transform.position.z > lastRoadTile.transform.position.z - 2 * roadLength + offsetDistance)
        {
            SpawnRoad(new Vector3(0, 0, lastRoadTile.transform.position.z + roadLength));
        }

        


    }
}
