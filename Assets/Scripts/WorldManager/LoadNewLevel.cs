﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadNewLevel : MonoBehaviour
{
    public string SceneName;
    public GameObject FadeInPanel;
    public GameObject FadeOutPanel;
    public float FadeWait;

    private void Awake()
    {
        GameObject temp = gameObject;
        if (FadeInPanel != null)
        {
            GameObject Panel = Instantiate(FadeInPanel, Vector3.zero, Quaternion.identity);
            Destroy(Panel, 0.333f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (collision.tag == "Player")
        {
            StartCoroutine(FadeCo());
        }
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
