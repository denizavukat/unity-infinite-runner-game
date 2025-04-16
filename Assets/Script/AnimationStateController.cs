using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;

    private void OnEnable()
    {
        PlayerInputKeyDetection.OnPlayerJump += OnPlayerJump;
        PlayerCollisionDetection.OnPlayerGroundCollision += OnPlayerGroundCollision;

    }

    private void OnPlayerJump()
    {
       
            animator.SetBool("IsJumping", true);
        
    }

    private void OnPlayerGroundCollision()
    {

        animator.SetBool("IsJumping", false);

    }

    private void OnDestroy()
    {
        PlayerInputKeyDetection.OnPlayerJump -= OnPlayerJump;
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
