using System;
using TMPro;
using UnityEngine;

public class PlayerCollectibles : MonoBehaviour
{
    [Header("Collectibles")]
    public int chestnuts;
    public int shinyCoins;

    [Header("GUI")] 
    public TMP_Text chestnutsGUI;
    public TMP_Text shinyCoinsGUI;
    
    
    void Start()
    {
        chestnuts = PlayerPrefs.GetInt("chestnuts");
        shinyCoins = PlayerPrefs.GetInt("shinycoins");
        UpdateChestnuts(chestnuts);
        UpdateShinyCoins(shinyCoins);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddChestnuts(int newAmount)
    {
        chestnuts += newAmount;
        UpdateChestnuts(chestnuts);
        PlayerPrefs.SetInt("chestnuts", chestnuts);
    }

    public void AddShinyCoins(int newAmount)
    {
        shinyCoins += newAmount;
        UpdateShinyCoins(shinyCoins);
        PlayerPrefs.SetInt("shinycoins", shinyCoins);
    }

    public void UpdateChestnuts(int newVar) => chestnutsGUI.text = Convert.ToString(newVar);
    public void UpdateShinyCoins(int newVar) => shinyCoinsGUI.text = Convert.ToString(newVar);
}
