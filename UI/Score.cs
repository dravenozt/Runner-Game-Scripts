using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject score;
    float timePassed;
    public GameObject player;
    private void Start() {
        timePassed=0;
    }
    private void Update() {


        if (player.GetComponent<CharacterController>().enabled)
        {
            timePassed+=Time.deltaTime;
        }
        
        score.GetComponent<TMP_Text>().text= timePassed.ToString("F2");//Time.time.ToString();
        
    }


}
