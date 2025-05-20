using UnityEngine;
using UnityEngine.EventSystems;

public class pictureToCutFrom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOver;
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
