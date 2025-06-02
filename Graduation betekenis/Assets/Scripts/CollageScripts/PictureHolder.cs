using System;
using UnityEngine;

public class PictureHolder : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
