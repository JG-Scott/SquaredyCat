using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public GameObject DeathCat;
    public GameObject frontLight;
    // Start is called before the first frame update
    public void handleDeath(){
        frontLight.GetComponent<Light>().intensity = 1.77f;
        DeathCat.SetActive(true);
    }
}
