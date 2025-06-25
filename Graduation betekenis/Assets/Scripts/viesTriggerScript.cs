using System;
using UnityEngine;
using UnityEngine.Events;

public class viesTriggerScript : MonoBehaviour
{
    public PlayerTrigger playTrigRef;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playTrigRef.enabled = true;
        }
    }
}