using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    public float collisionTimer=0;
    public Camera cam;
    public Transform camTransformHolder;
    public float shakeMagnitude;
    Vector3 velocity= Vector3.zero;
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
        
        //Physics.autoSyncTransforms=true;
        rb = GetComponent<Rigidbody>();
        xPos=rb.position.x;
        ypos=rb.position.y;
        zPos= rb.position.z;
        
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
        //Debug.Log(Physics.autoSyncTransforms);
        //Debug.Log(Time.time);
        RBControlSideMovement();
        //Debug.Log(collisionTimer);
        //RBControlSideMovement();
        //transform.Translate(Vector3.forward*Time.deltaTime*playerSpeed);

        
        if (collisionTimer>0.02)
        {   
            GetComponent<CapsuleCollider>().enabled=false;
            rb.useGravity=false;
            GetComponent<PlayerMovement>().enabled=false;
            
            
        }
        


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

        
        

        ConstraintXposition();

        //rb.AddForce(Vector3.forward * Time.fixedDeltaTime * playerSpeed, ForceMode.VelocityChange);


        //rb.drag = rb.drag - rb.drag * Time.fixedDeltaTime * 0.001f * acceleration;

        Jump();
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////şu ikisi aç kapa
        //rb.position= new Vector3(xPos,rb.position.y,rb.position.z);


        //transform.position= rb.position;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //zPos= Mathf.MoveTowards(zPos,zPos+1,playerSpeed * Time.deltaTime);
        //rb.MovePosition(new Vector3(rb.position.x,rb.position.y,zPos));



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////ileri hareket eden bu
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, playerSpeed * Time.fixedDeltaTime);


        

        

    }

    private void ConstraintXposition()
    {
        if (isChangingLane)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.freezeRotation = true;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.freezeRotation = true;
        }
    }

    private void LateUpdate() {
        
        //ControlSideMovement();
        
        
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

            if (!IsBetween(rb.position.x,(currentLane +1)*laneDistance+0.1f,(currentLane +1)*laneDistance-0.1f))//rb.position.x != (currentLane +1)*laneDistance //!IsBetween(rb.position.x,(currentLane +1)*laneDistance+0.1f,(currentLane +1)*laneDistance-0.1f)
            { 

                xDesiredPos=(currentLane +1)*laneDistance;
                //rb.AddForce(Vector3.right,ForceMode.VelocityChange);
                
                xPos= Mathf.MoveTowards(xPos,xDesiredPos,laneChangeSpeed * Time.deltaTime);
                
                //Vector3 targetPosition=new Vector3(xDesiredPos,rb.position.y,rb.position.z);
                rb.MovePosition(new Vector3(xPos,rb.position.y,rb.position.z));
                //rb.position= Vector3.SmoothDamp(rb.position,targetPosition,ref velocity,0.1f);
                
                //rb.position= new Vector3(xPos,rb.position.y,rb.position.z);

                
                

                
                                   

            }
            else
            {
                isTurningRight = false;
                isChangingLane=false;
                //rb.position= new Vector3(xPos,rb.position.y,rb.position.z);
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

            if (!IsBetween(rb.position.x,(currentLane -1)*laneDistance+0.1f,(currentLane -1)*laneDistance-0.1f))//rb.position.x != (currentLane-1)*laneDistance//!IsBetween(rb.position.x,(currentLane -1)*laneDistance+0.1f,(currentLane -1)*laneDistance-0.1f)
            {
                xDesiredPos=(currentLane -1)*laneDistance;
                xPos= Mathf.MoveTowards(xPos,xDesiredPos,laneChangeSpeed * Time.deltaTime);
                rb.MovePosition(new Vector3(xPos,rb.position.y,rb.position.z));
                //rb.position= new Vector3(xPos,rb.position.y,rb.position.z);
                
                

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
            if (!grapplingGun.grappling&&!cam.GetComponent<FollowCamera>().isDying&&other.gameObject.tag=="Ground")
            {
            canJump=true;
            animationController.CrossFade(default);//"run@loop");
            }

            

            //collisionTimer=0;
        //}

        /*else
        {
            canJump=false;
        }*/
    }
    
    private void OnCollisionStay(Collision other) {
        canJump=true;

        if (other.gameObject.tag=="ObstacleRoad")
        {
            
            collisionTimer+=Time.deltaTime/2;

            ////////////////////////Camera Shake
            float x= Random.Range(-1f,1f);
            float y= Random.Range(-1f,1f);

            Vector3 camPos= transform.position+ cam.GetComponent<FollowCamera>().offSet.position;

            cam.transform.position= camPos+new Vector3(x,y,0)*Time.deltaTime*shakeMagnitude;
            //cam.transform.position= cam.GetComponent<FollowCamera>().offSet.transform.position+transform.position;


            
            //cam.GetComponent<FollowCamera>().offSet.transform.position+=Vector3.forward*collisionTimer*5;
            //cam.transform.position+=Vector3.forward*shakeMagnitude*Time.deltaTime;

            
            //cam.transform.localPosition=camTransformHolder.transform.position;
        }

        
    }

    private void OnCollisionExit(Collision other) {
        canJump=false;
        collisionTimer=0;
        //cam.transform.localPosition=camTransformHolder.transform.position;
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










//Control Side Movement works with transforms, but this causes movement problems, doesnt work with rigid body movement
//from now on , we will work with RBControlSideMovement function


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

            if (transform.position.x < (currentLane +1)*laneDistance-0.1f)
            {
                targetPosition = new Vector3((currentLane+1)*laneDistance,rb.position.y, rb.position.z ); 
                
                transform.localPosition = Vector3.Lerp(transform.localPosition,targetPosition,0.2f);//Vector3.MoveTowards(transform.localPosition, targetPosition, laneChangeSpeed * Time.deltaTime);
                
                
                

            }
            else
            {
                isTurningRight = false;
                isChangingLane=false;
                currentLane++;

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

            if (transform.position.x > (currentLane-1)*laneDistance+0.1f)
            {
                targetPosition = new Vector3((currentLane-1)*laneDistance,transform.localPosition.y, transform.position.z);//transform.localPosition.y, transform.localPosition.z); //(currentLane + direction) * laneOffset
                transform.localPosition =Vector3.MoveTowards(transform.localPosition, targetPosition, laneChangeSpeed * Time.deltaTime);
                //rb.position = Vector3.SmoothDamp(rb.position,targetPosition,ref velocity,0.1f);
                //transform.localPosition = Vector3.Lerp(transform.localPosition,targetPosition,0.2f);
                

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
