using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    /* This Unity function let us track when ever another collider2D enters
     * into a TRIGGER collider attached to this GameObject.
     * IMPORTANT: For this to work at least one of the gameobjects needs to
     * have a RigidBody, otherwise it won't track it.
     */ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Here assign the checkpoint
            gameManager.RestartPlayer();
        }
    }
}
