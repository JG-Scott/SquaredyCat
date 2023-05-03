using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{


    public GameObject target;

    public float sensitivity = 400;
    public Camera cam;

    public static GameManager GM;

    private IEnumerator coroutine;

    public GameObject SM;

    public GameObject CM;

    public GameObject PlayerObject;
    public GameObject EnemyPrefab;
    public GameObject EnemyObject;

    public GameObject lastGun;

    public bool end = false;

    bool camNeeded = true;

    public GameObject events1;

    public GameObject events2;
    public bool isLookingAtEnemy = true;
    // Start is called before the first frame update
    void Start()
    {
        if(GM == null) {
            GM = this;
            DontDestroyOnLoad(GM);
            SceneManager.LoadScene("SampleScene");
        }
        coroutine = wait(1f);
        setScene(1);
    }

    // Update is called once per frame
    private void Update ()
    {
        
        if(PlayerObject == null) {
            PlayerObject = GameObject.Find("playerfinal");
            cam = PlayerObject.GetComponentInChildren<Camera>();
        }

             
        if(lastGun == null && !end) {
            lastGun = GameObject.Find("LastGun");
        }

        if(EnemyObject == null && !end) {
            EnemyObject = GameObject.Find("enemy");
            EnemyObject.GetComponent<EnemyAI>().attack(false);
            target = EnemyObject;
        }

        if(!isLookingAtEnemy) {
            Debug.Log("islooking");
            if (IsVisible(cam,target))
            {
                Debug.Log("islooking");
                isLookingAtEnemy = true;
                SM.GetComponent<ManageSound>().stopOutside();
                CM.GetComponent<CanvasController>().showElement(2, 1f);
                CM.GetComponent<CanvasController>().uiElements[1].SetActive(false);
                StartCoroutine(wait(1));
            }
            else
            {
            }
        }
    }

    public void handleEvent(string eventName) {
        
        switch(eventName) {
            case "bushes":
                SM.GetComponent<ManageSound>().playRustling();
                CM.GetComponent<CanvasController>().showElement(3, 2f);
                break;
            case "death":
                Destroy(PlayerObject.GetComponent<CharacterMovement>());
                Destroy(PlayerObject.GetComponent<CharacterController>());
                Destroy(PlayerObject.GetComponent<PlayerSound>());
                Destroy(cam.GetComponent<PlayerLook>());
                Debug.Log("3");
                PlayerObject.transform.rotation = Quaternion.Euler(0,0,0);
                cam.transform.rotation = Quaternion.Euler(0, 0,0);
                EnemyObject.SetActive(false);
                PlayerObject.GetComponent<Death>().handleDeath();
                StartCoroutine(deathWait());
                break;

            case "title":
                CM.GetComponent<CanvasController>().showElement(14, 2f);
                break;
            case "bushes2": 
                SM.GetComponent<ManageSound>().playRustlingSecond();
                break;

            case "LastRustle": 
                SM.GetComponent<ManageSound>().playRustlingThird();
                CM.GetComponent<CanvasController>().showElement(1, 2f);
                PlayerObject.GetComponent<CharacterMovement>().canMove = false;
                foreach(SkinnedMeshRenderer s in EnemyObject.GetComponentsInChildren<SkinnedMeshRenderer>()) {
                    s.enabled = true;
                }
                EnemyObject.GetComponent<BoxCollider>().enabled = true;
                EnemyObject.SetActive(true);
                isLookingAtEnemy = false;
                break;

            case "oneliner": 
                ManageSound.SM.stopStartChase();
                ManageSound.SM.stopOutside();
                ManageSound.SM.playSlamOpen();
                ManageSound.SM.playInside();
                StartCoroutine(pausePlayer(5.2f));
                CanvasController.CM.showElement(new Dictionary<int, float>(){{4, 2},{6, 2.2f},{5,1}}, 1);
                ManageSound.SM.playGlassBreak();
                break;
            case "finalDoor": 
                //StartCoroutine(pausePlayer(5.2f));
                //CanvasController.CM.showElement(new Dictionary<int, float>(){{4, 2},{6, 2.2f},{5,1}}, 1);
                EnemyObject.SetActive(false);
                EnemyObject.GetComponent<EnemyAI>().attack(false);
                GameObject.Find("finalDoor").GetComponent<Animator>().SetBool("close", true);
                CanvasController.CM.startTimer();
                break;
            case "spawnEnemy": 
                EnemyObject.GetComponent<NavMeshAgent>().enabled = false;
                EnemyObject.transform.position = new Vector3(-1,-.79f,-4.31f);
                EnemyObject.GetComponent<NavMeshAgent>().enabled = true;
                EnemyObject.GetComponent<EnemyAI>().attack(true, true);
                break;

            case "ShotGun1": 
                CanvasController.CM.showElement(7,1);
                GameObject.Find("shellEvent").GetComponent<BoxCollider>().enabled = true;
                break;
            case "ShotGun2": 
                CanvasController.CM.showElement(8,1);
                GameObject.Find("lastEvent").GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("shell (1)").SetActive(false);
                GameObject.Find("shell").SetActive(false);
                ManageSound.SM.playShellgrab();
                break;
            case "ShotGun3": 
                Debug.Log("1");
                CanvasController.CM.turnoffAll();
                EnemyObject.SetActive(true);
                EnemyObject.GetComponent<EnemyAI>().Finalattack();
                Debug.Log("2");
                ManageSound.SM.playShotguncock();
                PlayerObject.GetComponent<CharacterMovement>().Camera.GetComponent<Animator>().PlayInFixedTime("idle");
                Destroy(PlayerObject.GetComponent<CharacterMovement>());
                Destroy(PlayerObject.GetComponent<CharacterController>());
                Destroy(PlayerObject.GetComponent<PlayerSound>());
                Destroy(cam.GetComponent<PlayerLook>());
                Debug.Log("3");
                lastGun.transform.position = new Vector3(-13.17f,5.93f,-0.23f);
                PlayerObject.transform.position = new Vector3(-13.42f,5.94f,-2.04f);
                PlayerObject.transform.rotation = Quaternion.Euler(0,15,0);
                cam.transform.rotation = Quaternion.Euler(0, 0,0);


                break;
            default:
                Debug.Log("default");
                break;
        }
    }

    public void setScene(int sceneNum) {
        if(sceneNum==1) {
            events1.SetActive(true);
        } else if(sceneNum==2) {
            events2.SetActive(true);
            events1.SetActive(false);
           
        }
    }

    private IEnumerator pausePlayer(float time) {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("pause player");
        PlayerObject.GetComponent<CharacterMovement>().canMove = false;
        yield return new WaitForSeconds(time);
        PlayerObject.GetComponent<CharacterMovement>().canMove = true;
    }



    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point)< 0)
            {
                return false;
            }
        }
        return true;
    }

    

    private IEnumerator wait(float waittime) {
        Debug.Log("This entered");
        yield return new WaitForSeconds(waittime);
        EnemyObject.GetComponent<EnemyAI>().attack(true);
        SM.GetComponent<ManageSound>().playStartChase();
        PlayerObject.GetComponent<CharacterMovement>().canMove = true;
        Debug.Log("This Exited");
    }

    private IEnumerator deathWait() {
        yield return new WaitForSeconds(0.5f);
        CanvasController.CM.showElement(13, 3);
        ManageSound.SM.stopAll();
        yield return new WaitForSeconds(3f);

        if(SceneManager.GetActiveScene().name == "house"){
         SceneManager.LoadScene("house");

        } else {

           SceneManager.LoadScene("SampleScene");
        }

    }


    public void exitGame() {
        Application.Quit();
    }
}
