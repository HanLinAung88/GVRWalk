using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles the shooting of the bullets
public class CharacterShoot : MonoBehaviour {


    public GameObject bulletPrefab;

    public Transform bulletSpawn;

    AudioSource aScource;


	// Use this for initialization to initialize the sound source
	void Start () {
        aScource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame to check for input to shoot the bullet
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q) || (!Application.isEditor && Input.GetButtonDown("Fire3")))
        {
            Fire();
        }
	}

    /** 
     * This method is called when the player shoots the bullets
     */
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
