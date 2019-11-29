using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadNewLevel : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject FadeInPanel;
    [SerializeField] private GameObject FadeOutPanel;
    [SerializeField] private float FadeWait;
    [SerializeField] private bool isSaving;
    private SaveLoadActions sys;

    private void Awake()
    {
        sys = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveLoadActions>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (collision.tag == "Player")
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        if (!sys)
        {
            sys = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveLoadActions>();
        }
        sys.SavePlayer();
        StartCoroutine(FadeCo());
    }
    
    public IEnumerator FadeCo()
    {
        if (FadeOutPanel != null)
        {
            Instantiate(FadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(FadeWait);
        AsyncOperation Operation = SceneManager.LoadSceneAsync(SceneName);
        while (!Operation.isDone)
        {
            yield return null;
        }
    } 
}
