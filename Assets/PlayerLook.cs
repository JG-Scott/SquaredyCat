using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public float mouseSens = 100f;


    public bool canLook = true;

    public Transform playerBody;

    private float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Period)) {
            GameManager.GM.sensitivity+=10;
        }
        if(Input.GetKeyDown(KeyCode.Comma)) {
            GameManager.GM.sensitivity-=10;
        }
        if(canLook){

        float mouseX = Input.GetAxis("Mouse X") * GameManager.GM.sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * GameManager.GM.sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up, mouseX);

        }


    }
}
