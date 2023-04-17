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
    public Animator starAnimator;
    public float starReferencePoints=0;
    
    
    private void Start() {
        survivalScore=0;
        controller=player.GetComponent<CharacterController>();
        starAnimator=GameObject.FindGameObjectWithTag("StarUI").GetComponent<Animator>();
    }
    private void Update() {


        if (controller.enabled)
        {   


            //olmuyo çünkü sürekli player transforma kayıyosun
            survivalScore=Mathf.Ceil(player.transform.position.z/10)+starReferencePoints;
            //survivalScore+=Time.deltaTime;

            
        }
        
        score.GetComponent<TMP_Text>().text= survivalScore.ToString("F0");//Time.time.ToString();
        
        
        
    }


}
