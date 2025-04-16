using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public delegate void OnPlayerObstacleCollisionDelegate(Collider other);
    public static event OnPlayerObstacleCollisionDelegate OnPlayerObstacleCollision;

    public delegate void OnPlayerPointCollisionDelegate(Collider other);
    public static event OnPlayerPointCollisionDelegate OnPlayerPointCollision;

    public delegate void OnPlayerGroundCollisionDelegate();
    public static event OnPlayerGroundCollisionDelegate OnPlayerGroundCollision;

    public delegate void OnPlayerMagnetCollisionDelegate(Collider other);
    public static event OnPlayerMagnetCollisionDelegate OnPlayerMagnetCollision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {

            OnPlayerGroundCollision?.Invoke();
           

        }

        if (other.CompareTag("Obstacle"))
        {
            OnPlayerObstacleCollision?.Invoke(other);
        }

        if (other.CompareTag("Point"))
        {
            OnPlayerPointCollision?.Invoke(other);
        }
        /*
        if (other.CompareTag("Magnet"))
        {
            OnPlayerMagnetCollision?.Invoke(other);
        }*/


    }
}
