using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{

    public GameObject player;
    public GameObject tile1;
    public GameObject baseTile;
    public GameObject leftLantern;
    public GameObject rightLantern;
    public GameObject plane;
    private float positionMultiplier=31f;
    
    

    // Update is called once per frame


    private void Start()
    {
        GenerateMap();

    }



    void Update()
    {
        if (player.transform.position.z>positionMultiplier)
        {
            GenerateMap();
            positionMultiplier +=31f;
        }

         

    }

    private void GenerateMap()
    {
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();
        SpawnPlatform();

        SpawnBase();
        SpawnBase();
        SpawnBase();
        SpawnBase();
        SpawnBase();
        SpawnBase();

        SpawnLanterns();
        SpawnLanterns();
        SpawnLanterns();

        
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        
        
    }

    private void SpawnLanterns()
    {
        SpawnLeftLantern();
        SpawnRightLantern();
    }

    private void SpawnPlatform()
    {
        Vector3 loctile1 = new Vector3(tile1.transform.position.x, tile1.transform.position.y, tile1.transform.position.z);
        tile1 = Instantiate(tile1, loctile1 + Vector3.forward * 4.472136f, Quaternion.identity);
    }

    private void SpawnBase(){
        Vector3 locBaseTile= new Vector3(baseTile.transform.position.x, baseTile.transform.position.y, baseTile.transform.position.z);
        baseTile = Instantiate(baseTile, locBaseTile + Vector3.forward * 15f, Quaternion.identity);
    }

    private void SpawnLeftLantern(){
        Vector3 locLeftLantern= new Vector3(leftLantern.transform.position.x, leftLantern.transform.position.y, leftLantern.transform.position.z);
        leftLantern = Instantiate(leftLantern, locLeftLantern + Vector3.forward * 16f, Quaternion.identity);
    
    }

    private void SpawnRightLantern(){
        Vector3 locRightLantern= new Vector3(rightLantern.transform.position.x, rightLantern.transform.position.y, rightLantern.transform.position.z);
        rightLantern = Instantiate(rightLantern, locRightLantern + Vector3.forward * 16f, Quaternion.identity);
    
    }
    private void SpawnPlane(){
        Vector3 locPlane= new Vector3(plane.transform.position.x, plane.transform.position.y, plane.transform.position.z);
        plane = Instantiate(plane, locPlane + Vector3.forward * 10f, Quaternion.identity);        
    }




}
