using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{   
    public Mesh[] meshes;
    public Material[] materials;
    public Variables variables;
    public GameObject body;
    // Start is called before the first frame update
    void Start()
    {
        switch (variables.currentCharacterIndex)
        {
            case 0: body.GetComponent<SkinnedMeshRenderer>().sharedMesh=meshes[variables.currentCharacterIndex];
                    body.GetComponent<SkinnedMeshRenderer>().material=materials[variables.currentCharacterIndex];
                    break;

            case 1: if (variables.hasch02)
                    {
                        body.GetComponent<SkinnedMeshRenderer>().sharedMesh=meshes[variables.currentCharacterIndex];
                        body.GetComponent<SkinnedMeshRenderer>().material=materials[variables.currentCharacterIndex];
                    }

                    else
                    {
                        body.GetComponent<SkinnedMeshRenderer>().sharedMesh=meshes[0];
                        body.GetComponent<SkinnedMeshRenderer>().material=materials[0];
                    }
                    break;

            case 2: if (variables.hasch03)
                    {
                        body.GetComponent<SkinnedMeshRenderer>().sharedMesh=meshes[variables.currentCharacterIndex];
                        body.GetComponent<SkinnedMeshRenderer>().material=materials[variables.currentCharacterIndex];
                    }

                    else
                    {
                        body.GetComponent<SkinnedMeshRenderer>().sharedMesh=meshes[0];
                        body.GetComponent<SkinnedMeshRenderer>().material=materials[0];
                    }
                    break;


            case 3: if (variables.hasch04)
                    {
                        body.GetComponent<SkinnedMeshRenderer>().sharedMesh=meshes[variables.currentCharacterIndex];
                        body.GetComponent<SkinnedMeshRenderer>().material=materials[variables.currentCharacterIndex];
                    }

                    else
                    {
                        body.GetComponent<SkinnedMeshRenderer>().sharedMesh=meshes[0];
                        body.GetComponent<SkinnedMeshRenderer>().material=materials[0];
                    }
                    break;

            case 4: if (variables.hasch05)
                    {
                        body.GetComponent<SkinnedMeshRenderer>().sharedMesh=meshes[variables.currentCharacterIndex];
                        body.GetComponent<SkinnedMeshRenderer>().material=materials[variables.currentCharacterIndex];
                    }

                    else
                    {
                        body.GetComponent<SkinnedMeshRenderer>().sharedMesh=meshes[0];
                        body.GetComponent<SkinnedMeshRenderer>().material=materials[0];
                    }
                    break;
            default:break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
