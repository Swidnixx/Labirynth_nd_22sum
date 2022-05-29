using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMechanim : MonoBehaviour
{
    public DoorMechanim[] doors;
    public KeyColor properKey;
    public Animator keyAnimator;
    bool alreadyUsed = false;
    bool inRange;

    private void Update()
    {
        if(!alreadyUsed && Input.GetKeyDown(KeyCode.E) && inRange)
        {
            if (CheckKey()) 
            {
                alreadyUsed = true;
                keyAnimator.SetBool("useKey", true);
                Invoke(nameof(Unlock), keyAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }

    private void Unlock()
    {
        foreach(DoorMechanim d in doors)
        {
            d.Open();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
            GameManager.instance.InfoText.text = "Use the Lock"; 
            switch (properKey)
            {
                case KeyColor.Red:
                    GameManager.instance.InfoText.color = Color.red;
                    break;

                case KeyColor.Green:
                    GameManager.instance.InfoText.color = Color.green;
                    break;

                case KeyColor.Gold:
                    GameManager.instance.InfoText.color = Color.yellow;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            GameManager.instance.InfoText.text = "";
        }
    }

    public bool CheckKey()
    {
        switch (properKey)
        {
            case KeyColor.Red:
                if(GameManager.instance.redKey > 0)
                {
                    GameManager.instance.redKey--;
                    GameManager.instance.redKeyText.text = GameManager.instance.redKey.ToString();
                    return true;
                }
                break;

            case KeyColor.Green:
                if (GameManager.instance.greenKey > 0)
                {
                    GameManager.instance.greenKey--;
                    GameManager.instance.greenKeyText.text = GameManager.instance.greenKey.ToString();
                    return true;
                }
                break;

            case KeyColor.Gold:
                if (GameManager.instance.goldKey > 0)
                {
                    GameManager.instance.goldKey--;
                    GameManager.instance.goldKeyText.text = GameManager.instance.goldKey.ToString();
                    return true;
                }
                break;
        }

        Debug.Log("Nie masz odpowiedniego klucza");
        return false;
    }
}
