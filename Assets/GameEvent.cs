using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{

    public string EventName;
    public bool destroyAfter;
    // Start is called before the first frame update
    void start() {

    }
    public void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            GameManager.GM.handleEvent(EventName);
            if(destroyAfter) {
                Destroy(gameObject);
            }
        }
    }
}
