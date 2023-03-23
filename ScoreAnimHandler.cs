using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimHandler : MonoBehaviour
{   
    public Animator starAnimator;
    Animator scoreAnimator;
    // Start is called before the first frame update
    void Start()
    {
        starAnimator=GameObject.FindGameObjectWithTag("StarUI").GetComponent<Animator>();
        scoreAnimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(starAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="StarCollectAnim"){
            scoreAnimator.SetBool("canPlayScoreAnim",true);
            
        }
        if (scoreAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="ScoreUIAnim")
        {
            starAnimator.SetBool("canPlay",false);
        }
        if(starAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="StarDisabled"){
            scoreAnimator.SetBool("canPlayScoreAnim",false);
            
        }
        
    }
}
