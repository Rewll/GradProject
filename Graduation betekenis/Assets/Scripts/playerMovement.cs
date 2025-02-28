using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float xAxis;
    public float zAxis;
    [Space]
    public bool magLopen;
    void Update()
    {
        if (magLopen)
        {
            xAxis = Input.GetAxis("Horizontal");
            zAxis = Input.GetAxis("Vertical");

            Vector3 move = transform.right * xAxis + transform.forward * zAxis;
            controller.Move(move * speed * Time.deltaTime);
        }
    }
}
