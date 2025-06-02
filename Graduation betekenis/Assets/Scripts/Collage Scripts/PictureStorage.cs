using System;
using System.Collections.Generic;
using UnityEngine;

public class PictureStorage : MonoBehaviour
{
    public List<Texture> picturesStored = new List<Texture>();
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
