using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components of the PlayerController Class
    Rigidbody2D rb;
    Animator anim;

    //Variables that control our player
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce;

    //Other Variables
    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Vector2 groundCheckSize;

    [SerializeField]
    LayerMask ground;
    
    bool grounded;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }
    // Very usefull when talking about physics
    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, ground);
    }
    // Update is called once per frame
    void Update()
    {
        if(dead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        UpdateControls();
        UpdateAnimator();
        
        if(rb.velocity.x >0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void UpdateAnimator()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("VSpeed", rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void UpdateControls()
    {
        
        if(Input.GetAxis("Jump") > 0 && grounded) //Check if the player is on the ground
        {
            Jump();
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        horizontalInput = horizontalInput * speed;

        rb.velocity = new Vector2(horizontalInput, rb.velocity.y);

        if(Input.GetAxis("Fire1") > 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetTrigger("Jump");
    }
}
