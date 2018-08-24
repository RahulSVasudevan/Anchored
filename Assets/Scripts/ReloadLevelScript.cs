using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevelScript : MonoBehaviour {

    //GameObject LevelGenerator;

    public int EnemyGroupSize = 2;

    // Use this for initialization
    void Awake () {

        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

    }


    void Start()
    {
        //LevelGenerator = GameObject.Find("LevelGenerator");
        
        //EnemyGroupSize = LevelGenerator.GetComponent<LevelGenerator>().EnemyGroupSize;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyGroupSize += 1;

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
