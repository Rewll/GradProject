using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class LandMarkManager : MonoBehaviour
{
    [SerializeField] private AudioLayer landMarkLayerRef;
    [Space]
    public List<GameObject> landmarks = new List<GameObject>();
    [SerializeField] private int currentLandMarkIndex = 0;
    [Space] 
    [SerializeField] private float landMarkFadeInTime;
    [SerializeField] private float landMarkFadeOutTime;
    [Space] 
    public bool debug;
    public void ActivateLandMark(int newLandMarkIndex)
    {
        if (landMarkLayerRef.Isplaying())
        {
            if (newLandMarkIndex == currentLandMarkIndex)
            {
                Debug.Log("LM " + newLandMarkIndex + " was activated, but is already playing");
            }
            else
            {
                landMarkLayerRef.FadeOutSound(currentLandMarkIndex, landMarkFadeOutTime);
                landMarkLayerRef.FadeInSound(newLandMarkIndex, landMarkFadeInTime);
                Debug.Log("LM " + newLandMarkIndex + " Activated --- " + "Lm " + currentLandMarkIndex + " deactivated");
            }
        }
        else
        {
            landMarkLayerRef.FadeInSound(newLandMarkIndex, landMarkFadeInTime);
            Debug.Log("LM " + newLandMarkIndex + " Activated");
        }
        currentLandMarkIndex = newLandMarkIndex;
    }

    public void audioDebug()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ActivateLandMark(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateLandMark(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateLandMark(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateLandMark(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateLandMark(4);
        }
    }

    private void Update()
    {
        if (debug)
        {
            audioDebug();
        }
    }
}