using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    //Components of the PlayerController Class
    Rigidbody2D rb;
    Animator anim;

    //Variables that control our player
    [Header("Player Settings")]
    [SerializeField] // We use the SerializedField mainly to assing on inspector
    float speed;
    [SerializeField]
    float jumpForce;

    //Variables that help us recognize if the player is touching the ground
    [Header("Ground Variables")]
    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Vector2 groundCheckSize;

    [SerializeField]
    LayerMask ground;
    
    /*Player state variables.
     * This variables change depending on the player reaction to the level.
     * They help us to understand what is happening to the player to apply
     * animations, physics and other activities of the game flow.
         */
    bool grounded;
    [HideInInspector] //We don't need this variable to be shown on the inspector
    public bool dead = false;
    #endregion

    /* Start is called before the first frame update so here we can
     * initialize our variables such as the rigidbody and the animator
         */
    void Start()
    {
        /*The GetComponent is a great tool Unity has to get access to one of the
         * components a game object has. It can be used directly and by default
         * it will try and get the component from the gameobject where the
         * current script is added, or you can start with the name of the
         * gameobject and then .GetComponent to get a component on another
         * gameobject.
         * Also you can use GetComponentInChildren or InParent to get components
         * from children or parent gameobjects.
         */
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    /*We are using this Unity function to draw a gizmo that help us configure
     * the OverlapBox we create on the FixedUpdate
         */ 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }
    /* Very usefull when talking about physics, that's why we use it to track if
     * the player is touching the ground layer.
         */
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
    /*We created this function to update the animator variables with the
     * information from the player activity
     */ 
    void UpdateAnimator()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("VSpeed", rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
    /*Here we listen to control imputs from the user and run a function or
     * code acordingly.
     */ 
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
