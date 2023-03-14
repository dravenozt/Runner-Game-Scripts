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
    public Variables variables;

    //touch variables
    Vector2 endposition;
    Vector2 startposition;
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
        cam.GetComponent<AudioListener>().enabled=variables.isSoundEnabled;
        
    }

    
    void Update()
    {
        if (activeGrapple)
        {
            return;
        }

        //Speed Up the player over time
        if (playerSpeed<1500)
        {
            playerSpeed += Time.deltaTime * 5;
        }

        //Get key presses
        GetInput();

        //get mobile swipes
        GetInputMobile();

        RBControlSideMovement();

        //Codes to run when hit
        WhenDie();

    }




    private void FixedUpdate()
    {
        
        //When not changing lane, constraint x position
        ConstraintXposition();
        Jump();
        

        //Move character forward
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, playerSpeed * Time.fixedDeltaTime);



    }

        private void WhenDie()
    {
        if (collisionTimer > 0.02)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            rb.useGravity = false;
            rb.constraints=RigidbodyConstraints.FreezeAll;
            cam.GetComponent<AudioSource>().enabled=false;
            GetComponent<PlayerMovement>().enabled = false;


        }
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

    private void GetInputMobile()
    {
        if (Input.touchCount>0)
        {   
            Touch touch = Input.GetTouch(0);

            if (touch.phase==TouchPhase.Began)
            {
                startposition= touch.position;
            }


            



            if (!isChangingLane)
            {   
                //get touch
            if (touch.phase==TouchPhase.Ended)
            {
                endposition= touch.position;
            
                
                
                //get right swipe
                if (endposition.x>startposition.x&&(endposition.x-startposition.x>endposition.y-startposition.y)&&(endposition.x-startposition.x>startposition.y- endposition.y))
                {   
            
                    isChangingLane=true;       
                    isTurningRight=true;
                    isTurningLeft=false;
                    
                }

          
            
            
            
            
                //get left swipe
                if (endposition.x<startposition.x && (startposition.x-endposition.x>endposition.y-startposition.y)&&(startposition.x-endposition.x>startposition.y- endposition.y))
                {   
                    isChangingLane=true;
                    isTurningLeft=true;
                    isTurningRight=false;
                    
                }

                //jump
                if (endposition.y>startposition.y&&(endposition.y-startposition.y>endposition.x-startposition.x)&&(endposition.y-startposition.y>startposition.x -endposition.x))
                {
                isJumping= true;
                
                isChangingLane=false;
                animationController.CrossFade("jump");
                
                }

                //grapple
                if (startposition.y>endposition.y&&(startposition.y-endposition.y>endposition.x-startposition.x)&&(startposition.y-endposition.y>startposition.x -endposition.x))
                {
                    grapplingGun.StartGrappleWithAnim();
                }


            
                   
                    
                        //jump
                    if (endposition.y>startposition.y&&(endposition.y-startposition.y>endposition.x-startposition.x)&&(endposition.y-startposition.y>startposition.x -endposition.x))
                    {
                    isJumping= true;
                    
                    isChangingLane=false;
                    animationController.CrossFade("jump");
                    
                    }

                    
                
                
            }


            

            
        
        
            }
        
        }


            


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






    private void RBControlSideMovement()
    {
        if (isTurningRight)
        {
            //Can't go right anymore
            if (currentLane >= 1)
            {
                isChangingLane=false;
                isTurningRight = false;
                return;
            }
            //Change lane
            if (!IsBetween(rb.position.x,(currentLane +1)*laneDistance+0.1f,(currentLane +1)*laneDistance-0.1f))
            { 

                xDesiredPos=(currentLane +1)*laneDistance;
                
                xPos= Mathf.MoveTowards(xPos,xDesiredPos,laneChangeSpeed * Time.deltaTime);
                

                rb.MovePosition(new Vector3(xPos,rb.position.y,rb.position.z));                
                                   

            }
            //Update variables
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

            if (!IsBetween(rb.position.x,(currentLane -1)*laneDistance+0.1f,(currentLane -1)*laneDistance-0.1f))
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



    private void Jump()
    {
        if (isJumping && canJump)
        {   
            
            //rb.AddForce(Vector3.up*Time.fixedDeltaTime*50,ForceMode.VelocityChange);
            //if (rb.position.y < 2)
            //{
                rb.AddForce(Vector3.up.normalized * Time.fixedDeltaTime * jumpDistance, ForceMode.VelocityChange);
                
            //}
            isJumping = false;
            canJump=false;
            
            
        }
    }

    private void OnCollisionEnter(Collision other) {
   
            if (!grapplingGun.grappling&&!cam.GetComponent<FollowCamera>().isDying&&other.gameObject.tag=="Ground")
            {
            canJump=true;
            animationController.CrossFade(default);//"run@loop");
            }

            if (other.gameObject.tag=="Bump")
            {
                rb.AddForce(500*Time.deltaTime*Vector3.up,ForceMode.VelocityChange);
            }
            
    }
    
    private void OnCollisionStay(Collision other) {
        //canJump=true;

        if (other.gameObject.tag=="ObstacleRoad")
        {
            //Update the collisiontimer 
            collisionTimer+=Time.deltaTime/2;

            ////////////////////////Camera Shake
            float x= Random.Range(-1f,1f);
            float y= Random.Range(-1f,1f);

            Vector3 camPos= transform.position+ cam.GetComponent<FollowCamera>().offSet.position;

            cam.transform.position= camPos+new Vector3(x,y,0)*Time.deltaTime*shakeMagnitude;

        }

        
    }

    private void OnCollisionExit(Collision other) {
        canJump=false;
        //Set collision timer to 0 to prevent sudden hits
        collisionTimer=0;

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
                
                transform.localPosition = Vector3.Lerp(transform.localPosition,targetPosition,0.2f);
                
                
                

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
                targetPosition = new Vector3((currentLane-1)*laneDistance,transform.localPosition.y, transform.position.z);
                transform.localPosition =Vector3.MoveTowards(transform.localPosition, targetPosition, laneChangeSpeed * Time.deltaTime);

                

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
