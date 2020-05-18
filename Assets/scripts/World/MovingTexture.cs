using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTexture : MonoBehaviour
{
    // Scroll main texture based on time
    public float scrollSpeed = 1f;
    private int _zeroVal = 0;
    private Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, _zeroVal));
    }
}
