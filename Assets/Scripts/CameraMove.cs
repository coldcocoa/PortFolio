using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float zDistance;
    private float xDistance;
    public float speed;
    // Start is called before the first frame update
    private void Awake()
    {
        zDistance = target.position.z - transform.position.z;
        xDistance = target.position.x - transform.position.x;

    }
   
    // Update is called once per frame
    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 position = transform.position;
        position.z = target.position.z - zDistance;
        position.x = Mathf.Lerp(position.x, target.position.x - xDistance, Time.deltaTime * speed); 
        transform.position = position;
    }
}
