using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRes : MonoBehaviour
{   
    public static ScreenRes instance;
    private void Start() {
        
        
        if (instance!=null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance=this;
            Screen.SetResolution(Screen.currentResolution.width/2,Screen.currentResolution.height/2,true);
        }

        DontDestroyOnLoad(gameObject);
    }
}
