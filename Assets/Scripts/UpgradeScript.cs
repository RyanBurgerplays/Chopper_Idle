using UnityEngine;
using TMPro;

public class UpgradeScript : MonoBehaviour
{
    public TreeScript TreeScript;
    public TreeManager TreeManager;
    public CameraClicker Player;
    public GameManager GameManager;
    public int MaxUpgradeAmount;
    public TMP_Text UpgradeAmountText;
    public TMP_Text PriceText;
    public NeighborScript NeighborScript;
    public GameObject Neighbor;
    public int startPrice;
    public float upgradeMult;
    public int amount;
    private void Start()
    {
        UpdateUI();
    }
    private int calculatedPrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradeMult, amount)); //calculates how much the upgrade costs whenever you buy another
        return price;
    }
    public void AxeUpgrade()
    {
        if (amount < MaxUpgradeAmount)
        {
            int price = calculatedPrice();  //maybe mvoe above the if
            if (GameManager.currentWood >= price)
            {
                GameManager.currentWood = GameManager.currentWood - price;
                amount++;
                Player.ChopTime = 15 - amount;
                UpdateUI();
            }
        }
        
    }
    public void GrowUpgrade()
    {
        if (amount < MaxUpgradeAmount)
        {
            int price = calculatedPrice();
            if (GameManager.currentWood >= price)
            {
                GameManager.currentWood = GameManager.currentWood - price;
                amount++;
                GameManager.GrowSpeed = 15 - amount;
                UpdateUI();
            }
        }
    }
    public void TreeUpgrade()
    {
        if (amount < MaxUpgradeAmount)
        {
            int price = calculatedPrice();
            if (GameManager.currentWood >= price)
            {
                GameManager.currentWood = GameManager.currentWood - price;
                amount++;
                TreeManager.numberofTrees = 4 + amount;
                TreeManager.HowManyTrees();
                UpdateUI();
            }
        }
    }
    public void NeighborUpgrade()
    {
        if (amount < MaxUpgradeAmount)
        {
            int price = calculatedPrice();
            if (GameManager.currentWood >= price)
            {
                GameManager.currentWood = GameManager.currentWood - price;

                if (amount == 0)
                {
                    Neighbor.SetActive(true);
                    NeighborScript.Invoke("FirstBought", 2f);
                    amount++;
                    UpdateUI();
                }
                else
                {
                    amount++;
                    NeighborScript.neighborChopSpeed = NeighborScript.neighborChopSpeed - 1;
                    UpdateUI();
                }
            }
        }
    }
    public void RareTreeUpgrade()
    {
        if (amount < MaxUpgradeAmount)
        {
            int price = calculatedPrice();
            if (GameManager.currentWood >= price)
            {
                GameManager.currentWood = GameManager.currentWood - price;
                amount++;
                TreeManager.RareChanceUpgrade = TreeManager.RareChanceUpgrade + amount;
                UpdateUI();
            }
                
        }
    }
    public void WoodMultUpgrade()
    {
        if (amount < MaxUpgradeAmount)
        {
            int price = calculatedPrice();
            if (GameManager.currentWood >= price)
            {
                GameManager.currentWood = GameManager.currentWood - price;
                amount++;
                GameManager.WoodMult = GameManager.WoodMult + amount;
                UpdateUI();
            }
        }

    }
    public void UpdateUI()
    {
        UpgradeAmountText.text = amount.ToString();
        PriceText.text = calculatedPrice().ToString();
    }
}
