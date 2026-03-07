using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    public  float currentCoin;
    public  float currentWood = 15;
    public TMP_Text woodCount;
    public TMP_Text coinCount;
    public ShopUpgrade[] shopUpgrades;

    // Update is called once per frame
    void UpdateUI()
    {
        woodCount.text=currentWood.ToString("F1");
        coinCount.text=currentCoin.ToString("F1");

    }
    public bool PurchaseAction(int price, bool UsesWood)
    {
        if (UsesWood == true)
        {
            if (currentWood >= price)
            {
                currentWood -= price;
                UpdateUI();
                return true;
            }
        }
        else
        {
            if (currentCoin >= price)
            {
                currentCoin -= price;
                UpdateUI();
                return true;
            }
        }
        return false;
    }
    public float count;
    private float nextTimecheck = 1;
    private void Update()
    {
        if(nextTimecheck < Time.timeSinceLevelLoad)
        {
            IdleCount();
            nextTimecheck = Time.timeSinceLevelLoad + 1;
        }
        
        
    }
    
    void IdleCount()
    {
        float sumWood = 0;
        float sumCoin = 0;
        foreach (var ShopUpgrade in shopUpgrades) {
            if (ShopUpgrade.GivesWood==true) 
            {
                sumWood += ShopUpgrade.StuffperSecond();
            }
            else
            {
                sumCoin += ShopUpgrade.StuffperSecond();
            }
        }
        currentWood += sumWood;
        currentCoin += sumCoin;
        UpdateUI();

    }
}
