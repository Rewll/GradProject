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
}