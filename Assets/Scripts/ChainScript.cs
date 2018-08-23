using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainScript : MonoBehaviour {

    LineRenderer line;

    GameObject Anchor;

	// Use this for initialization
	void Start () {
        
        Anchor = transform.GetChild(0).gameObject;

        line = GetComponent<LineRenderer>();
        line.SetPosition(0, new Vector3(0, 0, transform.position.z));
    }
	
	
	void Update () {
  

        
        line.SetPosition(1, Anchor.transform.localPosition);

    }
}
