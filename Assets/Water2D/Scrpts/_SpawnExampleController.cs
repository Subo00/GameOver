namespace Water2D {

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class _SpawnExampleController : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
		Water2D_Spawner.instance.Dynamic = false;
		
		//Water2D_Spawner.instance.LifeTime = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startWater()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Water2D_Spawner.instance.Dynamic = true;
                Water2D_Spawner.instance.Spawn();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                Water2D_Spawner.instance.Dynamic = false;
            }
        }
}
}
