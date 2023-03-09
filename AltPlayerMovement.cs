using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayerMovement : MonoBehaviour
{
[Header("Arrange the movement speed")]
    public float movementFactor;
    Rigidbody rb;

    [Header("Arranger the side movement")]
    //public float sideMovementFactor;
    public bool isTurningRight=false;
    private bool isTurningLeft=false;
    public bool isChangingLane=false;

    public int currentLane=1;
    [Header("PlayerSpeed")]
    public float playerSpeed;
    public float laneChangeSpeed;

    Vector3 targetPosition;
    [Header("Arrange the each lane distances")]
    public int laneDistance;
    float xDesiredPos;
    float xPos;
    [Header("Change rate of velocity over time")]
    public float acceleration;
    public bool isJumping=false;
    public float jumpDistance;
    private float ypos;
    public bool canJump;
    public Animation animationController;
    private float zPos;
    public bool activeGrapple;
    public GrapplingGun grapplingGun;
    Vector3 velocity= Vector3.zero;
    CharacterController controller;
    bool isMid=true;
    bool isRight=false;
    bool isLeft=false;
    float lane=0;

    
    
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        xPos=rb.position.x;
        ypos=rb.position.y;
        zPos= rb.position.z;
        controller= GetComponent<CharacterController>();
        //xRotation=transform.rotation.x;
        
        //rb.AddForce(Vector3.forward*movementFactor*Time.fixedDeltaTime,ForceMode.Force);
        //movementFactor=700f;
        
    }

    
    void Update()
    {
        if (activeGrapple)
        {
            return;
        }

        GetInput();
        MoveSide();
        
        
        
        

    }

    

    private void FixedUpdate()
    {   
        

        

        Jump();
 

        

    }

    

    
  


    
    private void GetInput()
    {
        if (!isChangingLane)
        {   
            if (Input.GetKeyDown(KeyCode.RightArrow))
        {   
            
            isChangingLane=true;       
            isTurningRight=true;
            isTurningLeft=false;

            
            
                
            

            
            

          
            
            
            
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {   
            isChangingLane=true;
            isTurningLeft=true;
            isTurningRight=false;
            
        }
        }


        if (Input.GetKeyDown(KeyCode.UpArrow)&&canJump)
        {
            isJumping= true;
            
            isChangingLane=false;
            animationController.CrossFade("jump");
        }

       

    }
        

    private void Jump()
    {
        if (isJumping && canJump)
        {   
            
            //rb.AddForce(Vector3.up*Time.fixedDeltaTime*50,ForceMode.VelocityChange);
            if (rb.position.y < 2)
            {
                rb.AddForce(Vector3.up * Time.deltaTime * jumpDistance, ForceMode.VelocityChange);
            }
            isJumping = false;
            
            
        }
    }

    private void OnCollisionEnter(Collision other) {
       // if (other.gameObject.tag=="Ground")
        //{   
            if (!grapplingGun.grappling)
            {
            canJump=true;
            animationController.CrossFade("run@loop");
            }
        //}

        /*else
        {
            canJump=false;
        }*/
    }
    private void OnCollisionStay(Collision other) {
        canJump=true;
    }

    private void OnCollisionExit(Collision other) {
        canJump=false;
    }


    private Vector3 velocityToSet;
    private void SetVelocity(){
        
        rb.velocity=velocityToSet;
    }
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight){
        activeGrapple= true;

        velocityToSet=CalculateJumpVelocity(transform.position,targetPosition,trajectoryHeight);

        //rb.velocity= CalculateJumpVelocity(transform.position, targetPosition,trajectoryHeight);
        Invoke(nameof(SetVelocity),0.1f);
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint,Vector3 endPoint,float trajectoryHeight){
        float gravity= Physics.gravity.y;
        float displacementY= endPoint.y-startPoint.y;

        Vector3 displacementXZ = new Vector3(endPoint.x-startPoint.x,0f,endPoint.z-startPoint.z);//endPoint.x-startPoint.x vector3 ün asıl değeri

        Vector3 velocityY= Vector3.up*Mathf.Sqrt(-2*gravity*trajectoryHeight)*1.5f;// sonradan eklediğim çarpanlar aslında yok(0.8f)
        Vector3 velocityXZ= displacementXZ/(Mathf.Sqrt(-2*trajectoryHeight/gravity)+ Mathf.Sqrt(2*(displacementY-trajectoryHeight)/gravity));

        return rb.velocity.z*velocityXZ+ velocityY;//ilk çarpan yok

    }

    public bool IsBetween(float testValue, float bound1, float bound2)
     {
        return (testValue >= Mathf.Min(bound1,bound2) && testValue <= Mathf.Max(bound1,bound2));
     }










    private void MoveSide(){
        if (isTurningRight)
        {   
            //Vector3 targetPosition= new Vector3(1,rb.position.y,rb.position.z); 
            //rb.position= Vector3.Lerp(rb.position,targetPosition,0.1f);
            Vector3 targetPosition= new Vector3(1,rb.position.y,rb.position.z); 
            rb.position= Vector3.Lerp(rb.position,targetPosition,0.1f);
            //lane=Mathf.MoveTowards(lane,1,0.1f);
            //Debug.Log(lane);
            //isMid=false;
            
            
            
            
        }

        if (isTurningLeft)
        {   
             
            rb.position= Vector3.Lerp(rb.position,rb.position -Vector3.right,0.1f);
        }
    }

}
