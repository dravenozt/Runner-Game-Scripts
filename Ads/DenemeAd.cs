using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class DenemeAd : MonoBehaviour
{   
    BannerView bannerView;
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus=>{});
        this.RequestBanner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    void RequestBanner(){
        string adID= "	ca-app-pub-3940256099942544/6300978111";

        this.bannerView= new BannerView(adID,AdSize.Banner,AdPosition.Bottom);
        AdRequest request= new AdRequest.Builder().Build();


        this.bannerView.LoadAd(request);
    }
}
