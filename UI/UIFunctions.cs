using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    public GameObject pause;
    public GameObject unPause;
    public Camera cam;
    public GameObject soundOn;
    public GameObject soundOff;
    public Variables variables;


    private void Start() {
        soundOn.SetActive(variables.isSoundEnabled);
        soundOff.SetActive(!variables.isSoundEnabled);
        
        
    }

    private void Update() {
        cam.GetComponent<AudioListener>().enabled=variables.isSoundEnabled;
    }

    public void Restart(){
        SceneManager.LoadScene("Runner");
    }

    public void StartGame(){
        SceneManager.LoadScene("Runner");
    }
    public void Pause(){
        Time.timeScale=0;
        cam.GetComponent<AudioSource>().Pause();
        unPause.SetActive(true);
        pause.SetActive(false);

    }
    public void Unpause(){
        Time.timeScale=1;
        cam.GetComponent<AudioSource>().UnPause();
        pause.SetActive(true);
        unPause.SetActive(false);
    }

    public void GoHome(){
        SceneManager.LoadScene("MainMenu");
    }


    public void SoundOn(){
        variables.isSoundEnabled=false;
        //cam.GetComponent<AudioListener>().enabled=variables.isSoundEnabled;
        soundOff.SetActive(true);
        soundOn.SetActive(false);
        
    }

    public void SoundOff(){
        variables.isSoundEnabled=true;
        //cam.GetComponent<AudioListener>().enabled=variables.isSoundEnabled;
        soundOn.SetActive(true);
        soundOff.SetActive(false);
    }

    


}
