using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startFillingFinishPipe : MonoBehaviour
{
    //Collider m_ObjectCollider;
    public Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        //m_ObjectCollider = GetComponent<Collider>();
        // m_ObjectCollider.isTrigger = true;
        coll.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        int countObjects = 0;
        int countFilled = 0;
        GameObject[] coloredBuildings;
        coloredBuildings = GameObject.FindGameObjectsWithTag("Building");

        foreach (GameObject coloredBuilding in coloredBuildings)
        {
            countObjects += 1;
            if (coloredBuilding.GetComponent<BuildingTransparency>().done == true)
            {
                //m_ObjectCollider.isTrigger = true;
                countFilled += 1;
            }
        }
        if (countFilled == countObjects)
        {
            coll.isTrigger = true;
        }
    }
}
 