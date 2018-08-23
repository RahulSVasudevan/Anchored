﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    
    private Animator anim;
    private Rigidbody2D rb2d;

    float h;
    public float maxSpeed = 2f;
    float JumpTimer = 1f;
    float BubbleTimer = 4f;
    //float rotZ = 0;

    bool releaseAnchor = false;
    public bool anchorReleased = false;
    public bool facingRight = true;

    GameObject AnchorChain;
    GameObject Anchor;
    public GameObject Bubble;

    private IEnumerator coroutine;

    public AudioSource BubbleSound1;
    public AudioSource BubbleSound2;
    public AudioSource BubbleSound3;
    public AudioSource BubbleSound4;
    public AudioSource BubbleSound5;
    public AudioSource BubbleSound6;

    void Start () {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        AnchorChain = transform.Find("AnchorChain").gameObject;
        Anchor = AnchorChain.transform.Find("Anchor").gameObject;
       
    }


    private void FixedUpdate()
    {
        
    }

    void Update () {

// Navigation

        h = Input.GetAxis("Horizontal");

        //rb2d.AddForce(Vector2.right * h *5);
        rb2d.velocity = new Vector2(h * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        anim.SetFloat("Speed", Mathf.Abs(h));

        JumpTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && JumpTimer < 0f)
        {
            rb2d.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            JumpTimer = 1f;
        }



// Swinging Anchor

        //AnchorChain.SetActive(false);

        if (Input.GetKey("x") && anchorReleased == false && Anchor.GetComponent<AnchorScript>().comeBack == false)
        {
            AnchorChain.SetActive(true);
            Anchor.GetComponent<Rigidbody2D>().isKinematic = true;

            //rotZ += Time.deltaTime;
            AnchorChain.transform.Rotate(new Vector3(0, 0, 200) * Time.deltaTime);

            releaseAnchor = true;
        }
        else if (releaseAnchor)
        {
            anchorReleased = true;

            Anchor.GetComponent<Rigidbody2D>().isKinematic = false;

            Vector3 throwVector = Vector3.Normalize(Anchor.transform.position - transform.position);
            Anchor.transform.GetComponent<Rigidbody2D>().velocity = throwVector * 2; 

            releaseAnchor = false;
        }

        // Bubbles

        BubbleTimer -= Time.deltaTime;
        if (BubbleTimer < 0f)
        {
            coroutine = BubbleGenerate(1f,BubbleSound1);
            StartCoroutine(coroutine);
            coroutine = BubbleGenerate(1.1f, BubbleSound2);
            StartCoroutine(coroutine);
            coroutine = BubbleGenerate(1.2f, BubbleSound3);
            StartCoroutine(coroutine);
            coroutine = BubbleGenerate(1.3f, BubbleSound4);
            StartCoroutine(coroutine);
            coroutine = BubbleGenerate(1.4f, BubbleSound5);
            StartCoroutine(coroutine);
            coroutine = BubbleGenerate(1.5f, BubbleSound6);
            StartCoroutine(coroutine);

            BubbleTimer = 4f;
        }

    }

    IEnumerator BubbleGenerate(float delay, AudioSource AudSrc)
    {
        yield return new WaitForSeconds(delay);
        AudSrc.pitch = Random.Range(0.7f,1.7f);
        AudSrc.Play();
        Instantiate(Bubble, transform.position + new Vector3(Random.Range(-0.15f, 0.1f), 0,0), Quaternion.identity);
    }

    void Flip()
    {
        facingRight = !facingRight;
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        transform.GetComponent<SpriteRenderer>().flipX = !transform.GetComponent<SpriteRenderer>().flipX;
    }
}