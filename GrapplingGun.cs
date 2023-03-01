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



private void Start() {
    pm=GetComponentInParent<PlayerMovement>();
    GetClosestCeilingAhead();
}


private void Update() {
    if (Input.GetKeyDown(grappleKey))
        {
            StartGrappleWithAnim();
        }


        if (grapplingCDtimer>0)
    {
        grapplingCDtimer -= Time.deltaTime;
    }

    
    tavan= GameObject.FindGameObjectWithTag("Tavan");
    //Debug.Log(FindDirectionVector(transform.position,tavan.transform.position));
}

    private void StartGrappleWithAnim()
    {   
        if (grapplingCDtimer>0)
    {
        return;
    }

        StartCoroutine("StartGrappleSequence");
        /*animationController.CrossFade("attack_sword_01");//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Invoke("StartGrapple", 0.2f);//StartGrapple();*/
    }

    private void LateUpdate() {
    /*if (grappling)
    {
        lineRenderer.SetPosition(0,gunTip.position);
    }*/
}

    private IEnumerator StartGrappleSequence(){
        RaycastHit hit;

        if (Physics.Raycast(point.position,GetDirection() ,out hit,maxGrappleDistance,whatIsGrappable))
        {
        animationController.CrossFade("attack_sword_01");
        yield return new WaitForSeconds(0.2f);
        StartGrapple();
        animationController.CrossFade("sit_idle@loop");
        }

    }




void StartGrapple(){
    

    grappling= true;
    


    RaycastHit hit;

    if (Physics.Raycast(point.position,GetDirection() ,out hit,maxGrappleDistance,whatIsGrappable))// transform.TransformDirection(Vector3.up) eskiden direction buydu
    {
        grapplePoint= hit.point;

        Invoke(nameof(ExecuteGrapple),grappleDelayTime);
    }

    else
    {
        grapplePoint=point.position + transform.TransformDirection(Vector3.up); //point.forward*maxGrappleDistance;
        Debug.Log("hocam bişeye tutturamadık valla");
        Invoke(nameof(StopGrapple),grappleDelayTime);
    }

}

void ExecuteGrapple(){
    
    
    
    Vector3 lowestPoint= new Vector3(transform.position.x,transform.position.y-1f,transform.position.z);//-1f normali

    float grapplePointRelativeYPos= grapplePoint.y-lowestPoint.y;//herhangi bi değer çıkarma yok lowest point harici
    float highestPointOnArc=grapplePointRelativeYPos+overshootYaxis;

    if (grapplePointRelativeYPos<0)
    {
        highestPointOnArc=overshootYaxis;
    }
    Vector3 somevector= new Vector3(0,1,0);
    pm.JumpToPosition(grapplePoint,highestPointOnArc);//grapple pointe atlayacak
    Invoke(nameof(StopGrapple),1f);
}

void StopGrapple(){
    animationController.Blend("tumbling",1f);
    grappling= false;
    grapplingCDtimer=grapplingCD;
    //lineRenderer.enabled=false;
    


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
