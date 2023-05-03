using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{

    public AudioSource audio;

    public bool oneTime = true;
    public void startJangling() {
        GetComponent<Animator>().SetBool("jangle", true);
    }


    public void playJangle() {
        Debug.Log("Finaldoor");
        if(!GetComponent<AudioSource>().isPlaying) {
            audio.Play();
        }
    }

    public void stopJangle() {
            
            ManageSound.SM.stopInsideChase();
            ManageSound.SM.stopInside();
            Destroy(audio);
            GetComponent<Animator>().SetBool("open", true);
    }

    public void BangOpen() {
        if(oneTime){
            ManageSound.SM.playSlamOpen();
            oneTime = false;
        }
    }

    public void playOpen() {
        ManageSound.SM.playSlamOpen();
    }
}
