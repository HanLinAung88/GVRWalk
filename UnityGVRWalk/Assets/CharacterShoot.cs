using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour {


    public GameObject bulletPrefab;

    public Transform bulletSpawn;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q))
        {
            Fire();
        }
	}

    void Fire()
    {
        Quaternion r = bulletSpawn.rotation;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, r) as GameObject;
        bullet.transform.Rotate(0, 0, 90); 
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        Destroy(bullet, 4.0f);
    }
}
