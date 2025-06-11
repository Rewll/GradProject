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
    private bool _firstLandMark = true;
    
    public void ActivateLandMark(int newLandMarkIndex)
    {
        if (!_firstLandMark && newLandMarkIndex == currentLandMarkIndex)
            return; 
        
        if (!_firstLandMark)
        {
            landMarkLayerRef.FadeOutSound(currentLandMarkIndex, landMarkFadeOutTime);
            landMarkLayerRef.FadeInSound(newLandMarkIndex, landMarkFadeInTime);
            Debug.Log("LM " + newLandMarkIndex + " Activated --- " + "Lm " + currentLandMarkIndex + " deactivated");
        }
        else if (_firstLandMark)
        {
            landMarkLayerRef.FadeInSound(newLandMarkIndex, landMarkFadeInTime);
            _firstLandMark = false;
            Debug.Log("LM " + newLandMarkIndex + "activated");
        }
        currentLandMarkIndex = newLandMarkIndex;
    }
}