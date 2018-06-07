using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {
    
    public float speedMoveY;
    public float speedMoveX;
    public float speedMoveZ;

    public float minPosY;
    public float maxPosY;

    public float minPosX;
    public float maxPosX;

    public float minPosZ;
    public float maxPosZ;

    private float delta = 0.2F;


    private bool directionChange(float pos, float min_pos, float max_pos)
    {
        return pos < (min_pos + delta) ||
               pos > (max_pos - delta);
    }
	/* Checks */
	void Update () {
        bool change_direction_y = directionChange(transform.position.y, minPosY, maxPosY);
        if (change_direction_y)
        {
            speedMoveY *= -1;
        }

        bool change_direction_x = directionChange(transform.position.x, minPosX, maxPosX);
        if (change_direction_x)
        {
            speedMoveX *= -1;
        }

        bool change_direction_z = directionChange(transform.position.z, minPosZ, maxPosZ);
        if (change_direction_z)
        {
            speedMoveZ *= -1;
        }

        transform.Translate(speedMoveX * Time.deltaTime, speedMoveY * Time.deltaTime, speedMoveZ * Time.deltaTime);
	}
}
