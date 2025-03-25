using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tutorialbedoeling : MonoBehaviour
{
    public GameObject tutorialText1;
    public GameObject tutorialText2;
    public GameObject tutorialText3;
    public GameObject zwartVlak;
    public GameObject knop;
    bool algebeurd = false;

    private void Start()
    {
        tutorialText1.SetActive(true);
        tutorialText2.SetActive(false);
        tutorialText2.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            zwartVlak.SetActive(false);
        }
        if ((Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0) && !algebeurd)
        {
            algebeurd = true;
            StartCoroutine(tutorialding());
        }

        if (algebeurd)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                tutorialText3.SetActive(false);
            }
        }
    }
    IEnumerator tutorialding()
    {
        yield return new WaitForSeconds(2f);
        tutorialText1.SetActive(false);
        tutorialText2.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        tutorialText2.SetActive(false);
        yield return new WaitForSeconds(.25f);
        knop.SetActive(false);
    }
}
