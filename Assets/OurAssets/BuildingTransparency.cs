using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTransparency : MonoBehaviour
{
    private bool done;
    private SpriteRenderer spriteRenderer;
    private float opacity = 0.0f;
    private Transform buildingSize;
    //TODO: kada voda ide preko elementa transparency se postepeno povecava od 0 do maximuma dok voda prolazi kroz element
    private void Start()
    {
        done = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        buildingSize = gameObject.GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D target)
	{
        if (target.tag == "Metaball_liquid" && opacity < 1f)
        {
            target.gameObject.SetActive(false);
            opacity += 0.01f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
        }
    }

    //ne radi animacija da se poveca i smanji kad se napuni
    private void Update()
    {
        if (done == false && opacity > 1.0f)
        {
            float x = 1.0f;
            for (int i = 0; i < 5; i++)
            {
                x += .1f;
                ExampleCoroutine();
                buildingSize.localScale = new Vector3(x, x, x);
            }

            for (int i = 0; i < 5; i++)
            {
                x -= .1f;
                StartCoroutine(ExampleCoroutine());
                buildingSize.localScale = new Vector3(x, x, x);
            }
            done = true;
        }
    }
    private IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1);
    }
}
