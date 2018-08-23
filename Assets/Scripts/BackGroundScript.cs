using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScript : MonoBehaviour {

    public GameObject player;
    public GameObject bg1;
    public GameObject bg2;

    float offsetX,offsetY;

   
    void Start () {
        bg1.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        offsetX = player.transform.position.x;
        offsetY = player.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {

        bg1.transform.position = new Vector3((0.5f*(player.transform.position.x-offsetX))+offsetX, (0.5f * (player.transform.position.y - offsetY)) + offsetY);
        bg2.transform.position = player.transform.position + new Vector3(0.8f, 0.67f, 1);
    }
}
