using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    [SerializeField]
    private Text level;
    [SerializeField]
    private Text gemMachineText;
    [SerializeField]
    private Text gemText;
    [SerializeField]
    private GameObject gemImage;
    [SerializeField]
    private GUIAnimationEvent takeGemEvent;
    [SerializeField]
    private GameObject openShopReminder;
    [SerializeField]
    private BannerDialog banner;
    [SerializeField]
    private AudioSource sfx;
    [SerializeField]
    private AudioSource bgm;
    [SerializeField]
    private AudioClip purchaseSFX;
    [SerializeField]
    private Dialog shopDialog;
    [SerializeField]
    private Tab gemTab;
    private System.DateTime targetTime;
    private bool gemReady = false;
    private bool remindGem = true;
    private int gemCount = 0;

    private const int maxGems = 5;
    private static bool isShowTutorial;

    void Awake()
    {
        instance = this;
        isShowTutorial = PlayerPrefs.GetInt(Strings.SHOW_MENU_TUTORIAL, Strings.ON) == Strings.ON;
        PlayerPrefs.SetInt(Strings.SHOW_MENU_TUTORIAL, Strings.OFF);
        ShowBanner();
        //if (GoogleAnalyticsV4.instance == null) Instantiate(googleAnalyticsPrefab);
        AdmobController.CreateInstance();
    }
    void Start()
    {
        level.text = string.Format("Level {0}", LevelManager.LevelToString(Attributes.highest_level));
        targetTime = Attributes.diamonMachineTime;
        gemReady = targetTime.Subtract(System.DateTime.Now).TotalSeconds < 0;
        gemImage.SetActive(gemReady);
        UpdateGemText();
        if (Attributes.soundOn)
        {
            bgm.Play();
        }
        bgm.mute = !Attributes.soundOn;

        if (isShowTutorial) TakeGemRemind();
        GetComponent<AudioSource>().mute = !Attributes.soundOn;
        if (GoogleAnalyticsV4.instance != null) GoogleAnalyticsV4.instance.LogScreen(Scenes.MAIN_MENU);
    }

    void Update()
    {
        gemMachineText.text = GetGemText();
    }

    public void StartGame()
    {
        Debug.Log("Show tuts" + isShowTutorial);
        if (gemReady && remindGem)
        {
            remindGem = false;
            TakeGemRemind();
            return;
        }
        if (Attributes.gems > 0)
        {
            LevelManager.PlayLevel(Attributes.highest_level);
        }
        else
        {
            OpenShop();
            OpenGemTab();
        }
        isShowTutorial = false;
    }

    public void MapSelect()
    {
        if (gemReady && remindGem)
        {
            remindGem = false;
            TakeGemRemind();
            return;
        }
        isShowTutorial = false;
        Scenes.Load(Scenes.MAP_SCENE);
    }
    
    public void TakeGem()
    {
        if (gemReady)
        {
            gemReady = false;
            gemImage.SetActive(false);
            if (gemCount == 5)
                Attributes.diamonMachineTime = System.DateTime.Now.AddHours(maxGems);
            else
                Attributes.diamonMachineTime = Attributes.diamonMachineTime.AddHours(gemCount);
            targetTime = Attributes.diamonMachineTime;
            Attributes.gems += gemCount;
            UpdateGemText();
            if (isShowTutorial)
            {
                takeGemEvent.gameObject.SetActive(false);
                openShopReminder.SetActive(true);
            }
        }
    }

    private string GetGemText()
    {
        System.TimeSpan span = targetTime.Subtract(System.DateTime.Now);
        gemReady = span.Hours < maxGems - 1;
        gemCount = maxGems - span.Hours - ((span.Minutes > 0 || span.Seconds > 0) ? 1 : 0);
        if (gemCount > maxGems) gemCount = maxGems;
        if (gemReady)
        {
            gemImage.SetActive(gemReady);
            return "Get " + gemCount;
        }
        return string.Format("{0}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
    }

    public void OpenGemTab()
    {
        gemTab.Active();
    }

    public void UpdateGemText()
    {
        gemText.text = Attributes.gems.ToString();
    }


    void OnEnable()
    {
        InAppPurchase.onPurchasePack += OnPurchaseSucess;
        InAppPurchase.onPurchaseFail += OnPurchaseFail;
    }
    void OnDisable()
    {
        InAppPurchase.onPurchasePack -= OnPurchaseSucess;
        InAppPurchase.onPurchaseFail -= OnPurchaseFail;
    }

    public void PlayPurchasedSFX()
    {
        if (Attributes.soundOn) sfx.PlayOneShot(purchaseSFX);
    }

    void OnPurchaseSucess()
    {

    }
    void OnPurchaseFail()
    {

    }

    private void ShowBanner()
    {
        int r = Random.Range(0, 100);
        if (r < 30)
            StartCoroutine(LoadBannerFromUrl());
    }

    IEnumerator LoadBannerFromUrl()
    {
        Debug.Log("Show banner");
        string bannerUrl = Strings.BANNER_URL;
        WWW www = new WWW(bannerUrl);
        yield return www;
        JsonBanner jsonbanner = JsonUtility.FromJson<JsonBanner>(www.text);
        if (jsonbanner != null && jsonbanner.error == JsonBanner.ERROR_SUCCESS)
        {
            string url = jsonbanner.url_store;
            www = new WWW(jsonbanner.content);
            yield return www;
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            banner.Load(url, sprite);
        }
    }

    public void ShowAdmobBanner(bool isShow)
    {
        if (isShow)
            AdmobController.instance.ShowBanner();
        else
            AdmobController.instance.HideBanner();
    }

    public void OpenShop()
    {
        shopDialog.Open();
        if (isShowTutorial)
        {
            OpenGemTab();
            openShopReminder.SetActive(false);
            isShowTutorial = false;
        }
        if (GoogleAnalyticsV4.instance != null)
            GoogleAnalyticsV4.instance.LogEvent(new EventHitBuilder().SetEventCategory(Scenes.MAIN_MENU)
                .SetEventAction("OpenShop")
                .SetEventLabel("Open Shop"));
    }

    class JsonBanner
    {
        public string result;
        public int error;
        public string url_store;
        public string content;

        public const int ERROR_SUCCESS = 0;
    }
    public void UpdateScrollViewState(ScrollRect scroll, GameObject backButton, GameObject nextButton)
    {
        backButton.gameObject.SetActive(scroll.horizontalNormalizedPosition == 0);
        nextButton.gameObject.SetActive(scroll.horizontalNormalizedPosition == 1);
    }

    void TakeGemRemind()
    {
        if (!isShowTutorial) takeGemEvent.onCloseEvt = () => { takeGemEvent.gameObject.SetActive(false); };
        takeGemEvent.gameObject.SetActive(true);
    }    
}