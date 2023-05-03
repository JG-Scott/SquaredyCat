using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{


    // Start is called before the first frame update



    public static CanvasController CM;

    public bool stopAll =false;
    public List<GameObject> uiElements;

    public List<GameObject> tens;
    public List<GameObject> ones;
    void Start()
    {
        if(CM == null) {
            CM = this;
            DontDestroyOnLoad(CM);
        }
        showElement(0, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showElement(int id, float time) {
        StartCoroutine(show(id, time));
    }

    private IEnumerator show(int id, float time) {
        uiElements[id].SetActive(true);
        yield return new WaitForSeconds(time);
        uiElements[id].SetActive(false);
    }

    public void showElement(Dictionary<int, float> list, int after) {
         StartCoroutine(show(list, after));
    }

    private IEnumerator show(Dictionary<int, float> list, int after){
        foreach(var entry in list){
            uiElements[entry.Key].SetActive(true);
            yield return new WaitForSeconds(entry.Value);
            uiElements[entry.Key].SetActive(false);
        }
        if(after == 1) {
            GameObject.Find("jangleDoor").GetComponent<Animator>().SetBool("isJangling", true);
        }
    }


    public void startTimer(){
        StartCoroutine(timer());
    }

    public IEnumerator timer() {
        for(int x = 1; x >= 0; x--) {
         tens[x].SetActive(true);
            if(stopAll) {
                break;
            }
        for(int i = 9; i >=0; i--) {
            if(stopAll) {
                break;
            }
            ones[i].SetActive(true);
            yield return new WaitForSeconds(1);
            ones[i].SetActive(false);
        }
        tens[x].SetActive(false);
        }
        if(!stopAll) {
            GameManager.GM.handleEvent("death");
        }
        GameObject.Find("finalDoor").GetComponent<FinalDoor>().stopJangle();
    }


    public void turnoffAll() {
        stopAll=true;
        foreach(GameObject o in uiElements) {
            o.SetActive(false);
        }
        foreach(GameObject o in tens) {
            o.SetActive(false);
        }

        foreach(GameObject o in ones) {
            o.SetActive(false);
        }
    }
}
