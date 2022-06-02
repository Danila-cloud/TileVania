using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    int score = 0;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
    }

    void Update()
    {
        livesText.text = PlayerLives.ToString();
        scoreText.text = score.ToString();
    }
    public void DEATH()
    {
        if(PlayerLives > 1)
        {
            TakeLive();
        }
        else
        {
            ResetSession();
        }
    }
    void TakeLive()
    {
        PlayerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = PlayerLives.ToString();
    }
    void ResetSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
