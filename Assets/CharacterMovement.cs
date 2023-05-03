using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float speed;

    public GameObject Camera;
    public bool canMove = true;

    public bool endGame = false;


    private Vector3 movement = Vector3.zero;

    private CharacterController charCon;
    public bool isRunning = false;


    void Start()
    {
        charCon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!endGame) {
            Move();
        } 

    }

    private void Move() {
            float vertical = Input.GetAxis("Vertical")* speed;
            float horizontal = Input.GetAxis("Horizontal") * speed;
            if(Input.GetKey(KeyCode.LeftShift)) {
                speed = 10;
                isRunning = true;
                //Camera.GetComponent<Animator>().PlayInFixedTime("shake");
            } else {
                //Camera.GetComponent<Animator>().PlayInFixedTime("idle");
                isRunning = false;
                speed = 5;
            }


            
            movement = (transform.right * horizontal + transform.forward * vertical) * Time.deltaTime;
        if(canMove){
            charCon.Move(movement);
        } else {
           charCon.Move(Vector3.zero); 
        }
    }
    }
