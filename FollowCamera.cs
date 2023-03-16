using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{   
    public GameObject player;
    public PlayerMovement pm;
    public Transform offSet;
    public bool isDying=true;
    bool canAnimate=true;
    Vector3 velocity= Vector3.zero;
    public AnimationClip die;
    Rigidbody rb;


    private void Start() {
        rb=player.GetComponent<Rigidbody>();
    }
    private void Update() {
        
        
        transform.position=Vector3.SmoothDamp(transform.position,rb.position+ offSet.position,ref velocity,0.1f);//player.transform.position+ offSet.position;
        //offSet.transform.position+=Vector3.forward*player.GetComponent<PlayerMovement>().collisionTimer*player.GetComponent<PlayerMovement>().playerSpeed/700;

        /*if (offSet.transform.position.z>-4f)
        {
            Debug.Log("öldün agacım");
        }
        */
        
                     
        if (pm.enabled==false)
        {
            //transform.localPosition=pm.camTransformHolder.transform.position;

            
            isDying= false;
            
            
            
        }


        if (!isDying&&canAnimate)
        {
            StartCoroutine("Dying");
            canAnimate=false;
        }





        
        
            
        




    }

    IEnumerator Dying(){
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<Animation>().clip=die;
        player.GetComponent<Animation>().CrossFade("die");
            
            isDying=true;
            
    }


    private void FixedUpdate() {
        
    }
    private void LateUpdate() {
        
    }

    
}
