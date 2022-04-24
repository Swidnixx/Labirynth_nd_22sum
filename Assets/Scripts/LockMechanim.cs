using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMechanim : MonoBehaviour
{

    public KeyColor properKey;
    bool locked = false;
    bool inRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("Lock is in range");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            Debug.Log("Lock is not in range anymore");
        }
    }

    public bool CheckKey()
    {
        throw new NotImplementedException();
    }
}
