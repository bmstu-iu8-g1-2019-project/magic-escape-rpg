using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBossRoom : LoadNewLevel
{
    [SerializeField] List<string> bossesScenes;
    [SerializeField] List<int> recommendedLevel;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (bossesScenes.Count != recommendedLevel.Count)
        {
            Debug.LogError("Wrong number of bosses rooms or necessary levels. Check it!");
            return;
        }
        if (collision.tag == "Player")
        {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            if (player.bossesProgres < bossesScenes.Count)
            {
                if (player.Level >= recommendedLevel[player.bossesProgres])
                {
                    LoadLevel(bossesScenes[player.bossesProgres]);
                }
            }
        }
    }
}
