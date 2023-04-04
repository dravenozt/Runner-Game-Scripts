using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Popups : MonoBehaviour
{
    public GameObject sideSwipePopUp;
    public GameObject swipeUpPopUp;
    public GameObject swipeDownPopUp;
    public Variables variables;


    private void Update() {
        if (transform.position.z>45)
        {
            sideSwipePopUp.SetActive(true);

        }

        if (transform.position.z>165)
        {
            swipeUpPopUp.SetActive(true);
            sideSwipePopUp.SetActive(false);
        }

        if (transform.position.z>265)
        {
            swipeDownPopUp.SetActive(true);
            swipeUpPopUp.SetActive(false);
        }

        if (transform.position.z>380)
        {
            swipeDownPopUp.SetActive(false);
            variables.tutorialPlayed=true;
        }
    }
}
