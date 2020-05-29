using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Checkpoint currentCheckpoint;
    public PlayerController player;

    /* This is not the most efficent nor optimized way to use a GameManager
     * The FindObjectOfType is a powerfull but terrible for performance
     * function that allows you to go through all the gameobjects in the
     * hierarchy to find that component.
     */
    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerController>();
    }

    public void RestartPlayer()
    {
        player.dead = true;
        player.transform.position = new Vector3(currentCheckpoint.transform.position.x, currentCheckpoint.transform.position.y + 1f, 0);
        //wait maybe a couple of frames before letting it revive
        player.dead = false;
    }
}
