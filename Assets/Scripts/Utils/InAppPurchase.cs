using UnityEngine;
using System.Collections;
using OnePF;
using System.Collections.Generic;
public class InAppPurchase : MonoBehaviour
{
    bool _isInitialized = false;
    Inventory _inventory = null;

    [SerializeField]
    string diamondPack1 = "diamond_pack_1";
    [SerializeField]
    string diamondPack2 = "diamond_pack_2";
    [SerializeField]
    string diamondPack3 = "diamond_pack_3";
    [SerializeField]
    string googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgUBNeqCZMx7PmGjcVafNG2xLjyd6P2z02sRp/HRDrzydXG3Y05mrgaDZ7O15qAFJNLK3r95sez8tGYdLM+WmQAp8wWWasntoEdiqGgPWuDXVPlrBgVarOAD2M7t/BQ5qzO5QheLFeVjPc7UJD5Givb0GFGkO/hjffqzwsYqJtMR7kf6KeAMf8qSZxjEPLdhFaLx7hSZMM115rSGGMH7GTmnAuOIOjyNF08ATjpvo+MqdBm8gkNMai/qktkrxUM6+DqvBu+PmLPGgD4URKyPfIzO9fg7WO5M/iMEpjB6DtbrizvKDIXA107hNBdBEZZft4HmTNgrjdj++Vp0/znGFvwIDAQAB";

    public delegate void OnPurchasePack();
    public static event OnPurchasePack onPurchasePack;

    public delegate void OnPurchaseFail();
    public static event OnPurchaseFail onPurchaseFail;

    public delegate void OnRestore();
    public static event OnRestore onRestore;
    // Use this for initialization
    void Start()
    {
        print("init inapp");
        OpenIAB.mapSku(diamondPack1, OpenIAB_Android.STORE_GOOGLE, diamondPack1);
        OpenIAB.mapSku(diamondPack2, OpenIAB_Android.STORE_GOOGLE, diamondPack2);
        OpenIAB.mapSku(diamondPack3, OpenIAB_Android.STORE_GOOGLE, diamondPack3);

        initInApp();

    }
    public void buyPack1()
    {
        OpenIAB.purchaseProduct(diamondPack1);
    }
    public void buyPack2()
    {
        OpenIAB.purchaseProduct(diamondPack2);
    }
    public void buyPack3()
    {
        OpenIAB.purchaseProduct(diamondPack3);
    }
    public void querryItem()
    {

        OpenIAB.queryInventory(new string[] { diamondPack1, diamondPack2, diamondPack3 });
    }
    void initInApp()
    {
        //var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgUBNeqCZMx7PmGjcVafNG2xLjyd6P2z02sRp/HRDrzydXG3Y05mrgaDZ7O15qAFJNLK3r95sez8tGYdLM+WmQAp8wWWasntoEdiqGgPWuDXVPlrBgVarOAD2M7t/BQ5qzO5QheLFeVjPc7UJD5Givb0GFGkO/hjffqzwsYqJtMR7kf6KeAMf8qSZxjEPLdhFaLx7hSZMM115rSGGMH7GTmnAuOIOjyNF08ATjpvo+MqdBm8gkNMai/qktkrxUM6+DqvBu+PmLPGgD4URKyPfIzO9fg7WO5M/iMEpjB6DtbrizvKDIXA107hNBdBEZZft4HmTNgrjdj++Vp0/znGFvwIDAQAB";
        var options = new Options();
        options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
        options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
        options.checkInventory = false;
        options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
        options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
        options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_GOOGLE, googlePublicKey } };
        options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;
        OpenIAB.init(options);
    }
    private void OnEnable()
    {
        // Listen to all events for illustration purposes
        OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
        OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
        OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
        OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
        OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
        OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
        OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
        OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;

    }
    private void OnDisable()
    {
        // Remove all event handlers
        OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
        OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
        OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
        OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
        OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
        OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
        OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
        OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
    }
    private void billingSupportedEvent()
    {
        _isInitialized = true;
        Debug.Log("Pyramid: billingSupportedEvent");
    }
    private void billingNotSupportedEvent(string error)
    {
        Debug.Log("Pyramid: billingNotSupportedEvent: " + error);
    }
    private void queryInventorySucceededEvent(Inventory inventory)
    {
        if (inventory != null)
        {
            //	_label = inventory.ToString();
            _inventory = inventory;
            Debug.Log("Pyramid: queryInventorySucceededEvent: " + inventory.ToString());

            Debug.Log("Pyramid: queryInventorySucceededEvent: " + inventory.HasPurchase(diamondPack1));
            if (inventory.HasPurchase(diamondPack1))
            {
            }
            if (inventory.HasPurchase(diamondPack2))
            {
            }
            if (inventory.HasPurchase(diamondPack3))
            {
            }
            onRestore();
        }
    }
    private void queryInventoryFailedEvent(string error)
    {
        Debug.Log("Pyramid: queryInventoryFailedEvent: " + error);

        //		_label = error;
    }
    private void purchaseSucceededEvent(Purchase purchase)
    {
        Debug.Log("Pyramid: purchaseSucceededEvent: " + purchase);
        //	_label = "PURCHASED:" + purchase.ToString();

        if (purchase.Sku.Equals(diamondPack1))
        {
            onPurchasePack();

            OpenIAB.consumeProduct(purchase);
            Attributes.gems += 500;
        }
        if (purchase.Sku.Equals(diamondPack2))
        {
            onPurchasePack();

            OpenIAB.consumeProduct(purchase);
            Attributes.gems += 1200;

        }
        if (purchase.Sku.Equals(diamondPack3))
        {
            onPurchasePack();
            OpenIAB.consumeProduct(purchase);
            Attributes.gems += 2000;

        }
        if (MainMenu.instance != null)
            MainMenu.instance.UpdateGemText();
    }
    private void purchaseFailedEvent(int errorCode, string errorMessage)
    {
        Debug.Log("Pyramid: purchaseFailedEvent: " + errorMessage);

        if (errorCode == 7)
        {
            onPurchaseFail();
        }
        //		_label = "Purchase Failed: " + errorMessage;
    }
    private void consumePurchaseSucceededEvent(Purchase purchase)
    {
        Debug.Log("Pyramid: consumePurchaseSucceededEvent: " + purchase);
        //	_label = "CONSUMED: " + purchase.ToString();
    }
    private void consumePurchaseFailedEvent(string error)
    {
        Debug.Log("Pyramid: consumePurchaseFailedEvent: " + error);
        //	_label = "Consume Failed: " + error;
    }
}
