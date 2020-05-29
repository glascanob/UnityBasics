using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Variables
    //Game object components
    Animator anim;

    //Variables that control our enemy
    public int health = 1;
    bool dead = false;

    //Other
    Transform player;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (dead || player == null)
            return;

        if(player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void ReceiveDamage(int damageAmount)
    {
        if (dead)
            return;
        health -= damageAmount;

        if(health <= 0)
        {
            dead = true;
            anim.SetTrigger("Dead");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead)
            return;
        if(collision.CompareTag("Player"))
        {
            player = collision.transform;
            anim.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dead)
            return;
        if (collision.CompareTag("Player"))
        {
            player = null;
            anim.SetBool("Attack", false);
        }
    }
}
