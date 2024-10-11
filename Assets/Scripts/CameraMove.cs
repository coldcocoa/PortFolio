using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float zDistance;
    // Start is called before the first frame update
    private void Awake()
    {
        zDistance = target.position.z - transform.position.z;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 position = transform.position;
        position.z = target.position.z - zDistance;
        transform.position = position;
    }
}
