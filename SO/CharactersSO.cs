using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Characters")]

public class CharactersSO : ScriptableObject
{   
    
    public List<GameObject> allCharacters= new List<GameObject>();
}
