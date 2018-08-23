using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("DestroyBubble");
    }
	

	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
    }

    IEnumerator DestroyBubble()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
