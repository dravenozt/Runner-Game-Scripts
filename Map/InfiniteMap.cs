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
    public GameObject[] gameObjectListWithEmpty;
    public Vector3 locEmptyObject;
    public int[] sayilar;
    
    ObjectPooler objectPooler;
    Vector3 locTile1;
    Vector3 locBaseTile;
    Vector3 locLeftLantern;
    Vector3 locRightLantern;
    Vector3 locPlane;
    int oldInt;
    int newInt;
    public Variables variables;
    public float objectDistance;
    
    

    // Update is called once per frame

    public static InfiniteMap Instance;

    private void Awake() {
        Instance= this;
    }




    private void Start()
    {   
        
        locTile1=Vector3.zero;
        objectPooler= ObjectPooler.Instance;

        if (!variables.tutorialPlayed)
        {
            locEmptyObject = new Vector3(0, 0, 60); /////////////Empty objects starts little bit far
        }
        else{
            locEmptyObject = new Vector3(0, 0, 60);
        }
        
        locPlane=Vector3.zero+ Vector3.down/2;
        locRightLantern=Vector3.zero+ Vector3.forward*16f;
        
        newInt=Random.Range(6,29);


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
        
       


        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();
        SpawnPlane();


        //tutorial oynanmadıysa
        if (!variables.tutorialPlayed)
        {
                    //Teach swipe left-right
        objectPooler.SpawnFromPool("buyukagac",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*30;
        
        objectPooler.SpawnFromPool("pillars",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*30;

        objectPooler.SpawnFromPool("buyukagac2",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*30;
        
        objectPooler.SpawnFromPool("mezar",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*30; //180 mt bitiş


        //Teach swipe up
        objectPooler.SpawnFromPool("coffin",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*30;
        
        objectPooler.SpawnFromPool("emtree",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*30;
        
        objectPooler.SpawnFromPool("rock1",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*30; //270 bitiş


        //Teach swipe down        
        objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*45;

        objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*45;

        objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*45;

        objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*45;

        objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*45;

        objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
        locEmptyObject+=Vector3.forward*45;
        }

        else
        {
            SpawnObstacle();
            SpawnObstacle();
            SpawnObstacle();
            SpawnObstacle();
        }
        

    }

    void Update()
    {   
        //spawn platforms
        if (player.transform.position.z>locTile1.z-60)
        {
            SpawnPlatform();
        }

        if (player.transform.position.z>locBaseTile.z-60)
        {
            SpawnBase();
        }

        if (player.transform.position.z>locLeftLantern.z-60)
        {
            SpawnLeftLantern();
        }

        if (player.transform.position.z>locRightLantern.z-60)
        {
            SpawnRightLantern();
        }

        if (player.transform.position.z>locPlane.z-60)
        {
            SpawnPlane();
        }

        //Spawn
        if (Vector3.Distance(player.transform.position,locEmptyObject)<100f)
        {
            SpawnObstacle();
        }

        


    }

    private void GenerateObstacles()
    {   
        if (Vector3.Distance(player.transform.position,locEmptyObject)<150f)
        {
        CombineObjects();
        CombineObjects();
        CombineObjects();
        }
        
        
        
    }


   

    private void SpawnObstacle(){
        
        //newInt=Random.Range(6,20);
        while (newInt==oldInt)
        {
            newInt=Random.Range(6,29);
        }
        switch (newInt)//19 obje var
        {   
            //barrels
            case 6: 
            objectPooler.SpawnFromPool("barrels",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;
            
            //barrier
            case 7: 
            objectPooler.SpawnFromPool("barrier",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;
            
            //buyukagac2
            case 8: 
            objectPooler.SpawnFromPool("buyukagac2",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //buyukagac
            case 9: 
            objectPooler.SpawnFromPool("buyukagac",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //cart
            case 10: 
            objectPooler.SpawnFromPool("cart",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //coffin
            case 11: 
            objectPooler.SpawnFromPool("coffin",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //emtree
            case 12: 
            objectPooler.SpawnFromPool("emtree",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //hooktree
            case 13: 
            objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //ltasobegi
            case 14: 
            objectPooler.SpawnFromPool("ltasobegi",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //mezar
            case 15: 
            objectPooler.SpawnFromPool("mezar",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //pillars
            case 16: 
            objectPooler.SpawnFromPool("pillars",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //rock1
            case 17: 
            objectPooler.SpawnFromPool("rock1",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //rock2
            case 18: 
            objectPooler.SpawnFromPool("rock2",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //tasnojump
            case 19: 
            objectPooler.SpawnFromPool("tasnojump",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;

            //tree comb 1
            case 20: 
            objectPooler.SpawnFromPool("buyukagac",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            
            objectPooler.SpawnFromPool("buyukagac2",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            break;

            //tree comb 2
            case 21: 
            objectPooler.SpawnFromPool("buyukagac2",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            
            objectPooler.SpawnFromPool("buyukagac",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            break;

            //tree-pillar-tree combinations(1)
            case 22: 
            objectPooler.SpawnFromPool("buyukagac2",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;

            objectPooler.SpawnFromPool("pillars",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            
            objectPooler.SpawnFromPool("buyukagac",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            break;

            //tree-pillar-tree combinations(2)
            case 23: 
            objectPooler.SpawnFromPool("buyukagac",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;

            objectPooler.SpawnFromPool("pillars",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            
            objectPooler.SpawnFromPool("buyukagac2",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            break;
            
            //increase the chances hooktree spawn
            case 24: 
            objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;       
            

            //hooktree - trees combinations(1)
            case 25: 
            objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;

            objectPooler.SpawnFromPool("buyukagac",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            
            objectPooler.SpawnFromPool("buyukagac2",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            break;

            //hooktree - trees combinations(2)
            case 26: 
            objectPooler.SpawnFromPool("hooktree",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;

            objectPooler.SpawnFromPool("buyukagac2",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            
            objectPooler.SpawnFromPool("buyukagac",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;
            break;

            //increase barrels
            case 27: 
            objectPooler.SpawnFromPool("barrels",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;
            
            //increase barrier
            case 28: 
            objectPooler.SpawnFromPool("barrier",locEmptyObject,Quaternion.identity); 
            locEmptyObject+=Vector3.forward*objectDistance;;break;


            
            

            
            default:break;
        }
        oldInt=newInt;
    }

    
    
    private void CombineObjects(){
        
        List<GameObject> gameObjectList= new List<GameObject>();

        foreach (GameObject gameObject in gameObjectListWithEmpty)
        {
            gameObjectList.Add(gameObject);
        }
        

        GameObject emptyObject= Instantiate(gameObjectListWithEmpty[0],locEmptyObject,Quaternion.identity);

        int range= 23;

        for (int i = 1; i < 8; i++){
            int index=Random.Range(1,range);
            
            GameObject spawnedObject= Instantiate(gameObjectList[index],locEmptyObject,Quaternion.identity);
            gameObjectList.RemoveAt(index);
            range--;            
            locEmptyObject+=Vector3.forward*15;
        }
    }






    private void SpawnPlatform()
    {   

        objectPooler.SpawnFromPool("tile1",locTile1,Quaternion.identity);
        locTile1+=Vector3.forward * 4.472136f;

        
        

        
    }

    private void SpawnBase(){


        objectPooler.SpawnFromPool("baseTile",locBaseTile,Quaternion.identity);
        locBaseTile+=Vector3.forward * 15f;
    }

    private void SpawnLeftLantern(){


        objectPooler.SpawnFromPool("leftLamp",locLeftLantern,Quaternion.identity);
        locLeftLantern+=Vector3.forward * 32f;
    
    }

    private void SpawnRightLantern(){


        objectPooler.SpawnFromPool("rightLamp",locRightLantern,Quaternion.identity);
        locRightLantern+=Vector3.forward * 32f;
    
    }
    private void SpawnPlane(){


        objectPooler.SpawnFromPool("plane",locPlane,Quaternion.identity);
        locPlane+=Vector3.forward * 10f;     
           
    }




}
