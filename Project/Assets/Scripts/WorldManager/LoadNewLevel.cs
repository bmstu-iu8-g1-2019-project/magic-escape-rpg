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
    private SaveLoadActions sys;

    private void Awake()
    {
        sys = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveLoadActions>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (collision.tag == "Player")
        {
            LoadLevel(SceneName);
        }
    }

    public void LoadLevel(string str)
    {
        if (!sys)
        {
            sys = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveLoadActions>();
        }
        sys.SavePlayer();
        StartCoroutine(FadeCo(str));
    }
    
    public IEnumerator FadeCo(string str)
    {
        if (FadeOutPanel != null)
        {
            Instantiate(FadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(FadeWait);
        AsyncOperation Operation = SceneManager.LoadSceneAsync(str);
        while (!Operation.isDone)
        {
            yield return null;
        }
    } 
}
