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
    private bool isChangingLane=false;

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
    private bool isJumping=false;
    public float jumpDistance;
    private float ypos;
    private bool canJump;
    public Animation animationController;
    //public float jumpSpeed;
    
    //Vector3 verticalTargetPosition;

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        xPos=rb.position.x;
        ypos=rb.position.y;
        
        //rb.AddForce(Vector3.forward*movementFactor*Time.fixedDeltaTime,ForceMode.Force);
        //movementFactor=700f;
        
    }

    
    void Update()
    {


        GetInput();
        Debug.Log(rb.velocity);
        //Debug.Log(Time.time);
        //predictedPosition=transform.localPosition+rb.velocity*Time.deltaTime;
        //Debug.Log(transform.localPosition.z-rb.velocity.z*Time.time);//alınan toplam yol
        //Debug.Log(transform.forward);
        //Debug.Log(rb.GetPointVelocity(transform.localPosition));

        //rb.velocity = new Vector3(0, 0, playerSpeed);
        
        //ControlSideMovement();

        
        

    }

    

    private void FixedUpdate() {
        
        
        
        rb.AddForce(Vector3.forward*Time.fixedDeltaTime*playerSpeed,ForceMode.VelocityChange);
        RBControlSideMovement();

        rb.drag= rb.drag- rb.drag*Time.fixedDeltaTime*0.001f*acceleration;


        if (isJumping&&canJump)
        {
            //rb.AddForce(Vector3.up*Time.fixedDeltaTime*50,ForceMode.VelocityChange);
            if (rb.position.y<2)
            {   
                rb.AddForce(Vector3.up*Time.deltaTime*jumpDistance,ForceMode.VelocityChange);
            }
            isJumping=false;
            
        }

        


           
               


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

        if (Input.GetKey(KeyCode.LeftArrow))
        {   
            isChangingLane=true;
            isTurningLeft=true;
            isTurningRight=false;
            
        }
        }


        if (Input.GetKeyDown(KeyCode.UpArrow)&&canJump)
        {
            isJumping= true;
            
            animationController.CrossFade("jump");
        }
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

            if (rb.position.x != (currentLane +1)*laneDistance)
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

            if (rb.position.x != (currentLane-1)*laneDistance)
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
        if (other.gameObject.tag=="Ground")
        {   
            canJump=true;
            animationController.CrossFade("run@loop");
        }

        else
        {
            canJump=false;
        }
    }

    private void OnCollisionExit(Collision other) {
        canJump=false;
    }













//Control Side Movement works with transforms, but this causes movement problems, doesnt work with rigid body movement
//from now on , we will work with RBControlSideMovement function

/*
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

            if (transform.position.x != (currentLane +1)*laneDistance)
            {
                targetPosition = new Vector3((currentLane+1)*laneDistance,transform.localPosition.y, transform.position.z ); 
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, laneChangeSpeed * Time.deltaTime);
                
                
                

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
                targetPosition = new Vector3((currentLane-1)*laneDistance,transform.localPosition.y, transform.position.z);//transform.localPosition.y, transform.localPosition.z); //(currentLane + direction) * laneOffset
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, laneChangeSpeed * Time.deltaTime);
                
                

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



*/
    
}
