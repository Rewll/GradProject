using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Events;

public class Landmark : MonoBehaviour
{
    public int audioClipNum;
    public UnityEvent onEnter;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("iets is er");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("speler is er");

            onEnter.Invoke();
        }
    }
}
