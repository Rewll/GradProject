using System;
using UnityEngine;

public class redobjectScript : MonoBehaviour
{
    private BoxCollider _bc;
    public void Awake()
    {
        _bc = gameObject.GetComponent<BoxCollider>();
    }

    public void SetTriggerTrue()
    {
        _bc.enabled = true;
    }

    public void SetTriggerFalse()
    {
        _bc.enabled = false;
    }
}
