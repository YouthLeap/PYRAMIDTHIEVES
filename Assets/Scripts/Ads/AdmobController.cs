using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobController : MonoBehaviour
{
    public static AdmobController instance;

#if UNITY_ANDROID
    public static string BANNER_ID = "ca-app-pub-8688439802773899/5934862360";
    public static string INTER_ID = "ca-app-pub-8688439802773899/8888328764";
#elif UNITY_IOS
    public static string BANNER_ID = "ca-app-pub-8688439802773899/2841795161";
    public static string INTER_ID = "ca-app-pub-8688439802773899/4318528369 ";
#endif
    InterstitialAd interstitial = new InterstitialAd(INTER_ID);
    BannerView bannerView;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        RequestBanner();
        RequestInterstitial();

    }

    public static void CreateInstance()
    {
        if (instance == null)
        {
            instance = (new GameObject("Admob")).AddComponent<AdmobController>();
        }
    }


    private void RequestBanner()
    {
        bannerView = new BannerView(BANNER_ID, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        bannerView.Hide();
    }

    private void RequestInterstitial()
    {
        interstitial = new InterstitialAd(INTER_ID);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.OnAdClosed += Interstitial_OnAdClosed;
        interstitial.LoadAd(request);
    }

    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        RequestInterstitial();
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
            interstitial.Show();
    }

    public void ShowBanner()
    {
        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }
}
