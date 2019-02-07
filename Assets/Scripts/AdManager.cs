using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdManager : MonoBehaviour {

    public static AdManager instance;

    private string store_id = "3032681";
    private string video_ad = "video";
    private string banner_ad = "Banner";

    private void Awake() {
        SetUpAdSingleton();
    }

    private void SetUpAdSingleton() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        Monetization.Initialize(store_id, false);
    }

    public void ShowVideoAd() {
        if (Monetization.IsReady(video_ad)) {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(video_ad) as ShowAdPlacementContent;
            if(ad != null) {
                ad.Show();
            }
        }
    }
}
