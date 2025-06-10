using UnityEngine;
using DG.Tweening;
public class RotateLight : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond;

    private void Update()
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime, Space.World);
    }
}
