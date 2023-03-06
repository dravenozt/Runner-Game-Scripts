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
    private bool isTurningRight=false;
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
    public Vector3 speed;
    public float gravity= 12f;
   // private bool isSliding=false;
   // private bool canSlide=true;
    //public float slideAgression;
   // public float xRotation;
   // public bool stopSlidingPhase=false;
   // bool stopSlidingPhase2= false;
    //public float jumpSpeed;
    
    //Vector3 verticalTargetPosition;

    
    
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        xPos=rb.position.x;
        ypos=rb.position.y;
        zPos= rb.position.z;
        speed= new Vector3(0,0,3f);
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

        ControlSideMovement();
        Jump();

        //transform.position +=speed*Time.deltaTime;
        
        //speed.y-=gravity*Time.deltaTime;
        //Debug.Log(rb.velocity);
        
       // Debug.Log(Mathf.Sin(Time.time));
        //Debug.Log(Time.time);
        //predictedPosition=transform.localPosition+rb.velocity*Time.deltaTime;
        //Debug.Log(transform.localPosition.z-rb.velocity.z*Time.time);//alınan toplam yol
        //Debug.Log(transform.forward);
        //Debug.Log(rb.GetPointVelocity(transform.localPosition));
        //Debug.Log(transform.position.x);

        //rb.velocity = new Vector3(0, 0, playerSpeed);
        
        //ControlSideMovement();

    /*    if (isSliding)//&&transform.rotation.x!=-56)
        {   
            xRotation= Mathf.MoveTowards(xRotation,-90,slideAgression * Time.deltaTime);
            transform.rotation= Quaternion.Euler(xRotation,0,0);
            //Debug.Log("dönüyom aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            if (transform.rotation == Quaternion.Euler(-90,0,0))
            {
                stopSlidingPhase=true;
                isSliding=false;
            }
            
            
        }

        if (stopSlidingPhase)
        {   
            
            xRotation= Mathf.MoveTowards(xRotation,-60,100 * Time.deltaTime);
            transform.rotation= Quaternion.Euler(xRotation,0,0);
            

        }

        if (transform.rotation==Quaternion.Euler(-60,0,0)){

            stopSlidingPhase2=true;
        }
            if (stopSlidingPhase2)
            {   
                stopSlidingPhase=false;
                Debug.Log("hocam kalkıyorum");
                //slideAgression=200;
                xRotation= Mathf.MoveTowards(xRotation,0,slideAgression * Time.deltaTime);
                transform.rotation= Quaternion.Euler(xRotation,0,0);
                

            }
            if (transform.rotation == Quaternion.Euler(0,0,0))
            {
                    stopSlidingPhase2=false;
            }


        if (!stopSlidingPhase&&!isSliding&&!isJumping)
        {
            //animationController.CrossFade("run@loop");
        }*/
        

        
        

    }

    

    private void FixedUpdate()
    {

        //RBControlSideMovement();
        
    

        //rb.AddForce(Vector3.forward * Time.fixedDeltaTime * playerSpeed, ForceMode.VelocityChange);
        

        //rb.drag = rb.drag - rb.drag * Time.fixedDeltaTime * 0.001f * acceleration;

        //Jump();

        

        //zPos= Mathf.MoveTowards(zPos,zPos+1,playerSpeed * Time.deltaTime);
        //rb.MovePosition(new Vector3(rb.position.x,rb.position.y,zPos));
        

        
        
        rb.velocity= new Vector3(rb.velocity.x,rb.velocity.y,playerSpeed*Time.fixedDeltaTime);

        

    }



    private void LateUpdate() {
        

        
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

        /*
        if (Input.GetKeyDown(KeyCode.DownArrow)&&canSlide)
        {   
            
            isSliding=true;
            animationController.CrossFade("damage",2f);
            animationController.CrossFade("run@loop");
            // x 'i - 56 derece döndürecen

            
            
        }*/

    }






    private void RBControlSideMovement()
    {
        if (isTurningRight)
        {

            if (currentLane >= 1)
            {
                isChangingLane=false;
                isTurningRight = false;
                return;
            }

            if (!IsBetween(rb.position.x,(currentLane +1)*laneDistance+0.1f,(currentLane +1)*laneDistance-0.1f))//rb.position.x != (currentLane +1)*laneDistance
            { 

                xDesiredPos=(currentLane +1)*laneDistance;
                
                
                xPos= Mathf.MoveTowards(xPos,xDesiredPos,laneChangeSpeed * Time.deltaTime);
                rb.MovePosition(new Vector3(xPos,rb.position.y,rb.position.z));

                
                

                
                                   

            }
            else
            {
                isTurningRight = false;
                isChangingLane=false;
                currentLane++;
                //rb.constraints=RigidbodyConstraints.FreezeRotation;
                //rb.constraints=RigidbodyConstraints.FreezePositionX;

                return;
            }
            
        }


        if (isTurningLeft)
        {

            if (currentLane <= -1)
            {   
                isChangingLane=false;
                isTurningLeft = false;
                return;
            }

            if (!IsBetween(rb.position.x,(currentLane -1)*laneDistance+0.1f,(currentLane -1)*laneDistance-0.1f))//rb.position.x != (currentLane-1)*laneDistance
            {
                xDesiredPos=(currentLane -1)*laneDistance;
                xPos= Mathf.MoveTowards(xPos,xDesiredPos,laneChangeSpeed * Time.deltaTime);
                rb.MovePosition(new Vector3(xPos,rb.position.y,rb.position.z));
                
                

            }
            else
            {
                isChangingLane=false;
                isTurningLeft = false;
                currentLane--;
                
                

                return;
            }
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
    private void Jump()
    {
        if (isJumping && canJump)
        {   
            
           /* //rb.AddForce(Vector3.up*Time.fixedDeltaTime*50,ForceMode.VelocityChange);
            if (rb.position.y < 2)
            {
                rb.AddForce(Vector3.up * Time.deltaTime * jumpDistance, ForceMode.VelocityChange);
            }*/
            JumpToPosition(transform.position+Vector3.forward*20,10f);
            isJumping = false;
            activeGrapple=false;
            
            
        }
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

        Vector3 displacementXZ = new Vector3(0,0f,endPoint.z-startPoint.z);//endPoint.x-startPoint.x vector3 ün asıl değeri

        Vector3 velocityY= Vector3.up*Mathf.Sqrt(-2*gravity*trajectoryHeight)*0.8f;// sonradan eklediğim çarpanlar aslında yok
        Vector3 velocityXZ= displacementXZ/(Mathf.Sqrt(-2*trajectoryHeight/gravity)+ Mathf.Sqrt(2*(displacementY-trajectoryHeight)/gravity));

        return velocityXZ+ velocityY;//ilk çarpan yok rb.velocity.z///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }

    public bool IsBetween(float testValue, float bound1, float bound2)
     {
        return (testValue >= Mathf.Min(bound1,bound2) && testValue <= Mathf.Max(bound1,bound2));
     }


    private void ControlSideMovement()
    {
        if (isTurningRight)
        {

            if (currentLane >= 1)
            {
                isChangingLane=false;
                isTurningRight = false;
                return;
            }

            if (transform.position.x!=(currentLane +1)*laneDistance)//rb.position.x != (currentLane +1)*laneDistance
            { 
                
                xDesiredPos=(currentLane +1)*laneDistance;
                
                Vector3 targetPosition= new Vector3(xDesiredPos,transform.position.y,transform.position.z);
                
                transform.position=Vector3.MoveTowards(transform.position,targetPosition,laneChangeSpeed*Time.deltaTime);

                
                
                //transform.position= new Vector3(currentPosition,transform.position.y,transform.position.z);

                
                //xPos= Mathf.MoveTowards(xPos,xDesiredPos,laneChangeSpeed * Time.deltaTime);
                //rb.MovePosition(new Vector3(xPos,rb.position.y,rb.position.z));

                
                

                
                                   

            }
            else
            {
                isTurningRight = false;
                isChangingLane=false;
                currentLane++;
                //rb.constraints=RigidbodyConstraints.FreezeRotation;
                //rb.constraints=RigidbodyConstraints.FreezePositionX;

                return;
            }
            
        }


        if (isTurningLeft)
        {

            if (currentLane <= -1)
            {   
                isChangingLane=false;
                isTurningLeft = false;
                return;
            }

            if (transform.position.x!=(currentLane -1)*laneDistance)//rb.position.x != (currentLane-1)*laneDistance
            {
                xDesiredPos=(currentLane -1)*laneDistance;
                
                
                
                Vector3 targetPosition= new Vector3(xDesiredPos,transform.position.y,transform.position.z);
                
                transform.position=Vector3.MoveTowards(transform.position,targetPosition,laneChangeSpeed*Time.deltaTime);
                
                //Mathf.MoveTowards(transform.position.x,xDesiredPos,laneChangeSpeed*Time.deltaTime);
                //xPos= Mathf.MoveTowards(xPos,xDesiredPos,laneChangeSpeed * Time.deltaTime);
                //rb.MovePosition(new Vector3(xPos,rb.position.y,rb.position.z));
                
                

            }
            else
            {
                isChangingLane=false;
                isTurningLeft = false;
                currentLane--;
                
                

                return;
            }
        }
    }

}
