using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.IO;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public  float currentCoin;
    public float currentWood;
    public TMP_Text woodCount;
    public TMP_Text coinCount;
    public ShopUpgrade[] shopUpgrades;
    public int GrowSpeed;
    public int WoodMult;
    private string savePath;




    public void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        GrowSpeed = 15;
        WoodMult = 1;
        LoadGame();
        UpdateUI();
        StartCoroutine(SaveTime());


    }
    IEnumerator SaveTime()
    {
        while (true) {
            yield return new WaitForSeconds(10f);
            SaveGame();
        }

    }
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
                SaveGame();
                return true;
            }
        }
        else
        {
            if (currentCoin >= price)
            {
                currentCoin -= price;
                UpdateUI();
                SaveGame();
                return true;
            }
        }
        return false;
    }

    private float nextTimecheck = 1;
    private void Update()       //gives stuff every second
    {
        if(nextTimecheck < Time.timeSinceLevelLoad)
        {
            IdleCount();
            nextTimecheck = Time.timeSinceLevelLoad + 1;
        }



    }
    
    public void IdleCount()            // how much of each resource you get per second 
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
    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.currentWood = currentWood;
        data.currentCoin = currentCoin;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Saved to: " + savePath);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            currentWood = data.currentWood;
            currentCoin = data.currentCoin;

            Debug.Log("Loaded save!");
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }
}
[System.Serializable]
public class SaveData
{
    public float currentWood;
    public float currentCoin;
}
