using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
   
    void Start()
    {
        FindObjectOfType<AudioManager>().Play(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
