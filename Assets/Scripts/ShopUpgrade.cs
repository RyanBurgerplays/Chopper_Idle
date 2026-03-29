using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopUpgrade : MonoBehaviour
{
    public int startpPrice;
    public float upgradeMult;
    public float amountpersecond;
    public TMP_Text priceText;
    public TMP_Text amountText;
    public TMP_Text amountperText;
    public int amount;
    public float upgradepercentage;
    public bool UsesWood;   //what is used to buy ut
    public bool GivesWood;  //what it gives
    public GameManager gameManager;
    public GameObject backgroundsColor;
    private int calculatedPrice()
    {
        int price = Mathf.RoundToInt(startpPrice * Mathf.Pow(upgradeMult, amount)); //calculates how much the upgrade costs whenever you buy another
        return price;
    }
    public float StuffperSecond()
    {
        return amount * (amountpersecond*(1+upgradepercentage)) ;   // how much it produces a secnd
    }

    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        priceText.text = calculatedPrice().ToString();
        amountperText.text = (amountpersecond + "per"); 
        amountText.text=amount.ToString();

    }
    public  void ClickAction()
    {
        int price = calculatedPrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price, UsesWood); //sees if it can be afforded and checks if it costs wood or coin
        if (purchaseSuccess) 
        {
            backgroundsColor.GetComponent<Image>().color = Color.lightGreen;
            StartCoroutine(ChangeColorFast(0.4f));
            amount++;
            UpdateUI();
        }
        else
        {
            backgroundsColor.GetComponent<Image>().color = Color.darkRed;
            StartCoroutine(ChangeColorFast(0.4f));

        }
    }
    IEnumerator ChangeColorFast(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        backgroundsColor.GetComponent<Image>().color = Color.red;

    }
}
