using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPipe : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Metaball_liquid")
        {
            target.gameObject.SetActive(false);
        }
    }
}
