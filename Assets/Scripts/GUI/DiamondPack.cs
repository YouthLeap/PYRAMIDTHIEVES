using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using OnePF;

public class DiamondPack : MonoBehaviour
{
    [SerializeField]
    private float cost;
    [SerializeField]
    private int ammount;
    [SerializeField]
    private Text costText;
    [SerializeField]
    private Text ammountText;
    //[SerializeField]
    //private string sku;

    void Start()
    {
        costText.text = cost + "$";
        ammountText.text = ammount.ToString();
    }

    //public void Purchase()
    //{
    //    OpenIAB.purchaseProduct(sku);
    //}
}
