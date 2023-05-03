using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorJangle : MonoBehaviour
{
    public int numPlays = 20;
    bool oneTime = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playJangle() {
        numPlays-=1;

        if(!oneTime) {
            ManageSound.SM.playInsideChase();
            oneTime = true;
        }
        GetComponent<AudioSource>().volume = 1.5f;
        if(!GetComponent<AudioSource>().isPlaying) {
            GetComponent<AudioSource>().Play();
        }
        
        if(numPlays <= 0) {
            Debug.Log("trhossadjj");
            GetComponent<AudioSource>().Stop();
            GetComponent<Animator>().SetBool("isJangling", false);
            //make it open/spawn enemy
        }
    }

    public void playOpen() {
        ManageSound.SM.playSlamOpen();
        GameManager.GM.handleEvent("spawnEnemy");
    }
}
