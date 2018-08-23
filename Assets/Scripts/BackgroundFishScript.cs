using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFishScript : MonoBehaviour {

    int direction = 0;

	// Use this for initialization
	void Start () {
        if (Random.value > 0.5)
            direction = 1;
        else
        {
            direction = -1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
            
	}
	
	
	void FixedUpdate () {
        transform.position += new Vector3(direction * Time.deltaTime *0.5f,0,0);

        if(transform.position.x >35)
            transform.position = new Vector3(0.1f, transform.position.y, transform.position.z);

        if (transform.position.x < 0)
            transform.position = new Vector3(34.9f, transform.position.y, transform.position.z);
    }
}
