using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPipe : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject victoryDisplay;
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
        if (target.tag == "Metaball_liquid" && filled < 100)
        {
            target.gameObject.SetActive(false);
            if (filled == 0)
                FindObjectOfType<AudioManager>().Play(1);

            filled += 1;
            if (filled >= 100)
            {
                done = true;
                if (filled == 100)
                {
                    FindObjectOfType<AudioManager>().Play(2);
                    filled += 1;
                    victoryDisplay.SetActive(true);
                }

            }
        }
    }
}
