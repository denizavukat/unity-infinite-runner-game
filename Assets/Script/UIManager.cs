using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private int score;

    public delegate void OnUIRestartDelegate();
    public static event OnUIRestartDelegate OnUIRestart;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        GameManager.OnUIUpdateScoreText += OnUIUpdateScoreText;
        GameManager.OnUIGameOver += OnUIGameOver;
    }
    private void OnDisable()
    {
        GameManager.OnUIUpdateScoreText -= OnUIUpdateScoreText;
        GameManager.OnUIGameOver -= OnUIGameOver;
    }


    private void OnUIUpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
    private void OnUIGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
   

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
