using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private Text level;
    [SerializeField]
    private Text life;
    [SerializeField]
    private Text gemText;
    [SerializeField]
    private Toast toast;
    [SerializeField]
    private Sprite soundOn;
    [SerializeField]
    private Sprite soundOff;
    [SerializeField]
    private Image sound;
    [SerializeField]
    private AudioSource buttonSFX;

    public Dialog pauseDialog;
    public LCDialog levelCompleteDialog;
    public Dialog gameOverDialog;
    public Dialog infoDialog;
    public Dialog livesPurchaseDialog;

    void Start()
    {
        SetLevelText(LevelManager.level);
        gemText.text = Attributes.gems.ToString();
        SetLifeText();
        if (Attributes.lives == 3) ShowInfoDialog();
        sound.sprite = (Attributes.soundOn) ? soundOn : soundOff;
        buttonSFX.mute = !Attributes.soundOn;
    }

    public void SetLevelText(int level)
    {
        this.level.text = LevelManager.LevelToString(level);
    }

    public void SetLifeText()
    {
        life.text = Attributes.lives.ToString();
    }

    public void ToggleInfo(Toggle toggle)
    {
        Attributes.settingShowInfo = !toggle.isOn;
    }

    public void ShowToast(string message)
    {
        toast.Show(message);
    }

    void ShowInfoDialog()
    {
        if (!GameManager.isPaidLives)
        {
            return;
        }
        else
        {
            GameManager.isPaidLives = false;
        }
        if (Attributes.settingShowInfo)
        {
            GameManager.instance.SetState(GameManager.State.PAUSE);
            infoDialog.Open();
        }
    }
    
    public void ToggleSound()
    {
        Attributes.soundOn = !Attributes.soundOn;
        SoundManager.instance.ToggleSound(Attributes.soundOn);
        sound.sprite = (Attributes.soundOn) ? soundOn : soundOff;
        buttonSFX.mute = !Attributes.soundOn;
    }

    public void PlayButtonSFX()
    {
        if (Attributes.soundOn) buttonSFX.Play();
    }

    public void QuitWithConfirm(Dialog confirm)
    {
        if (Attributes.lives >= 3)
            GameManager.instance.BackToMainMenu();
        else
            confirm.Open();
    }
}
