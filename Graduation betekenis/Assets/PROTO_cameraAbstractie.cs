using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PROTO_cameraAbstractie : MonoBehaviour
{
    public List<GameObject> abstracteObjecten = new List<GameObject>();
    public LayerMask abstractieMask;
    public LayerMask raakLayerMask;
    LayerMask normaleMask;

    public Camera kkamera;
    RaycastHit hit;
    int rayLength = 100;

    private void Start()
    {
        normaleMask = kkamera.cullingMask;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (GetComponent<PROTO_playerModeManager>().inCameraMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, rayLength, raakLayerMask))
                {
                    if (!abstracteObjecten.Contains(hit.transform.gameObject))
                    {
                        abstracteObjecten.Add(hit.transform.gameObject);
                    }
                    //Debug.Log("hit: " + hit.transform.name
                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * rayLength);
    }
    public void abstractieTijd()
    {
        foreach (GameObject item in abstracteObjecten)
        {
            item.layer = LayerMask.NameToLayer("Abstractie");
        }
        kkamera.clearFlags = CameraClearFlags.SolidColor;
        kkamera.cullingMask = abstractieMask;
    }
}
