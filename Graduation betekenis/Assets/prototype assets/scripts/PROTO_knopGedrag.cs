using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PROTO_knopGedrag : MonoBehaviour
{
    public GameObject drukStuk;
    public bool actief;
    [Header("Knop Events")]
    public UnityEvent onMouseEnter;
    public UnityEvent onMouseOver;
    public UnityEvent onMouseClick;
    public UnityEvent onMouseExit;
    public UnityEvent onMouseExitWithoutClick;
    [Space]
    public bool muisRaakt;
    bool geklikt;
    [Header("Raycast zooi")]
    public LayerMask knoplayer;
    public float rayLength = 100;
    Ray ray;

    private void Update()
    {
        if (actief)
        {
            if (raycastcheck(knoplayer) && !muisRaakt)
            {
                geklikt = false;
                Debug.Log("muis betreed");
                muisRaakt = true;
                onMouseEnter.Invoke();
            }

            if (Input.GetMouseButtonDown(0) && muisRaakt)
            {
                Debug.Log("op knop geklikt");
                geklikt = true;
                onMouseClick.Invoke();
            }

            if (muisRaakt)
            {
                Debug.Log("muis is eroverheen");
                onMouseOver.Invoke();
            }

            if (!raycastcheck(knoplayer) && muisRaakt)
            {
                Debug.Log("muis verlaat");
                muisRaakt = false;
                onMouseExit.Invoke();
                if (!geklikt)
                {
                    //Debug.Log("hover");
                    onMouseExitWithoutClick.Invoke();
                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * rayLength);
    }

    bool raycastcheck(LayerMask lm)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength, lm))
        {
            if (hit.transform.gameObject == drukStuk)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void zetActief()
    {
        actief = true;
    }
}
