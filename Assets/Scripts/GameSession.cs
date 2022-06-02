using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;

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
    }
    void ResetSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
