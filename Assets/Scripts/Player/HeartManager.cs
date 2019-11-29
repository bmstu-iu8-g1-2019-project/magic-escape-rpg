using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private Image[] Hearts;
    [SerializeField] private Sprite FullHeart;
    [SerializeField] private Sprite HalfFullHeart;
    [SerializeField] private Sprite EmptyHeart;
    public FloatValue HeartContainers;
    [SerializeField] private FloatValue PlayerCurrentHealth;
    private bool isInitialized;

    void Start()
    {
        InitHearts();
        UpdateHearts();
        isInitialized = true;
    }

    public void InitHearts()
    {
        HeartContainers.InitialValue = PlayerCurrentHealth.InitialValue / 2; 
        for (int i = 0; i < HeartContainers.InitialValue; i++)
        {
            Hearts[i].gameObject.SetActive(true);
            if (!isInitialized)
            {
                Hearts[i].sprite = FullHeart;
            }
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

    public void IncHeartAmmount(int num)
    {
        HeartContainers.InitialValue += num;
        InitHearts();
    }
}
