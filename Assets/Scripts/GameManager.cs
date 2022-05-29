using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum KeyColor
{
    Red,
    Green,
    Gold
}
public class GameManager : MonoBehaviour
{
    //UI
    public GameObject endGamePanel;
    public Text endGameText;
    public Text InfoText;
    public Text timeText;
    public Text crystalText;
    public Text redKeyText;
    public Text greenKeyText;
    public Text goldKeyText;
    public GameObject snowFlake;

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
        snowFlake.SetActive(false);
        secondsLeft--;
        timeText.text = secondsLeft.ToString();

        if (secondsLeft <= 0)
        {
            EndGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void WinGame()
    {
        Cursor.lockState = CursorLockMode.None;
        endGameText.text = "You Win";
        endGamePanel.SetActive(true);
        PlayClip(winClip);
        CancelInvoke(nameof(TimerTick));
        Time.timeScale = 0;
    }
    private void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        endGameText.text = "Game Over";
        endGamePanel.SetActive(true);
        PlayClip(loseClip);
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
        crystalText.text = points.ToString();
    }

    public void AddTime(int time)
    {
        secondsLeft += time;
        timeText.text = secondsLeft.ToString();
    }

    public void Freeze(int time)
    {
        CancelInvoke(nameof(TimerTick));
        snowFlake.SetActive(true);
        InvokeRepeating(nameof(TimerTick), time, 1);
    }

    public void AddKey(KeyColor color)
    {
        switch(color)
        {
            case KeyColor.Red:
                redKey++;
                redKeyText.text = redKey.ToString();
                break;
            case KeyColor.Green:
                greenKey++;
                greenKeyText.text = greenKey.ToString();
                break;
            case KeyColor.Gold:
                goldKey++;
                goldKeyText.text = goldKey.ToString();
                break;
        }
    }
    #endregion
}
