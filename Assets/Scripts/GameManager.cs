using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] int secondsLeft = 100;

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
        //InvokeRepeating(nameof(TimerTick), 3, 1);
        StartCoroutine(Stopper());
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
        print(secondsLeft + " seconds left");

        if(secondsLeft <= 0)
        {
            // game ended
        }
    }
}
