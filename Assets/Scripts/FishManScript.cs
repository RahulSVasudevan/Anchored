using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManScript : MonoBehaviour {

    float attackTimer = 0;
    float jumpTimer = 0;
    float direction = 0;

    public GameObject player;
    public GameObject knife;
    public GameObject fishMen;

    public bool isHooked = false;
   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        attackTimer -= Time.deltaTime;
        jumpTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float DistanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (transform.position.x - player.transform.position.x > 0)
        {
            direction = -1;
            Flip();
        }
        else           
        {
            direction = 1;
            Flip();
        }

        if(!isHooked)
        { 
            if ((attackTimer < 0) && (DistanceFromPlayer < 2f))
            {
                var projectile = Instantiate(knife, transform.position, Quaternion.identity);

                Vector3 throwDirection = Vector3.Normalize(player.transform.position - transform.position);
                projectile.GetComponent<Rigidbody2D>().velocity = throwDirection;

                attackTimer = 2f;
            }

            if (jumpTimer < 0)
            {
                if (DistanceFromPlayer > 2f && DistanceFromPlayer < 5f)
                {
                    transform.GetComponent<Rigidbody2D>().velocity = new Vector3(direction, 3, 0);
                }

                if (DistanceFromPlayer < 1f)
                {
                    transform.GetComponent<Rigidbody2D>().velocity = new Vector3(-direction,3,0);
                }
                jumpTimer = 2f;
            }
        }

    }


    void Flip()
    {
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        if(direction == -1)
            transform.GetComponent<SpriteRenderer>().flipX = false;
        else if(direction == 1)
            transform.GetComponent<SpriteRenderer>().flipX = true;

    }


}
