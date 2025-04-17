using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{


    public delegate void OnPlayerHitObstacleDelegate();
    public static event OnPlayerHitObstacleDelegate OnPlayerHitObstacle;

    public delegate void OnPlayerFallDelegate();
    public static event OnPlayerFallDelegate OnPlayerFall;


    public delegate void ResetPositionsDelegate();
    public static event ResetPositionsDelegate OnResetPositions;

    public int maxLineCount = 3;
    public float lineWidth = 3f;
    public float startingLineXPosition = -3;
    public float lineChangeSpeed = 20f;
    private int currentLine = 1;

    public float forwardSpeed = 20f;
    private Vector3 forwardVector = new Vector3(0, 0, 1);


    public float jumpForce = 10f;
    public float jumpheight = 20.0f;
    public float maxHeight = 30.0f;
    public float verticalVelocity = 0f;

    Vector3 crushDirectionVector = new Vector3(0, 0, 1);

    private int jumpCount = 0;
    public int maxJumpCount = 2;

    public float gravity = -15f;

    public bool isOnGround;
    public bool isJumping;

    public float resetDistance = 100.0f;

    public Vector3 initialPlayerPositions;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        PlayerInputKeyDetection.OnPlayerJump += OnPlayerJump;
        PlayerInputKeyDetection.OnPlayerGoRight += OnPlayerGoRight;
        PlayerInputKeyDetection.OnPlayerGoLeft += OnPlayerGoLeft;

        PlayerCollisionDetection.OnPlayerGroundCollision += OnPlayerGroundCollision;
        PlayerCollisionDetection.OnPlayerObstacleCollision += OnPlayerObstacleCollision;
        
    }

    private void OnPlayerGroundCollision()
    {
        
        isOnGround = true;
        isJumping = false;
        jumpCount = 0;
        verticalVelocity = 0f;
    }

   
    
    private void OnPlayerObstacleCollision(Collider other)
    {
        if (IsDead(other))
        {
            OnPlayerHitObstacle?.Invoke();
            Debug.Log("Game OVER");
            return;
        }
    }


    private void OnPlayerJump()
    {
        if(jumpCount < maxJumpCount)
        {
            jumpCount++;
            isJumping = true;
            isOnGround = false;
            verticalVelocity = jumpForce;
           
        }
    }

    private void OnPlayerGoRight()
    {
        if(currentLine < maxLineCount - 1)
        {
            currentLine++;
        }
        
    }
    private void OnPlayerGoLeft()
    {
        if (currentLine >0)
        {
            currentLine--;
        }

    }

    private void OnDisable()
    {
        PlayerInputKeyDetection.OnPlayerJump -= OnPlayerJump;
        PlayerInputKeyDetection.OnPlayerGoRight -= OnPlayerGoRight;
        PlayerInputKeyDetection.OnPlayerGoLeft -= OnPlayerGoLeft;

        PlayerCollisionDetection.OnPlayerGroundCollision -= OnPlayerGroundCollision;
        PlayerCollisionDetection.OnPlayerObstacleCollision -= OnPlayerObstacleCollision;

    }

    // Start is called before the first frame update
    void Start()
    {
   
        initialPlayerPositions = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameActive) return;
       
        transform.position += forwardVector * forwardSpeed * Time.deltaTime;
            MoveSideways();

            if (!isOnGround)
            {
                verticalVelocity += gravity * Time.deltaTime;
                transform.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);
                if(verticalVelocity<= 0)
                {
                    OnPlayerFall?.Invoke();
                }
                if (transform.position.y <= initialPlayerPositions.y)
                {
                    transform.position = new Vector3(transform.position.x, initialPlayerPositions.y, transform.position.z);
                    
                }
            }




           
    }

    private void MoveSideways()
    {

        Vector3 desiredPosition = new Vector3(startingLineXPosition + currentLine * lineWidth, transform.position.y, transform.position.z);

        Vector3 currentPosition = transform.position;
        Vector3 differenceVector = desiredPosition - currentPosition;


        if (differenceVector.magnitude < 0.01)
        {
            transform.position = desiredPosition;
            return;
        }
        Vector3 directionVector = differenceVector.normalized;
        transform.position += directionVector * lineChangeSpeed * Time.deltaTime;

    }
    
    private bool IsDead(Collider other)
    {
        Vector3 otherObjectPosition = other.transform.position;
        otherObjectPosition.y = initialPlayerPositions.y;
        Vector3 inBetweenDirectionVector = (transform.position - otherObjectPosition).normalized;

        float dotProduct = Vector3.Dot(inBetweenDirectionVector, crushDirectionVector);
   
        return dotProduct < -0.65f;
    }



    


    
       


    
      
}
