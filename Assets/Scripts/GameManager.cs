using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager current;
    public bool isGameOver;
    public int lives;
    public int power;

    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }
    }

    public static bool IsGameOver()
    {
        if (current == null)
        {
            return false;
        }

        return current.isGameOver;
    }

    public static void PlayerDied()
    {
        if (current == null)
        {
            return;
        }
        current.Invoke("RestartScene", 0);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
