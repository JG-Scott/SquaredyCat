using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    CharacterController cc;
    void Start () {
        cc = GetComponent<CharacterController>();
    }
 
 // Update is called once per frame
    void Update () {
        if (cc.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false)
        {

            GetComponent<AudioSource>().volume = Random.Range(0.3f, 0.5f);
            GetComponent<AudioSource>().pitch = Random.Range(.5f, .8f);
            
            if(GetComponent<CharacterMovement>().isRunning) {
                GetComponent<AudioSource>().pitch = Random.Range(1, 1.2f);
            }
            
            GetComponent<AudioSource>().Play();
        }
    }

}
