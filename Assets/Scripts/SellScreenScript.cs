using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SellScreenScript : MonoBehaviour
{
    public float currentWood;
    public float currentCoin;
    public float woodValue;
    public TMP_Text SellTimerText;
    public TMP_Text SellPriceText;
    public float remainingSeconds;
    private int woodvalueTimer= 60;
    public GameManager gameManager;
    void Start()
    {
        woodValue = Random.Range(10, 0);
        SellPriceText.text = "Current Value $" + (woodValue.ToString());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingSeconds > 0) 
        {
            remainingSeconds -= Time.deltaTime;
            SellTimerText.text = "Time Until Reset " + (remainingSeconds.ToString("F0"));
        }
        if (remainingSeconds <= 0) 
        {
            woodValue = Random.Range(10, 0);
            SellPriceText.text = "Current Value $" + (woodValue.ToString());
            remainingSeconds = woodvalueTimer;
        }

        currentWood = gameManager.currentWood;
        currentCoin = gameManager.currentCoin;
    }
    public void SellWood1()            
    {
        if (currentWood > 1) {
            gameManager.currentCoin = currentCoin + woodValue;
            gameManager.currentWood--;
        }
    }
    public void SellWood10()
    {
        if (currentWood > 10)
        {
            gameManager.currentCoin = currentCoin + (woodValue*10);
            gameManager.currentWood= currentWood - 10;
        }
    }
    public void SellWoodAll()
    {
        gameManager.currentCoin = currentCoin + (currentWood * woodValue);
        gameManager.currentWood = 0;
    }

}
