using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriekManager : MonoBehaviour
{
    [Header("References")]
    public PlayerAgent playerAgentRef;
    public AudioLayer fabriekAmbianceLayerRef;
    [Space] 
    public Image fadeVlak;
    public Animator machineAnim;
    [Space] 
    public List<GameObject> ophangTutorialdingen;
    [Space]
    [Header("Variables:")]
    public int volgendeSceneIndex;
}
