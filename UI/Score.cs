using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject score;
    [HideInInspector]public float survivalScore;
    public GameObject player;
    CharacterController controller;
    
    
    private void Start() {
        survivalScore=0;
        controller=player.GetComponent<CharacterController>();
    }
    private void Update() {


        if (controller.enabled)
        {   
            survivalScore=Mathf.Ceil(player.transform.position.z/10);
            //survivalScore+=Time.deltaTime;
        }
        
        score.GetComponent<TMP_Text>().text= survivalScore.ToString("F0");//Time.time.ToString();
        
        
        
    }


}
