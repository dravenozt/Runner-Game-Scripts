using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Variables")]
public class Variables : ScriptableObject
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


    public void SavePlayer(){
        SaveSystem.SavePlayer(this);

    }


    public void LoadPlayer(){
        PlayerData data= SaveSystem.LoadPlayer();

        isSoundEnabled= data.isSoundEnabled;
        survivalScore= data.survivalScore;

        hasch01= data.hasch01;
        hasch02=data.hasch02;
        hasch03=data.hasch03;
        hasch04= data.hasch04;
        hasch05= data.hasch05;

        currentCharacterIndex= data.currentCharacterIndex;

        tutorialPlayed=data.tutorialPlayed;
    }
    
    

}
