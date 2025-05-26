using UnityEngine;
using UnityEngine.UI;

public class FabriekManager : MonoBehaviour
{
    public GameObject playerObjectRef;
    public PlayerAgent playerAgentRef;
    [Space] 
    public Image fadeVlak;

    public GameObject deurBeginRuimte;
    public Transform deurGeslotenPos;
    public Transform deurOpenPos;
    [Space] 
    public int volgendeSceneIndex;

    public void SetDeur(bool deurBool)
    {
        if (deurBool)
        {
            deurBeginRuimte.transform.position = deurOpenPos.position;
        }
        else if(!deurBool)
        {
            deurBeginRuimte.transform.position = deurGeslotenPos.position;
        }
    }
}
