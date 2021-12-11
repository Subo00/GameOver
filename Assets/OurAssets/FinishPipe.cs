using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool done;
    private int filled;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        done = false;
        filled = 0;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Metaball_liquid")
        {
            target.gameObject.SetActive(false);
            filled += 1;
            if (filled >= 100) done = true;
        }
    }
}
