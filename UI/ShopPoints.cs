using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPoints : MonoBehaviour
{   
    public Variables variables;
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text= GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text= variables.survivalScore.ToString();
    }
}
