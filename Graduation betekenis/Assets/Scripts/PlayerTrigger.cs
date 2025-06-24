using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.enabled)
        {
            onEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.enabled)
        {
            onExit.Invoke();
        }
    }
}
