using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{   
    public GameObject player;
    public Transform spiderChasePos;
    public Transform spiderBackPos;
    Vector3 velocity= Vector3.zero;
    float catchTime;
    public SkinnedMeshRenderer spiderMesh;
    bool chase=true;
    public float chaseInterval;
    public bool canDie=false;

    private void Start() {
        catchTime=0.3f;
    }

    private void Update() {
        
        
        
        
        if (transform.localPosition.z<-5)
        {
            spiderMesh.enabled=false;
        }

        else
        {
            spiderMesh.enabled=true;
        }

        if (player.GetComponent<PlayerMovement>().activeGrapple)
        {
            StopChasing();
        }

        if (player.GetComponent<PlayerMovement>().isSpiderChasing)
        {   
            canDie=true;
            StartChasing();
            

            if (chase)
            {
                StartCoroutine("WaitAndDropChase");
                
            }
            StopChasing();

            
            
        }




        
    }


    


    public void StartChasing(){
        if (chase&&player.GetComponent<PlayerMovement>().isSpiderChasing)
        {
            //set the spider active
        spiderMesh.enabled=true;
        
        //change position
        transform.localPosition= Vector3.SmoothDamp(transform.localPosition,
        spiderChasePos.position,ref velocity,catchTime);
        }

        
    }


    public void StopChasing(){
        if (!chase)
        {   
            Debug.Log("chase bırakılıyor");
            transform.localPosition= Vector3.SmoothDamp(transform.localPosition,
            spiderBackPos.position,ref velocity,catchTime+3);
        }

        if (transform.localPosition.z<-5&&!chase)
        {
                
                chase=true;
                spiderMesh.enabled=false;
                player.GetComponent<PlayerMovement>().isSpiderChasing= false;
                canDie=false;
                
        }
        



    }
    IEnumerator WaitAndDropChase(){
        yield return new WaitForSeconds(chaseInterval);
        chase=false;
        //yield return new WaitForSeconds(3f);
        
        

        
        
    }

    
}
