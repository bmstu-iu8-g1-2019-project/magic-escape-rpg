using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeDungKill : MonoBehaviour
{
    [SerializeField] GameObject DamageZone;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DamageZone.SetActive(true);
            StartCoroutine(WaitCo());
        }
    }

    private IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(0.1f);
        DamageZone.SetActive(false);
    }
}
