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
    int isSoundEnabled=1;//1 => true, 0 => false
    


    private void Start() {
        
        variables.LoadPlayer();/////////////////////////////////////////////////// buraya bakkkkkk loadluyoruz
        
        isSoundEnabled=PlayerPrefs.GetInt(nameof(isSoundEnabled));
        //soundOn.SetActive(variables.isSoundEnabled);
        //soundOff.SetActive(!variables.isSoundEnabled);
        
        
        if (isSoundEnabled==0)
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);//sound off button is active cuz sound is off
            AudioListener.pause=true;

        }
        else
        {
            soundOn.SetActive(true);//sound on button is active cuz sound is on
            soundOff.SetActive(false);
            AudioListener.pause=false;
        }
        
    }

    private void Update() {
        //cam.GetComponent<AudioListener>().enabled=variables.isSoundEnabled;

        Debug.Log("issound enabled değeri: " + isSoundEnabled);

        /*
        if (isSoundEnabled==1)
        {
          //cam.GetComponent<AudioListener>().enabled=true;,
          AudioListener.pause=false;  
        }

        else{
            //cam.GetComponent<AudioListener>().enabled=false;

        }*/
    }

    public void Restart(){
        SceneManager.LoadScene("Runner");
    }

    public void StartGame(){
        //SceneManager.LoadScene("Runner");
        

        if (variables.tutorialPlayed)
        {
            LoadLevelAsync(1);
        }
        else
        {
            LoadLevelAsync(3);
        }
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


    //if you click this it will pause the sound
    public void SoundOn(){
        //variables.isSoundEnabled=false;
        //cam.GetComponent<AudioListener>().=false;
        soundOff.SetActive(true);
        soundOn.SetActive(false);

        variables.SavePlayer();//////////////////////////////////////////////////////////////////////////////////////////
        PlayerPrefs.SetInt(nameof(isSoundEnabled),0);
        isSoundEnabled=PlayerPrefs.GetInt(nameof(isSoundEnabled));
        AudioListener.pause=true;
        
    }

    //if you click this it will unpause the sound
    public void SoundOff(){
        //variables.isSoundEnabled=true;
        //cam.GetComponent<AudioListener>().enabled=true;
        soundOn.SetActive(true);
        soundOff.SetActive(false);

        variables.SavePlayer();////////////////////////////////////////////////////////////////////////////////////////////////
        PlayerPrefs.SetInt(nameof(isSoundEnabled),1);
        isSoundEnabled=PlayerPrefs.GetInt(nameof(isSoundEnabled));
        AudioListener.pause=false;
    }

    public void GoStore(){
        SceneManager.LoadScene("Store");
    }
    

    public void LoadLevelAsync(int sceneIndex){
        StartCoroutine(LoadAscynchron(sceneIndex));
    }
    IEnumerator LoadAscynchron(int sceneIndex){
        AsyncOperation operation= SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }

}
