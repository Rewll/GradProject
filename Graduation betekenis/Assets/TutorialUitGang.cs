using System;
using UnityEngine;
using UnityEngine.Events;

public class TutorialUitGang : MonoBehaviour
{
    public UnityEvent spelerDoorUitgang;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spelerDoorUitgang.Invoke();
        }
    }

}
