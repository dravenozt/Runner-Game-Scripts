using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData 
{

    public bool isSoundEnabled;
    public int survivalScore;
    public bool hasch01=true;
    public bool hasch02;
    public bool hasch03;
    public bool hasch04;
    public bool hasch05;
    public int currentCharacterIndex;
    public bool tutorialPlayed;


    public PlayerData(Variables variables){

        isSoundEnabled=variables.isSoundEnabled;
        survivalScore=variables.survivalScore;
        hasch01=variables.hasch01;
        hasch02=variables.hasch02;
        hasch03=variables.hasch03;
        hasch04=variables.hasch04;
        hasch05=variables.hasch05;
        currentCharacterIndex=variables.currentCharacterIndex;
        tutorialPlayed=variables.tutorialPlayed;

    }


}
