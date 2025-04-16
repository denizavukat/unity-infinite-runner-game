using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    public float offScreenDistance = 100.0f;
    public string objectType;

    void Update()
    {
        if(PlayerController.instance.transform.position.z - transform.position.z > offScreenDistance)
        {
            ObjectPooler.instance.ReturnToPool( objectType,gameObject);
        }
        
    }
}
