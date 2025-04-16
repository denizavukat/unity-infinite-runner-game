using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputKeyDetection : MonoBehaviour
{

    public delegate void OnPlayerJumpDelegate();
    public static event OnPlayerJumpDelegate OnPlayerJump;

    public delegate void OnPlayerGoRightDelegate();
    public static event OnPlayerGoRightDelegate OnPlayerGoRight;

    public delegate void OnPlayerGoLeftDelegate();
    public static event OnPlayerGoLeftDelegate OnPlayerGoLeft;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPlayerJump?.Invoke();
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            OnPlayerGoLeft?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            OnPlayerGoRight?.Invoke();

        }

    }
}
