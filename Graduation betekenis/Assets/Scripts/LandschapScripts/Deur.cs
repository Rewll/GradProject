using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Events;

public class Deur : MonoBehaviour
{
    public UnityEvent onEnter;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("iets is er");
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("speler is er");

            onEnter.Invoke();
        }
    }
}
