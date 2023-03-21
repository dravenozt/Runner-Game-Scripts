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
    public Vector3 velocity= Vector3.zero;
    public Variables variables;

    //touch variables
    Vector2 endposition;
    Vector2 startposition;
    public bool isSpiderChasing=false;
    public GameObject spider;
    bool canSwipe=true;
    [HideInInspector]public CharacterController controller;
    [HideInInspector]public float gravity = -15f;
    //gravity resets to this float when grounded
    public float gravityOffSet=-15f;
    public float jumpSpeed;
    Vector3 upPosition;
    Vector3 desiredUpPos;
    bool doGrapple=false;
    bool shakeCamera=false;
    SpiderMovement spiderMovement;
    
    
    
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
        
        spiderMovement=spider.GetComponent<SpiderMovement>();
        rb = GetComponent<Rigidbody>();
        xPos=rb.position.x;
        ypos=rb.position.y;
        zPos= rb.position.z;
        cam.GetComponent<AudioListener>().enabled=variables.isSoundEnabled;
        controller=GetComponent<CharacterController>();
        
    }

    
    void Update()
    {   
        
        
        //Debug.Log(rb.velocity.z);
        if (activeGrapple)
        {
            return;
        }

        //Speed Up the player over time
        if (rb.velocity.z<29)
        {
            playerSpeed += Time.deltaTime * acceleration/10;
        }

        //Get key presses
        GetInput();

        //get mobile swipes
        GetInputMobile();

        //RBControlSideMovement();

        //Codes to run when hit
        WhenDie();

        if (collisionTimer>0)
        {
            isSpiderChasing=true;
            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////character controller movement
        
       
        
        
        //jump - grapple jump
        JumpCC();
        JumpGrappleCC();
        
        //move forward
        velocity= new Vector3(0,gravity*Time.deltaTime,playerSpeed*Time.deltaTime);
        controller.Move(velocity);
        
        if (controller.isGrounded)
        {   gravityOffSet=-15f;
            gravity=gravityOffSet;
            velocity.y=gravity;
            grapplingGun.canGrappleJump=false;
            
            isJumping=false;
            canJump=true;
        }
        
        if (grapplingGun.canGrappleJump)
        {
            //gravity+=gravity*Time.deltaTime*0.5f;
        }
        
        if (!controller.isGrounded)//&&!grapplingGun.canGrappleJump)
        {
            gravity+=gravity*Time.deltaTime;
        }
        

        
        
        ControlSideMovementCC();
        
        
        //Debug.Log(controller.isGrounded);

        if ((controller.collisionFlags&CollisionFlags.Sides)!=0||(controller.collisionFlags&CollisionFlags.Above)!=0)
        {   
            if (Time.time<3)
            {
                return;
            }
            if (spiderMovement.canDie)
            {
                StartCoroutine("ShakeCameraAndDie");
                rb.constraints=RigidbodyConstraints.FreezeAll;
            }
            
            //Debug.Log("yandan veya yukardan temas");
            isSpiderChasing=true;
            shakeCamera=true;
        }

        
        if (controller.velocity.z<0.5f)
        {   
            StartCoroutine("ShakeCameraAndDie");
            rb.constraints=RigidbodyConstraints.FreezeAll;
            
            //WhenDieCC();
        }

        if (shakeCamera)
        {
            StartCoroutine("ShakeCamera");
        }
        
        
        

    }




    private void FixedUpdate()
    {
        
        //When not changing lane, constraint x position
        //ConstraintXposition();
        //Jump();
        

        //Move character forward
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, playerSpeed * Time.fixedDeltaTime);//////////////////////////////////////////ileri hareket ettiren şey

        if (doGrapple)
        {
            grapplingGun.StartGrappleWithAnim();
            doGrapple=false;
        }

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
                canSwipe=true;
            }


            



            if (true)
            {   
                

            
                //get touch
            if (touch.phase==TouchPhase.Moved)
            {
                endposition= touch.position;
                
            
                
                
                //get right swipe
                if (endposition.x>startposition.x&&(endposition.x-startposition.x>endposition.y-startposition.y)&&(endposition.x-startposition.x>startposition.y- endposition.y)&&canSwipe)
                {   
            
                    isChangingLane=true;       
                    isTurningRight=true;
                    isTurningLeft=false;
                    canSwipe=false;

                    

                    
                    
                    
                    
                    
                    
                }

          
            
            
            
            
                //get left swipe
                if (endposition.x<startposition.x && (startposition.x-endposition.x>endposition.y-startposition.y)&&(startposition.x-endposition.x>startposition.y- endposition.y)&&canSwipe)
                {   
                    isChangingLane=true;
                    isTurningLeft=true;
                    isTurningRight=false;

                    canSwipe=false;

                    

                    
                    

                    
                    
                    
                    
                    
                    
                    
                }

                //grapple
                if (startposition.y>endposition.y&&(startposition.y-endposition.y>endposition.x-startposition.x)&&(startposition.y-endposition.y>startposition.x -endposition.x)&&canSwipe)
                {   
                    Debug.Log("valla ben grapple tuşuna bastım");
                    grapplingGun.StartGrappleWithAnim();
                    canSwipe=false;
                    
                    
                }

                //jump
                if (endposition.y>startposition.y&&(endposition.y-startposition.y>endposition.x-startposition.x)&&(endposition.y-startposition.y>startposition.x -endposition.x)&&canSwipe&&canJump)
                {
                isJumping= true;
                
                isChangingLane=false;
                animationController.CrossFade("jump");
                canSwipe=false;
                
                
                
                
                
                }

                

                


            
                   
                    
                 
                    
                
                
            }
            
            if (touch.phase==TouchPhase.Ended)
            {   
                endposition= touch.position;
                canSwipe=true;

                
                
            }
            


            

            
        
        
            }
            return;

            
        
        }


            


    }

    
    private void GetInput()
    {
        if (!controller.enabled)
        {
            return;
        }
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
            //JumpCC();
            
            if (grapplingGun.canGrappleJump)
            {
                gravity=gravityOffSet-13;
                canJump=false;
            }
            isChangingLane=false;
            animationController.CrossFade("jump");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////sonradan ekledik lazımdı denemek için
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //grapplingGun.StartGrappleWithAnim();
            doGrapple=true;
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
            if (!IsBetween(rb.position.x,(currentLane +1)*laneDistance+0.15f,(currentLane +1)*laneDistance-0.15f))
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

            if (IsBetween(rb.position.x,(currentLane +1)*laneDistance+0.4f,(currentLane +1)*laneDistance-0.4f))
            {
                //isTurningRight = false;
                isChangingLane=false;
                
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

            if (!IsBetween(rb.position.x,(currentLane -1)*laneDistance+0.15f,(currentLane -1)*laneDistance-0.15f))
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

            if (IsBetween(rb.position.x,(currentLane -1)*laneDistance+0.4f,(currentLane -1)*laneDistance-0.4f))
            {
                isChangingLane=false;                

            }
        }
    }



    private void Jump()
    {
        if (isJumping&&canJump)
        {   
            
            //rigidbody
            //rb.AddForce(Vector3.up.normalized * Time.fixedDeltaTime * jumpDistance*30000, ForceMode.Impulse);
            
                
            
            
                
                
            
            isJumping = false;
            canJump=false;
            
            
        }
    }

    private void OnCollisionEnter(Collision other) {

            canJump=true;
            if (!grapplingGun.grappling&&!cam.GetComponent<FollowCamera>().isDying&&other.gameObject.tag=="Ground")
            {
            
            animationController.CrossFade(default);//"run@loop");
            }

            if (other.gameObject.tag=="Bump")
            {
                rb.AddForce(500*Time.deltaTime*Vector3.up,ForceMode.VelocityChange);
            }

            if (spider.GetComponent<SpiderMovement>().canDie&&other.gameObject.tag=="ObstacleRoad")
            {
                GetComponent<CapsuleCollider>().enabled = false;
                 rb.useGravity = false;
            rb.constraints=RigidbodyConstraints.FreezeAll;
            cam.GetComponent<AudioSource>().enabled=false;
            GetComponent<PlayerMovement>().enabled = false;
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

            Vector3 camPos= rb.position+ cam.GetComponent<FollowCamera>().offSet.position;

            cam.transform.position= camPos+new Vector3(x,y,-4.3f)*Time.deltaTime*shakeMagnitude;
            

        }

        
    }

    private void OnCollisionExit(Collision other) {
        //canJump=false;
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


/////////////////////////////////////////////////////////////////////////////////////////////////////CharacterController


        private void ControlSideMovementCC()
    {   
        if (controller.velocity.z<0.5f)
        {
            return;
        }
        if (isTurningRight)
        {

            if (currentLane >= 1)
            {
                isChangingLane=false;
                isTurningRight = false;
                return;
            }

            if (transform.position.x != (currentLane +1)*laneDistance)
            {
                targetPosition = new Vector3((currentLane+1)*laneDistance,transform.position.y, transform.position.z ); 
                
                
                transform.position =Vector3.MoveTowards(transform.localPosition, targetPosition, laneChangeSpeed * Time.deltaTime);
                
                
                

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

            if (transform.position.x != (currentLane-1)*laneDistance)
            {
                targetPosition = new Vector3((currentLane-1)*laneDistance,transform.position.y, transform.position.z);
                transform.position =Vector3.MoveTowards(transform.localPosition, targetPosition, laneChangeSpeed * Time.deltaTime);

                

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


    private void JumpCC()
    {   
        
        if (isJumping)
        {   
            
                controller.Move(Vector3.up*Time.deltaTime*jumpSpeed);
   
        }
    }


        public void JumpGrappleCC()
    {
        if (grapplingGun.canGrappleJump)
        {   
                controller.Move(Vector3.up*Time.deltaTime*jumpSpeed*1.5f);    
            
        }
    }

   
   IEnumerator ShakeCamera(){
    if (!controller.enabled)
    {
        yield break;
    }
    cam.transform.position=new Vector3(cam.transform.position.x+ Random.Range(-0.1f,0.1f),cam.transform.position.y+Random.Range(-0.1f,0.1f),cam.transform.position.z);
    yield return new WaitForSeconds(0.1f);
    shakeCamera=false;
    
    
   }
   

   void WhenDieCC(){
        controller.enabled=false;
   }

   IEnumerator ShakeCameraAndDie(){
    if (!controller.enabled)
    {
        yield break;
    }
    cam.transform.position=new Vector3(cam.transform.position.x+ Random.Range(-0.1f,0.1f),cam.transform.position.y+Random.Range(-0.1f,0.1f),cam.transform.position.z);
    yield return new WaitForSeconds(0.1f);
    animationController.CrossFade("die");
    shakeCamera=false;
    controller.enabled=false;
    
    
   }
    
}
