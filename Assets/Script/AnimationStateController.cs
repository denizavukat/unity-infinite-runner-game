using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;

    private void OnEnable()
    {
        PlayerInputKeyDetection.OnPlayerJump += OnPlayerJump;
        PlayerController.OnPlayerFall += OnPlayerFall;
        PlayerCollisionDetection.OnPlayerGroundCollision += OnPlayerGroundCollision;

    }

    private void OnPlayerJump()
    {
        animator.SetTrigger("Jump");
       
       
        
    }
    private void OnPlayerFall()
    {
        animator.SetTrigger("Fall");

    }

    private void OnPlayerGroundCollision()
    {


        animator.SetTrigger("Run");

    }

    private void OnDestroy()
    {
        PlayerInputKeyDetection.OnPlayerJump -= OnPlayerJump;
        PlayerController.OnPlayerFall -= OnPlayerFall;
        PlayerCollisionDetection.OnPlayerGroundCollision -= OnPlayerGroundCollision;
    }


    void Start()
    {
        animator = GetComponent<Animator>();
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
