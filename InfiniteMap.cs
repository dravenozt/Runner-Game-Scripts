using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InfiniteMap : MonoBehaviour
{

    public GameObject player;
    public GameObject tile1;
    public GameObject baseTile;
    public GameObject leftLantern;
    public GameObject rightLantern;
    public GameObject plane;
    private float positionMultiplier=31f;
    public GameObject[] obstacles;
    
    

    // Update is called once per frame


    private void Start()
    {
        GenerateMap();
        



    }



    void Update()
    {

        if (player.transform.position.z > positionMultiplier)
        {   
            
                GenerateMap();
                positionMultiplier += 31f;
            
            
            
        }

        
        DestroyBehind();

    }

    private void DestroyBehind()
    {   
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.z < player.transform.position.z - 30)
            {
                GameObject.Destroy(obstacle);

            }
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
        //SpawnBase();
        //SpawnBase();

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
        if (Vector3.Distance(player.transform.position,tile1.transform.position)<100f){
        Vector3 loctile1 = new Vector3(tile1.transform.position.x, tile1.transform.position.y, tile1.transform.position.z);
        tile1 = Instantiate(tile1, loctile1 + Vector3.forward * 4.472136f, Quaternion.identity);
        }
        
        
        //obstacles.Append(tile1);
        
    }

    private void SpawnBase(){
        
        if (Vector3.Distance(player.transform.position,baseTile.transform.position)<60f)
        {
            Vector3 locBaseTile= new Vector3(baseTile.transform.position.x, baseTile.transform.position.y, baseTile.transform.position.z);
            baseTile = Instantiate(baseTile, locBaseTile + Vector3.forward * 15f, Quaternion.identity);
        }

        
        //obstacles.Append(baseTile);
    }

    private void SpawnLeftLantern(){
        if (Vector3.Distance(player.transform.position,leftLantern.transform.position)<100f){
            Vector3 locLeftLantern= new Vector3(leftLantern.transform.position.x, leftLantern.transform.position.y, leftLantern.transform.position.z);
            leftLantern = Instantiate(leftLantern, locLeftLantern + Vector3.forward * 16f, Quaternion.identity);
        }

        //obstacles.Append(leftLantern);
    
    }

    private void SpawnRightLantern(){
        if (Vector3.Distance(player.transform.position,rightLantern.transform.position)<100f){
        Vector3 locRightLantern= new Vector3(rightLantern.transform.position.x, rightLantern.transform.position.y, rightLantern.transform.position.z);
        rightLantern = Instantiate(rightLantern, locRightLantern + Vector3.forward * 16f, Quaternion.identity);
        }

        //obstacles.Append(rightLantern);
    
    }
    private void SpawnPlane(){
        if (Vector3.Distance(player.transform.position,plane.transform.position)<100f){
        Vector3 locPlane= new Vector3(plane.transform.position.x, plane.transform.position.y, plane.transform.position.z);
        plane = Instantiate(plane, locPlane + Vector3.forward * 10f, Quaternion.identity);
        }

        //obstacles.Append(plane);     
           
    }




}
