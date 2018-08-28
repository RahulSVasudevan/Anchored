using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Camera MainCamera;

    public GameObject Player;
    public GameObject Bubble;

    float BubbleTimer = 0f;


	// Use this for initialization
	void Start () {

        MainCamera = Camera.main;

    }
	
	// Update is called once per frame
	void Update () {
        if (Player.transform.position.x > 2 && Player.transform.position.x < 30)
        {
            transform.position = Player.transform.position + new Vector3(0.8f, 0.67f, -1);

            transform.position = new Vector3(transform.position.x, (transform.position.y + 2.5f) / 2, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, ((Player.transform.position.y + 0.67f) + 2.5f) / 2, transform.position.z);
        }
        //ShakeTimer += Time.deltaTime * 50;

// Bubbles
        BubbleTimer -= Time.deltaTime;
        if(BubbleTimer < 0)
        {
            Instantiate(Bubble, 
                        transform.position + 
                            new Vector3(Random.Range(-MainCamera.orthographicSize, MainCamera.orthographicSize) * MainCamera.aspect,
                            -MainCamera.orthographicSize, 1), 
                        Quaternion.identity);
            BubbleTimer = 1f;
        }

       
	}

   

}
