using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorScript : MonoBehaviour {

    LineRenderer line;

    GameObject Chain;
    GameObject HookedObject;

    public bool comeBack = false;
    bool hasHookedObject = false;

    // Use this for initialization
    void Start () {

        Chain = transform.parent.gameObject;
        line = Chain.GetComponent<LineRenderer>();
        line.SetPosition(0, Chain.transform.localPosition);
    }
	
	// Update is called once per frame
	void Update () {

        line.SetPosition(1, transform.localPosition);

        if (hasHookedObject == true && (Vector3.Distance(Chain.transform.localPosition, transform.localPosition) < 0.7f))
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);        

            Chain.transform.Rotate(new Vector3(0, 0, 400) * Time.deltaTime);

                
        }
       
    }

    private void FixedUpdate()
    {
        if ((Vector3.Distance(Chain.transform.localPosition, transform.localPosition) > 3f) || comeBack == true)
        {
            Vector3 throwVector = Vector3.Normalize(Chain.transform.position - transform.position);
            transform.GetComponent<Rigidbody2D>().velocity = throwVector * 2;

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

            HookedObject = collision.gameObject;
           
            transform.position = HookedObject.transform.position;
            transform.GetComponent<CircleCollider2D>().radius = 0.03f;

            HookedObject.GetComponent<Rigidbody2D>().isKinematic = true;
            HookedObject.transform.parent = transform;
            HookedObject.GetComponent<FishManScript>().isHooked = true;

            comeBack = true;
            hasHookedObject = true;
        }

        if ((collision.gameObject.tag == "Ground" || (collision.gameObject.tag == "Platform" && (collision.gameObject.transform.position.y + collision.offset.y) < Chain.transform.position.y)) 
            && hasHookedObject)
        {
            hasHookedObject = false;
            comeBack = true;

            HookedObject.GetComponent<FishManScript>().isHooked = false;
            HookedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            HookedObject.transform.parent = HookedObject.GetComponent<FishManScript>().fishMen.transform;
            HookedObject.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 4, 0);
            HookedObject.transform.rotation = Quaternion.identity;

            transform.GetComponent<CircleCollider2D>().radius = 0.14f;
        }
    }
}
