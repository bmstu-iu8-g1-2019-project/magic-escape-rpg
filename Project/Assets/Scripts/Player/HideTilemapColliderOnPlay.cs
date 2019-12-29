using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideTilemapColliderOnPlay : MonoBehaviour
{

    private TilemapRenderer TilemapRenderer;

    void Start()
    {
        TilemapRenderer = GetComponent<TilemapRenderer>();
        TilemapRenderer.enabled = false;
    }
}
