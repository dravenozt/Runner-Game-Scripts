using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{   

    //Pool o each gameobject, like leftlamp or basetile etc
    [System.Serializable]
    public class Pool{
        public string tag;
        public GameObject prefab;
        public int size;
    }


    #region Singleton
    public static ObjectPooler Instance;

    private void Awake() {
        Instance= this;
    }

    #endregion

    public InfiniteMap infiniteMap; 

    //list of pools 
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> pooldictionary;
    // Start is called before the first frame update
    void Start()
    {   
        infiniteMap=InfiniteMap.Instance;


        pooldictionary= new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {   
            //pool of objects
            Queue<GameObject> objectPool= new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {   
                //instantiate gameobject
                GameObject obj= Instantiate(pool.prefab);
                obj.SetActive(false);

                //add it to queue
                objectPool.Enqueue(obj);
            }

            pooldictionary.Add(pool.tag,objectPool);
        }
    }



    //A spawn function run like instantiate but uses objectpools
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation){

        //pull the gameobject from pool
        GameObject objectToSpawn= pooldictionary[tag].Dequeue();
        
        //put the object to world
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position= position;
        objectToSpawn.transform.rotation= rotation;

        //push it to queue again
        pooldictionary[tag].Enqueue(objectToSpawn);

        //set the location
        

        return objectToSpawn;
    }

}
