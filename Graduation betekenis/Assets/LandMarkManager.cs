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
    
    private bool _firstLandMark = true;
    
    public void ActivateLandMark(int newLandMarkIndex)
    {
        if (newLandMarkIndex == currentLandMarkIndex)
            return;  
        
        if (!_firstLandMark)
        {
            landMarkLayerRef.StopPlaying(currentLandMarkIndex);
            //landMarkLayerRef.PlaySound(newLandMarkIndex);
            landMarkLayerRef.FadeIn(newLandMarkIndex);
            Debug.Log("LM " + newLandMarkIndex + " Activated --- " + "Lm " + currentLandMarkIndex + " deactivated");
        }
        else if (_firstLandMark)
        {
            //landMarkLayerRef.PlaySound(newLandMarkIndex);
            landMarkLayerRef.FadeIn(newLandMarkIndex);
            _firstLandMark = false;
            Debug.Log("LM " + newLandMarkIndex + "activated");
        }
        currentLandMarkIndex = newLandMarkIndex;
    }
}