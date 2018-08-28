using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorScript : MonoBehaviour {

    LineRenderer line;

    GameObject Chain;
    GameObject HookedObject;
    AudioSource ChainSound;
    //GameObject HookedObjectOriginalParent;

    public bool comeBack = false;
    bool hasHookedObject = false;
    bool releaseEnemy = false;

    float hookedTimer = 2f;

    // Use this for initialization
    void Start () {

        Chain = transform.parent.gameObject;
        //ChainSound = GameObject.Find("ChainSound").GetComponent<AudioSource>();

        line = Chain.GetComponent<LineRenderer>();
        line.SetPosition(0, Chain.transform.localPosition);
    }
	
	// Update is called once per frame
	void Update () {

        line.SetPosition(1, transform.localPosition);

        if (hasHookedObject == true && (Vector3.Distance(Chain.transform.localPosition, transform.localPosition) < 0.6f) 
            && HookedObject.tag == "Enemy")
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);        

            Chain.transform.Rotate(new Vector3(0, 0, 600) * Time.deltaTime);

            hookedTimer -= Time.deltaTime;
            if (hookedTimer < 0)
            {
                hasHookedObject = false;
                comeBack = true;

                HookedObject.GetComponent<FishManScript>().isHooked = false;
                HookedObject.GetComponent<FishManScript>().isDown = true;
                HookedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                HookedObject.transform.parent = HookedObject.GetComponent<FishManScript>().fishMen.transform;
                HookedObject.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 4, 0);
                HookedObject.transform.rotation = Quaternion.identity;

                HookedObject.GetComponent<AudioSource>().pitch = Random.Range(0.6f, 1);
                HookedObject.GetComponent<AudioSource>().Play();

                transform.GetComponent<CircleCollider2D>().radius = 0.14f;
                hookedTimer = 2f;
            }
        }  
            
        
    }

    private void FixedUpdate()
    {
        if ((Vector3.Distance(Chain.transform.localPosition, transform.localPosition) > 3f) || comeBack == true)
        {
            Vector3 throwVector = Vector3.Normalize(Chain.transform.position - transform.position);
            transform.GetComponent<Rigidbody2D>().velocity = throwVector * 3;

            comeBack = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Chain.SetActive(false);
            transform.localPosition = new Vector3(0.45f,0,0);
            collision.gameObject.GetComponent<PlayerScript>().anchorReleased = false;
        }

        if (collision.gameObject.tag == "Enemy" &&  (Vector3.Distance(collision.gameObject.transform.position, Chain.transform.position) > 0.3f))
        {
            //ChainSound.Play();

            HookedObject = collision.gameObject;
            //HookedObjectOriginalParent = collision.gameObject.transform.parent.gameObject;

            transform.position = HookedObject.transform.position;
            transform.GetComponent<CircleCollider2D>().radius = 0.03f;

            HookedObject.GetComponent<Rigidbody2D>().isKinematic = true;
            HookedObject.transform.parent = transform;
            HookedObject.GetComponent<FishManScript>().isHooked = true;

            comeBack = true;
            hasHookedObject = true;
        }

        if ((collision.gameObject.tag == "Ground" || (collision.gameObject.tag == "Platform" && (collision.gameObject.transform.position.y + collision.offset.y) < Chain.transform.position.y)) 
            && hasHookedObject && HookedObject.tag == "Enemy")
        {
            hasHookedObject = false;
            comeBack = true;

            HookedObject.GetComponent<FishManScript>().isHooked = false;
            HookedObject.GetComponent<FishManScript>().isDown = true;
            HookedObject.GetComponent<Rigidbody2D>().isKinematic = false;           
            HookedObject.transform.parent = HookedObject.GetComponent<FishManScript>().fishMen.transform;
            HookedObject.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 4, 0);
            HookedObject.transform.rotation = Quaternion.identity;

            HookedObject.GetComponent<AudioSource>().pitch = Random.Range(0.6f, 1);
            HookedObject.GetComponent<AudioSource>().Play();

            transform.GetComponent<CircleCollider2D>().radius = 0.14f;
        }

        if (collision.gameObject.tag == "EnemyProjectile")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = -collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            collision.gameObject.GetComponent<FishKnifeScript>().isReflected = true;
        }
    }
}
