using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class ShopUI : MonoBehaviour
{   
    public Variables variables;
    public GameObject BuyButton;
    public GameObject selectUI;
    public int characterIndex=0;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        variables.currentCharacterIndex=characterIndex;
    }


    public void Buy(){
        if (variables.survivalScore>=3000)
        {
            
            variables.survivalScore-=3000;

            
            switch (variables.currentCharacterIndex)
            {
                case 1:variables.hasch02=true;selectUI.GetComponent<CharacterSelectUI>().charIndexAndBought[1]=true;break;
                case 2:variables.hasch03=true;selectUI.GetComponent<CharacterSelectUI>().charIndexAndBought[2]=true;break;
                case 3:variables.hasch04=true;selectUI.GetComponent<CharacterSelectUI>().charIndexAndBought[3]=true;break;
                case 4:variables.hasch05=true;selectUI.GetComponent<CharacterSelectUI>().charIndexAndBought[4]=true;break;
                default:break;
            }
            BuyButton.SetActive(false);
        }
    }
    public void RightCharacter(){
        
        
        
        if (characterIndex<4)
        {
            characterIndex+=1;
        }

        else
        {
            characterIndex=0;
        }
    }
    public void LeftCharacter(){
        
        
        if (characterIndex>0)
        {
            characterIndex-=1;
        }
        else
        {
            characterIndex=4;
        }
    }
    public void GoHome(){
        SceneManager.LoadScene("MainMenu");
    }
}
