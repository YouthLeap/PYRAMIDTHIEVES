using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CharacterEntry : MonoBehaviour
{
    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private int cost;
    [SerializeField]
    private Text costText;

    [SerializeField]
    private CharacterGroup group;

    void Start()
    {
        CheckState();
        costText.text = cost.ToString();
        if (cost == 0) costText.text = "0";
        //PlayerPrefs.SetInt(Strings.PURCHASED_PLAYER + "0", Strings.OFF);
    }

    public void Buy()
    {
        if (Attributes.GetPlayer(id))
        {
            buttonText.text = "Select";
            if (Attributes.selectedPlayer != id)
            {
                group.Select(this);
                if (GoogleAnalyticsV4.instance != null)
                    GoogleAnalyticsV4.instance.LogEvent(new EventHitBuilder().SetEventCategory("Player")
                    .SetEventAction("Select player " + id));
            }
        }
        else
        {
            MainMenu.instance.PlayPurchasedSFX();
            int gems = Attributes.gems;
            if (gems >= cost)
            {
                gems -= cost;
                MainMenu.instance.UpdateGemText();

                Attributes.PurchasedPlayer(id);
                if (GoogleAnalyticsV4.instance != null)
                    GoogleAnalyticsV4.instance.LogEvent(new EventHitBuilder().SetEventCategory("Player")
.SetEventAction("Purchase player " + id));
                Attributes.gems = gems;
                CheckState();
                MainMenu.instance.UpdateGemText();
            }
            else
            {
                MainMenu.instance.OpenGemTab();
            }
        }
    }

    void CheckState()
    {
        if (Attributes.GetPlayer(id))
        {
            buttonText.text = "Select";
            if (Attributes.GetPlayer(id)) costText.gameObject.SetActive(false);
            if (Attributes.selectedPlayer == id) group.Select(this);
        }
    }

    public void Select(bool select)
    {
        buyButton.interactable = !select;
        if (select) Attributes.selectedPlayer = id;
    }
}
