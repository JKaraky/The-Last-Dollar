using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdBannerRandomizer : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
