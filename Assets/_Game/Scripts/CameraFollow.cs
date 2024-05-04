using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform traget;
    public Vector3 offest;
    public float value;

    private void LateUpdate()
    {
        Vector3 pos = traget.position + offest;

        transform.position = Vector3.Lerp(transform.position, pos, value * Time.deltaTime);
    }
}
