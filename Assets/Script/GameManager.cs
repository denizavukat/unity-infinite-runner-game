using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public delegate void OnUIUpdateScoreTextDelegate(int score);
    public static event OnUIUpdateScoreTextDelegate OnUIUpdateScoreText;

    public delegate void OnUIGameOverDelegate();
    public static event OnUIGameOverDelegate OnUIGameOver;

    

    private int score;

    public bool isGameActive = true;
    

    public static GameManager instance;

    private void OnEnable()
    {
        PlayerController.OnPlayerHitObstacle += OnPlayerHitObstacle;
        PlayerCollisionDetection.OnPlayerPointCollision += OnPlayerPointCollision;
    }

    private void OnPlayerHitObstacle()
    {
        GameOver();
    }

    private void OnPlayerPointCollision(Collider other)
    {
        UpdateScore();
        other.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerHitObstacle -= OnPlayerHitObstacle;
        PlayerCollisionDetection.OnPlayerPointCollision -= OnPlayerPointCollision;
    }



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

    void Start()
    {

        score = 0;
        OnUIUpdateScoreText?.Invoke(score);
    }

    void Update()
    {
        
    }

    private void GameOver()
    {
        isGameActive = false;
        OnUIGameOver?.Invoke();
    }

    

    private void UpdateScore()
    {
        score++;
        OnUIUpdateScoreText?.Invoke(score);
       
      
    }

   

}
