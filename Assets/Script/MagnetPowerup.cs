using System;
using UnityEngine;

public class MagnetPowerup : MonoBehaviour
{
    private bool magnetActive = false;


    private void OnEnable()
    {
        PlayerCollisionDetection.OnPlayerMagnetCollision += OnPlayerMagnetCollision;
    }

    private void OnPlayerMagnetCollision(Collider other)
    {
        ActivateMagnet();
    }

    void Update()
    {

    }

    private void ActivateMagnet()
    {
        magnetActive = true;
    }

    private void OnDisable()
    {
        PlayerCollisionDetection.OnPlayerMagnetCollision -= OnPlayerMagnetCollision;
    }
}
