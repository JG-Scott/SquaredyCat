using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{   

    public NavMeshAgent enemy;
    public GameObject player;
    private bool isAttacking = false;
    private bool isAttackingFinal = false;

    private bool endGame = false;
    private string animtype = "Chase";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) {
            player = GameObject.Find("playerfinal");
        }
        transform.LookAt(player.transform);

        if(isAttacking) {
             GetComponent<Animator>().SetBool(animtype, true);
            enemy.SetDestination(player.transform.position);
            if(Vector3.Distance(player.transform.position, gameObject.transform.position) <= 20) {
                enemy.speed = 9;
            } else {
                 enemy.speed = 20;
            }
        } 

    }

    public void attack(bool a, bool isRunning = false) {
        isAttacking = a;
        if(isRunning) {
            animtype = "run";
            enemy.speed  = 15;
            GetComponent<AudioSource>().Play();
        }
    }


    private  IEnumerator stallforSecond() {
        transform.position = new Vector3(-13.04f,4.48f,11.13f);
        ManageSound.SM.playHeartBeat();

        yield return new WaitForSeconds(10);
            foreach(SkinnedMeshRenderer s in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>()) {
                s.enabled = true;
            }
            GetComponent<Animator>().PlayInFixedTime("upright");
            ManageSound.SM.playScream();
            enemy.isStopped = false;
            enemy.SetDestination(player.transform.position);
            enemy.speed = 50;
            enemy.acceleration = 100;
            enemy.angularSpeed =1000;
            yield return new WaitForSeconds(0.25f);
            ManageSound.SM.playShotgun();
            
    }

    public void Finalattack() {
        endGame = true;
        isAttacking = false;
        enemy.isStopped = true;
        foreach(SkinnedMeshRenderer s in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>()) {
            s.enabled = false;
        }
        StartCoroutine(stallforSecond());
    }

    public void OnTriggerEnter(Collider col) {
        if(col.tag == "Player" && endGame) {
        } else if(col.tag == "Player") {
            Debug.Log("kill player");
           GameManager.GM.handleEvent("death");
        }
    }
}
