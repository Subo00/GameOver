using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float mRotation = 0.0f;
    private float RotationSpeedLeft = 15f;
    private float RotationSpeedRight = -15f;
    public float FluidSpeed=5.0f;
    // Update is called once per frame
    void Update()
    {
        var hInput = Input.GetAxis("Horizontal");

        if (hInput == 1)
        {
            

            mRotation += hInput + RotationSpeedLeft;
            mRotation *= Time.deltaTime;
            transform.Rotate(0, 0, mRotation);

            var dir = transform.localRotation * Vector2.down;

            Water2D.Water2D_Spawner.instance.initSpeed = dir * FluidSpeed;
        }
        else if (hInput == -1)
        {


            mRotation += hInput + RotationSpeedRight;
            mRotation *= Time.deltaTime;
            transform.Rotate(0, 0, mRotation);

            var dir = transform.localRotation * Vector2.down;

            Water2D.Water2D_Spawner.instance.initSpeed = dir * FluidSpeed;
        }

    }
}
