using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeActivate : MonoBehaviour {

    public float speed = 20f;
    private bool beenHit = false;

    public Transform platformSpawnPoint;
    public GameObject platformBlock;


	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}

    private void OnCollisionEnter(Collision target)
    {
        if (beenHit)
        {
            return;
        }

        /* First collision with a bullet - enable new platform */
        if (target.transform.gameObject.CompareTag("Bullet"))
        {
            beenHit = true;
            speed *= 5;
            Instantiate(platformBlock, platformSpawnPoint.position, platformSpawnPoint.rotation);
        }
    }
}
