using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManageSound : MonoBehaviour
{
        // Start is called before the first frame update
    public static ManageSound SM;
    public bool outside;
    public bool inside; 

    public bool end = false;

    public bool chased; 

    public List<AudioSource> sounds;


    void Start()
    {
        if(SM == null) {
            SM = this;
            DontDestroyOnLoad(SM);
        }
        outside = true;
        playOutside();
    }

    public void Update() {
        if(end) {
            if(!sounds[12].isPlaying) {
                end = false;
                StartCoroutine(wait());
            }
        }
    }




    public void playOutside(){
        sounds[0].Play();
    }

    public void stopOutside(){
        sounds[0].Stop();
    }

    public void playRustling() {
        sounds[1].Play();
    }

    public void playRustlingSecond() {
        sounds[2].Play();
    }

    public void playRustlingThird() {
        sounds[3].Play();
    }

    public void playStartChase() {
        sounds[4].Play();
    }
    public void stopStartChase(){
        sounds[4].Stop();
    }

    public void playGlassBreak() {
        sounds[5].Play();
    }

    public void playBanging() {
        sounds[6].Play();
    }

    public void stopBanging() {
        sounds[6].Stop();
    }

    public void playInside() {
        sounds[7].Play();
    }

    public void stopInside() {
        sounds[7].Stop();
    }

    public void playSlamOpen() {
        sounds[8].Play();
    }

    public void playInsideChase() {
        sounds[9].Play();
    }

    public void stopInsideChase() {
        sounds[9].Stop();
    }


    public void playHeartBeat() {
        sounds[10].Play();
    }

    public void playScream() {
        sounds[11].Play();
    }

    public void playShotgun() {
        sounds[12].Play();
        StartCoroutine(endstall());
    }

    public void playShotguncock() {
        sounds[13].Play();
    }
    public void playShellgrab() {
        sounds[14].Play();
    }


    

    public void stopAll() {
        for(int i = 0; i <=12; i++) {
            if(i != 12){
                sounds[i].Stop();
            }

        }
    }

    public IEnumerator endstall() {
        yield return new WaitForSeconds(0.25f);
        stopAll();
        GameManager.GM.end = true;
        end = true;
        CanvasController.CM.showElement(13, 100000000);
        SceneManager.LoadScene("end");
    }

    public IEnumerator wait() {
        CanvasController.CM.showElement(10, 100000000);
        yield return new WaitForSeconds(1.25f);
        CanvasController.CM.showElement(11, 1000000);
        yield return new WaitForSeconds(1.25f);
        Cursor.lockState = CursorLockMode.None;
        CanvasController.CM.showElement(12, 1000000);
    }


    
}
