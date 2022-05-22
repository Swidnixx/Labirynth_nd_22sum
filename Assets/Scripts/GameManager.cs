using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KeyColor
{
    Red,
    Green,
    Gold
}
public class GameManager : MonoBehaviour
{
    //Sound 
    public AudioSource audioSource;
    public AudioClip pauseClip;
    public AudioClip loseClip;
    public AudioClip winClip;

    //Singleton
    public static GameManager instance;

    [SerializeField] int secondsLeft = 100;
    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;

    private bool gamePaused = false;

    private int points = 0;

    #region Unity Callbacks
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple GameManagers in the Scene");
        }
    }

    void Start()
    {
        InvokeRepeating(nameof(TimerTick), 3, 1);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        PauseCheck();
    }
    #endregion

    #region Game Flow Methods
    void PauseCheck()
    {
        //Input.GetKeyDown(KeyCode.Escape);
        if(Input.GetButtonDown("Cancel"))
        {
            PlayClip(pauseClip);

            if(gamePaused)
            {
                Debug.Log("Game Resumed");
                gamePaused = false;
                Time.timeScale = 1;
            }
            else
            {
                Debug.Log("Game Paused");
                gamePaused = true;
                Time.timeScale = 0;
            }
        }
    }

    IEnumerator Stopper()
    {
        while(secondsLeft >= 0)
        {
            TimerTick();
            yield return new WaitForSeconds(1);
        }
    }
    void TimerTick()
    {
        secondsLeft--;
        //print(secondsLeft + " seconds left");

        if(secondsLeft <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game ended");
        CancelInvoke(nameof(TimerTick));
        Time.timeScale = 0;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    #endregion

    #region PickUp Methods
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
    }

    public void AddTime(int time)
    {
        secondsLeft += time;
    }

    public void Freeze(int time)
    {
        CancelInvoke(nameof(TimerTick));
        InvokeRepeating(nameof(TimerTick), time, 1);
    }

    public void AddKey(KeyColor color)
    {
        switch(color)
        {
            case KeyColor.Red:
                redKey++;
                break;
            case KeyColor.Green:
                greenKey++;
                break;
            case KeyColor.Gold:
                goldKey++;
                break;
        }
    }
    #endregion
}
