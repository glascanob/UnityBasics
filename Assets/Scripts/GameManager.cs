using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Checkpoint currentCheckpoint;
    [HideInInspector]
    public PlayerController player;

    public GameObject pauseButton;
    public GameObject pauseMenu;

    [HideInInspector]
    public int points = 0;

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
    private void Start()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void PauseGame(bool pause)
    {
        pauseButton.SetActive(!pause);
        pauseMenu.SetActive(pause);
        /*
         * Alternative way of doing the same as the following script line
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        */
        Time.timeScale = pause ? 0 : 1;
        player.gamePaused = pause;
    }

    public void AddPoints()
    {
        points ++;
    }

    public void RestartGame()
    {
        PauseGame(false);
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
    public void RestartPlayer()
    {
        player.dead = true;
        player.transform.position = new Vector3(currentCheckpoint.transform.position.x, currentCheckpoint.transform.position.y + 1f, 0);
        //wait maybe a couple of frames before letting it revive
        player.dead = false;
    }
}
