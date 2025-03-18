using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatieregelaar : MonoBehaviour
{
    public Animator anim;

    public void animTrigger(string parameter)
    {
        anim.SetTrigger(parameter);
    }

    public void animInt(int getal)
    {
        anim.SetInteger("resultaat", getal);
    }

    private void Start()
    {
        if (GetComponent<PROTO_knopGedrag>())
        {
            if (!GetComponent<PROTO_knopGedrag>().actief)
            {
                anim.SetTrigger("nonactief");
            }
        }
    }
}