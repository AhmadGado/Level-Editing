using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] CharacterController controller;
    [SerializeField] Animator anim;
    [SerializeField] float Speed;
    [SerializeField] AudioSource footSteps;
    private bool walk;
    private float walkMang;
    private float x;
    private float z;
    private Vector3 movement;
     
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        movement = transform.right * x + transform.forward * z;
        walkMang=movement.magnitude;
        if (walkMang > 0)
        {
            walk = true;
            anim.SetBool("Walk", walk);
        }
        else
        {
            walk = false;
            anim.SetBool("Walk", walk);
        }
        
        controller.Move(movement * Speed * Time.deltaTime);
    }

}
