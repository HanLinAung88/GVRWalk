using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    MeshRenderer rend;
         
	// Use this for initialization
	void Start () {
        rend = GetComponent<MeshRenderer>();

        Color thisColor = rend.material.color;
        thisColor.a = 0f;
        rend.material.color = thisColor;
       
        startFadeIn();
	}

    IEnumerator fadeIn()
    {
        for (float f = 0.05f; f <= 1f; f += 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.025f);
        }
    }

    private void startFadeIn()
    {
        StartCoroutine("fadeIn");
    }
}
