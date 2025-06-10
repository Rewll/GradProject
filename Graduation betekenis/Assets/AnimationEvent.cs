using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public UnityEvent enableKam;
    public UnityEvent disableKam;

    public void  EnableInvoke()
    {
        enableKam.Invoke();
    }

    public void DisableInvoke()
    {
        disableKam.Invoke();
    }
}
