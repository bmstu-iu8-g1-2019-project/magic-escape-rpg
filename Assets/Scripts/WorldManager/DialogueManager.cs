using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueBox;
    public bool DialogueActive;
    public Text DialogueText;
    public bool GamePaused;
    private void Awake()
    {
        DialogueActive = true;
        DialogueBox.SetActive(DialogueActive);
        DialogueText.text = "Press W/A/S/D to move and 'Space' to attack. Press 'E' to close this message";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogueActive = false;
            DialogueBox.SetActive(DialogueActive);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                GameAwake();
            } else {
                GamePause();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!DialogueActive)
            {
                DialogueActive = true;
                DialogueBox.SetActive(DialogueActive);
                GamePause();
            }
            DialogueText.text = "Try not to suck";
            GamePause();
        }
    }

    public void GamePause()
    {
        if (!GamePaused)
        {
            Debug.Log("+");
            Time.timeScale = 0f;
            GamePaused = true;
        }
    }

    public void GameAwake()
    {
        if (GamePaused)
        {
            Time.timeScale = 1f;
            GamePaused = false;
        }
    }

}
