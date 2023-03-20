using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class GrapplingGun : MonoBehaviour
{
private PlayerMovement pm;
public Transform point;
public Transform gunTip;
public LayerMask whatIsGrappable;
public LineRenderer lineRenderer;


public float maxGrappleDistance;
public float grappleDelayTime;

private Vector3 grapplePoint;

[Header("Cooldown")]
public float grapplingCD;
private float grapplingCDtimer;

public KeyCode grappleKey= KeyCode.DownArrow;

public bool grappling;
public float overshootYaxis;

//deneme
private GameObject tavan;

public Animation animationController;
public bool canGrappleJump=false;
public LayerMask layerMask;
public GameObject[] allObjects;




public Collider[] colliders;///////////////////////////////////////sonradan



private void Start() {
    
    
    pm=GetComponentInParent<PlayerMovement>();
    GetClosestCeilingAhead();

    
}


private void Update() {
    
    //pc control
    /*if (Input.GetKeyDown(grappleKey))
    {
            StartGrappleWithAnim();
    }*/
    
    if (colliders.Length>0)
    {
        Debug.Log("haha");
    }
    

        if (grapplingCDtimer>0)
    {
        grapplingCDtimer -= Time.deltaTime;
    }

    
    tavan= GameObject.FindGameObjectWithTag("Tavan");
    
}



    public void StartGrappleWithAnim()
    {   
        if (grapplingCDtimer>0)
    {
        return;
    }

        StartCoroutine("StartGrappleSequence");
        
    }

    private void LateUpdate() {
    
}

    private IEnumerator StartGrappleSequence(){
        //RaycastHit hit;
        grapplePoint=GetClosestCeilingAhead().transform.position;
        if(Vector3.Distance(transform.position,grapplePoint)<25)//if(colliders.Length>0)//if (Physics.Raycast(point.position,GetDirection() ,out hit,maxGrappleDistance,whatIsGrappable))
        {
        
        animationController.CrossFade("attack_sword_01");
        yield return new WaitForSeconds(0.2f);
        StartGrapple();
        animationController.CrossFade("sit_idle@loop");

        
        }

        

    }




void StartGrapple(){
    

    grappling= true;
    
    

    //RaycastHit hit;

    if(Vector3.Distance(transform.position,grapplePoint)<25)//if(colliders.Length>0)//if (Physics.Raycast(point.position,GetDirection() ,out hit,maxGrappleDistance,whatIsGrappable))// transform.TransformDirection(Vector3.up) eskiden direction buydu
    {
        grapplePoint=GetClosestCeilingAhead().transform.position;//colliders[0].gameObject.transform.position; //hit.point;

        Invoke(nameof(ExecuteGrapple),grappleDelayTime);
        GetClosestCeilingAhead().GetComponent<Collider>().enabled=false;//hit.collider.enabled=false;///////////////////////////////////////////////////////////////////////////////////////////////////////////buraya iyi bakkkkkkkkk
        
    }

    else
    {
        grapplePoint=point.position + transform.TransformDirection(Vector3.up); //point.forward*maxGrappleDistance;
        Debug.Log("hocam bişeye tutturamadık valla");
        Invoke(nameof(StopGrapple),grappleDelayTime);
    }

}

void ExecuteGrapple(){
    
    canGrappleJump=true;
    //grappling=true;////////////////////////////sonradan ekledim
    Vector3 lowestPoint= new Vector3(transform.position.x,transform.position.y-1f,transform.position.z);//-1f normali

    float grapplePointRelativeYPos= grapplePoint.y-lowestPoint.y;//herhangi bi değer çıkarma yok lowest point harici
    float highestPointOnArc=grapplePointRelativeYPos+overshootYaxis;

    if (grapplePointRelativeYPos<0)
    {
        highestPointOnArc=overshootYaxis;
    }
    Vector3 somevector= new Vector3(0,1,0);
    
    //pm.JumpToPosition(grapplePoint,highestPointOnArc);//grapple pointe atlayacak eskiden buydu
    pm.JumpGrappleCC();
    Invoke(nameof(StopGrapple),0.8f);/////////////////1f idi burası
    
}

void StopGrapple(){
    animationController.Blend("tumbling",1f);
    grappling= false;
    grapplingCDtimer=grapplingCD;
    
    
    //canGrappleJump=false;
    


    pm.activeGrapple=false;
}



private Vector3 FindDirectionVector(Vector3 initialCoordinates,Vector3 targetCoordinates){

    
    Vector3 directionVector= targetCoordinates-initialCoordinates;
    Vector3 xdirectionVector = new Vector3(directionVector.x,0,0);
    return directionVector-xdirectionVector;
}

//find closest ceiling
private GameObject GetClosestCeilingAhead(){
    GameObject[] ceilings=GameObject.FindGameObjectsWithTag("Tavan");
    var ceilingDistances = new Dictionary<GameObject,float>();
    
    foreach (GameObject ceiling in ceilings)
    {
        if (ceiling.transform.position.z> transform.position.z)
        {   
            ceilingDistances.Add(ceiling,Vector3.Distance(transform.position,ceiling.transform.position));
        }
        
    }
    
    if (ceilingDistances.Count>0)
    {
            var maxValueKey = ceilingDistances.OrderByDescending(x=>x.Value).Last().Key;

            return maxValueKey;
    }
    else
    {
        return null;
    }
    
    
}

private Vector3 GetDirection(){
    if (GetClosestCeilingAhead()!=null)
    {
        return FindDirectionVector(transform.position,GetClosestCeilingAhead().transform.position);
    }

    else
    {
        return Vector3.forward;
    }
}

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

        public bool IsGrappling()
    {
        return grappling;
    }




}
