using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Checkpoint currentCheckpoint;
    public PlayerController player;

    private void Start()
    {
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
