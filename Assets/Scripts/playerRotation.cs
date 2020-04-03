using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private float x;
    private float z;
    private Vector3 movement;
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        movement = transform.right * x + transform.forward * z;
        transform.Rotate(x * Vector3.up * Time.deltaTime * 50);
    }
}
