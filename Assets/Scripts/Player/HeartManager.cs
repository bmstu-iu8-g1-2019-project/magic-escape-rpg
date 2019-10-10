using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] Hearts;
    public Sprite FullHeart;
    public Sprite HalfFullHeart;
    public Sprite EmptyHeart;
    public FloatValue HeartContainers;
    public FloatValue PlayerCurrentHealth;

    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < HeartContainers.InitialValue; i++)
        {
            Hearts[i].gameObject.SetActive(true);
            Hearts[i].sprite = FullHeart;
        }
    }

    public void UpdateHearts()
    {
        float Health = PlayerCurrentHealth.RuntimeValue / 2;
        for (int i = 0; i < HeartContainers.RuntimeValue; i++)
        {
            if (i <= Health - 1f)
            {
                Hearts[i].sprite = FullHeart;
            } else if (i >= Health){
                Hearts[i].sprite = EmptyHeart;
            } else {
                Hearts[i].sprite = HalfFullHeart;
            }
        }
    }
}
