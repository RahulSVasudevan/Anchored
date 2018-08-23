using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPlatformScript : MonoBehaviour {

    public GameObject player;
    public GameObject fishmen;

    public BoxCollider2D collider1;
    public BoxCollider2D collider2;
    public BoxCollider2D collider3;

    float height;

    GameObject[] characters;

    // Use this for initialization
    void Start () {

        characters = new GameObject[fishmen.transform.childCount + 1];

        characters[0] = player;

        for (int i = 0; i < fishmen.transform.childCount; i++)
        {
            characters[i+1] = fishmen.transform.GetChild(i).gameObject;
        }

    }
	
	// Update is called once per frame
	void FixedUpdate () {


        for(int i = 0;i< characters.Length; i++)
        { 
            height = characters[i].transform.position.y - 0.14f;
        

            if (Mathf.Abs(characters[i].transform.position.x - transform.position.x) < 2)
            {
                if (height < (transform.position.y + collider1.offset.y))
                {
                    Physics2D.IgnoreCollision(characters[i].GetComponent<Collider2D>(), collider1, true);
                }
                else
                {
                    Physics2D.IgnoreCollision(characters[i].GetComponent<Collider2D>(), collider1, false);
                }

                if (height < (transform.position.y + collider2.offset.y))
                {
                    Physics2D.IgnoreCollision(characters[i].GetComponent<Collider2D>(), collider2, true);
                }
                else
                {
                    Physics2D.IgnoreCollision(characters[i].GetComponent<Collider2D>(), collider2, false);
                }

                if (height < (transform.position.y + collider3.offset.y))
                {
                    Physics2D.IgnoreCollision(characters[i].GetComponent<Collider2D>(), collider3, true);
                }
                else
                {
                    Physics2D.IgnoreCollision(characters[i].GetComponent<Collider2D>(), collider3, false);
                }
            }
        }
    }
}
