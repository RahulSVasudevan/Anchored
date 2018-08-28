using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManScript : MonoBehaviour {

    Animator anim;

    float attackTimer = 0;
    float jumpTimer = 0;
    float direction = 0;

    public GameObject player;
    public GameObject knife;
    public GameObject fishMen;

    public bool isHooked = false;
    public bool isDown = false;
   

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        attackTimer -= Time.deltaTime;
        jumpTimer -= Time.deltaTime;



        if (isDown)
        {          
            anim.SetBool("IsDown", true);

            if (GameObject.Find("Anchor"))
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Anchor").GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.transform.GetComponent<Collider2D>());
        }
        else
            anim.SetBool("IsDown", false);
    }

    private void FixedUpdate()
    {
        float DistanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        
        if(!isHooked && !isDown)
        {
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

            anim.SetBool("IsAttacking", false);
            if ((attackTimer < 0) && (DistanceFromPlayer < 2f))
            {
                
                anim.SetBool("IsAttacking",true);

                if (attackTimer < -0.3f)
                { 
                    var projectile = Instantiate(knife, transform.position + new Vector3(direction *0.2f,0,0), Quaternion.identity);

                    Vector3 throwDirection = Vector3.Normalize(player.transform.position - transform.position);
                    projectile.GetComponent<Rigidbody2D>().velocity = throwDirection;

                    attackTimer = 2f;
                    anim.SetBool("IsAttacking", false);
                }
            }

            if (jumpTimer < 0)
            {
                if (DistanceFromPlayer > 2f && DistanceFromPlayer < 3f)
                {
                    transform.GetComponent<Rigidbody2D>().velocity = new Vector3(direction, Random.Range(2,4), 0);
                }

                if (DistanceFromPlayer < 1f)
                {
                    transform.GetComponent<Rigidbody2D>().velocity = new Vector3(-direction, Random.Range(2, 4), 0);
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
