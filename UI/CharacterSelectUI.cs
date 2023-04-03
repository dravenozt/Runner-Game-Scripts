using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterSelectUI : MonoBehaviour
{   
    public Variables variables;
    public GameObject[] allCharacters;
    public GameObject currentCharacter;
    public ShopUI shopUI;
    public GameObject buyButton;
    public Dictionary<int,bool> charIndexAndBought=new Dictionary<int, bool>();
    
    // Start is called before the first frame update
    void Start()
    {   
        
        charIndexAndBought.Add(0,variables.hasch01);
        charIndexAndBought.Add(1,variables.hasch02);
        charIndexAndBought.Add(2,variables.hasch03);
        charIndexAndBought.Add(3,variables.hasch04);
        charIndexAndBought.Add(4,variables.hasch05);
    }

    // Update is called once per frame
    void Update()
    {   
        currentCharacter= allCharacters[shopUI.characterIndex];
        
        //Debug.Log(charIndexAndBought.ContainsKey(shopUI.characterIndex));
        
        buyButton.SetActive(!charIndexAndBought.Values.ToArray()[variables.currentCharacterIndex]);//.(variables.currentCharacterIndex));
        foreach (GameObject character in allCharacters)
        {
            if (character!=currentCharacter)
            {   
                currentCharacter.SetActive(true);
                character.SetActive(false);
            }
        }
    }
}
