using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForRotation : MonoBehaviour
{
    float mouseX;
    [SerializeField] float mouseSenstivity;
    [SerializeField] Transform cameraLookAtBody;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSenstivity;

        cameraLookAtBody.Rotate(Vector3.up * mouseX);
        
    }
}
