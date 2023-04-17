using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class AdManager : MonoBehaviour
{   
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712"; ////// ad id for test (interstitial)

    public bool reklamverimmi;
    public GameObject player;
    public CharacterController controller;
    public int deathCount=0;
    bool canCount;



    void Start()
    {   
        canCount=true;
        deathCount=PlayerPrefs.GetInt(nameof(deathCount),deathCount);
        controller= player.GetComponent<CharacterController>();
        MobileAds.Initialize(initStatus=>{});

        //this.RequestBanner();
        this.LoadInterstitialAd();
        
        
    }


    private void Update() {
        
        Debug.Log("Death count: " + deathCount);
        if (!controller.enabled&&canCount)
        {
            deathCount++;
            PlayerPrefs.SetInt(nameof(deathCount),deathCount);
            
            canCount=false;

            if (deathCount>=3)
            {
                //this.ShowAd();
                StartCoroutine(nameof(ShowAdCoroutine));
                deathCount=0;
                PlayerPrefs.SetInt(nameof(deathCount),deathCount);
            }
        }
        //Debug.Log(interstitialAd.CanShowAd());
        
        if (reklamverimmi)
        {
            this.ShowAd();
            reklamverimmi=false;
        }
    }





public void LoadInterstitialAd()
  {
      // Clean up the old ad before loading a new one.
      if (interstitialAd != null)
      {
            interstitialAd.Destroy();
            interstitialAd = null;
      }

      Debug.Log("Loading the interstitial ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest.Builder()
              .AddKeyword("unity-admob-sample")
              .Build();

      // send the request to load the ad.
      InterstitialAd.Load(_adUnitId, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              interstitialAd = ad;
          });
  }


//Show interstitial ad
  public void ShowAd()
{
    if (interstitialAd != null && interstitialAd.CanShowAd())
    {
        Debug.Log("Showing interstitial ad.");
        interstitialAd.Show();
    }
    else
    {
        Debug.LogError("Interstitial ad is not ready yet.");
    }
}


    
    void RequestBanner(){
        string adID= "ca-app-pub-3940256099942544/6300978111";

        this.bannerView= new BannerView(adID,AdSize.Banner,AdPosition.Bottom);
        AdRequest request= new AdRequest.Builder().Build();


        this.bannerView.LoadAd(request);
    }


    IEnumerator ShowAdCoroutine(){
        yield return new WaitForSeconds(.3f);
        this.ShowAd();

    }
}
