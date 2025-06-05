using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeselectObject : MonoBehaviour, IPointerDownHandler
{
    public CollageCreateState gameManRef;

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown from: " + this.gameObject.name );
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            gameManRef.Deselect();
        }
    }
}
