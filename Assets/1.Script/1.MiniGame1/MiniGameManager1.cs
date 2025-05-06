using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager1 : MonoBehaviour
{
    static MiniGameManager1 gameManager;

    public static MiniGameManager1 Instance
    {
        get { return gameManager; }
    }

    private int currentScore = 0;
    public bool isGameOver;

    private void Awake()
    {
        gameManager = this;
    }

    void Start()
    {
        currentScore = 0;
        isGameOver = false;
        StartCoroutine(scoreAdd());
    }

    public void GameOver()
    {
        isGameOver = true;
        GetComponent<UIManager>().InitUi(this.currentScore);
        Debug.Log("Game Over");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoBackToMain()
    {
        SceneManager.LoadScene("Main");
    }

    IEnumerator scoreAdd()
    {
        while (isGameOver == false)
        {
            yield return new WaitForSeconds(1);
            AddScore(1);
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;

        Debug.Log("Score: " + currentScore);
    }

}
