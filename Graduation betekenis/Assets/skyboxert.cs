using UnityEngine;

public class skyboxert : MonoBehaviour
{
    public float rotatespeed = 1.2f;
    public void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotatespeed);
    }
}
