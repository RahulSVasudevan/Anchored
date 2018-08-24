using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishKnifeScript : MonoBehaviour {

    //public Vector3 destination;

    //Vector3 direction;

    public bool isReflected = false;

	void Start () {

        //destination = new Vector3(0,0,0);
        //direction = Vector3.Normalize(destination - transform.position) * 0.02f;
        StartCoroutine("DestroyKnife");
    }

   

    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(0, 0, 200) * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy" && isReflected)
        {
            collision.gameObject.GetComponent<FishManScript>().isDown = true;
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyKnife()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
