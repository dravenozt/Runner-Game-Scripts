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

private bool grappling;
public float overshootYaxis;

//deneme
private GameObject tavan;


private void Start() {
    pm=GetComponentInParent<PlayerMovement>();
    GetClosestCeilingAhead();
}


private void Update() {
    if (Input.GetKeyDown(grappleKey))
    {
        StartGrapple();
    }


    if (grapplingCDtimer>0)
    {
        grapplingCDtimer -= Time.deltaTime;
    }

    
    tavan= GameObject.FindGameObjectWithTag("Tavan");
    //Debug.Log(FindDirectionVector(transform.position,tavan.transform.position));
}


private void LateUpdate() {
    if (grappling)
    {
        lineRenderer.SetPosition(0,gunTip.position);
    }
}






void StartGrapple(){
    if (grapplingCDtimer>0)
    {
        return;
    }

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
    lineRenderer.enabled=true;
    lineRenderer.SetPosition(1,grapplePoint);
}

void ExecuteGrapple(){
    Vector3 lowestPoint= new Vector3(transform.position.x,transform.position.y-1f,transform.position.z);

    float grapplePointRelativeYPos= grapplePoint.y-lowestPoint.y;
    float highestPointOnArc=grapplePointRelativeYPos+overshootYaxis;

    if (grapplePointRelativeYPos<0)
    {
        highestPointOnArc=overshootYaxis;
    }
    pm.JumpToPosition(grapplePoint,highestPointOnArc);
    Invoke(nameof(StopGrapple),1f);
}

void StopGrapple(){
    grappling= false;
    grapplingCDtimer=grapplingCD;
    lineRenderer.enabled=false;
}



private Vector3 FindDirectionVector(Vector3 initialCoordinates,Vector3 targetCoordinates){

    Vector3 directionVector= targetCoordinates-initialCoordinates;
    return directionVector;
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


}
