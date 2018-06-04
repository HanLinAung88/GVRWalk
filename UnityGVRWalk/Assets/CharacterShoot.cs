using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour {


    public GameObject bulletPrefab;

    public Transform bulletSpawn;

    AudioSource aScource;


	// Use this for initialization
	void Start () {
        aScource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q) || (!Application.isEditor && Input.GetButtonDown("Fire3")))
        {
            Fire();
        }
	}

    void Fire()
    {
        aScource.Play();
        Quaternion r = bulletSpawn.rotation;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, r) as GameObject;
        bullet.transform.Rotate(0, 0, 90); 
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        Destroy(bullet, 4.0f);
    }
}
