using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Image = UnityEngine.UI.Image;

public class ChestSystem : MonoBehaviour
{
    public GameObject mainHUD;
    [Header("Chest Rarities")] 
    public ChestRarity selectedChestRarity;
    public ChestRarity firstChestRarity;
    public ChestRarity secondChestRarity;
    public ChestRarity thirdChestRarity;

    [Header("Chest Slots")] 
    public Slot firstSlot;
    public Slot secondSlot;
    public Slot thirdSlot;

    [Header("Chest Button Transforms")] 
    public GameObject firstChestTransform;
    public GameObject secondChestTransform;
    public GameObject thirdChestTransform;

    [Header("Chest Pop Up")]
    public GameObject popUpPanel;
    public TMP_Text popUpChestRarity;
    public Image popUpChestImage;
    public bool fadeInChestPopUp;
    public CanvasGroup popUpPanelCG;

    [Header("Chest Sprites")]
    public Sprite[] chestSprites = new Sprite[7];

    [Header("SFX")] 
    public AudioClip unlockedSfx;

    private bool _full;

    public void HidePopUpPanel()
    {
        fadeInChestPopUp = true;
    }
    
    public void Start()
    {
        // initialization
        popUpPanel.SetActive(false);
        popUpPanel.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void Update()
    {
        // checking if slot are empty
        // 1st slot
        if (firstSlot == Slot.Empty)
        {
            firstChestTransform.GetComponent<Image>().enabled = false;
        }
        else if (firstSlot == Slot.HasChest)
        {
            firstChestTransform.GetComponent<Image>().enabled = true;
        }
        
        // 2nd slot
        if (secondSlot == Slot.Empty)
        {
            secondChestTransform.GetComponent<Image>().enabled = false;
        }
        else if (secondSlot == Slot.HasChest)
        {
            secondChestTransform.GetComponent<Image>().enabled = true;
        }
        
        // 3rd slot
        if (thirdSlot == Slot.Empty)
        {
            thirdChestTransform.GetComponent<Image>().enabled = false;
        }
        else if (thirdSlot == Slot.HasChest)
        {
            thirdChestTransform.GetComponent<Image>().enabled = true;
        }
        
        // pop up panel FadeIn function
        if (fadeInChestPopUp)
        {
            if (popUpPanelCG.alpha < 1)
            {
                popUpPanelCG.alpha += Time.deltaTime;
                if (popUpPanelCG.alpha >= 1)
                    fadeInChestPopUp = false;
            }
        }
    }

    public void AddChestToSlot()
    {
        // for loop to check all the slots
        for (int s = 0; s <= 2; s++)
        {
            // we are on 1st slot
            if (s == 0)
            {
                // is 1st slot empty?
                if (firstSlot == Slot.Empty)
                {
                    firstSlot = Slot.HasChest;
                    firstChestRarity = (ChestRarity) Random.Range(0, 6);
                    selectedChestRarity = firstChestRarity;
                    firstChestTransform.GetComponent<Image>().sprite = chestSprites[(int) selectedChestRarity];
                    break;
                }
            }
            // we are on 2nd slot
            else if (s == 1)
            {
                // is 2nd slot empty?
                if (secondSlot == Slot.Empty)
                {
                    secondSlot = Slot.HasChest;
                    secondChestRarity = (ChestRarity) Random.Range(0, 6);
                    selectedChestRarity = secondChestRarity;
                    secondChestTransform.GetComponent<Image>().sprite = chestSprites[(int) selectedChestRarity];
                    break;
                }
            }
            // we are on 3rd slot
            else if (s == 2)
            {
                // is 3rd slot empty?
                if (thirdSlot == Slot.Empty)
                {
                    thirdSlot = Slot.HasChest;
                    thirdChestRarity = (ChestRarity) Random.Range(0, 6);
                    selectedChestRarity = thirdChestRarity;
                    thirdChestTransform.GetComponent<Image>().sprite = chestSprites[(int) selectedChestRarity];
                    break;
                }
            }
        }

        if (!_full)
        {
            mainHUD.SetActive(false);
            popUpPanel.SetActive(true);
            popUpChestRarity.text = Convert.ToString(selectedChestRarity);
            popUpChestImage.sprite = chestSprites[(int) selectedChestRarity];
            AudioSource.PlayClipAtPoint(unlockedSfx, new Vector3(0,0,0));
            fadeInChestPopUp = true;
        }

        // are all slots has chest?
        if (firstSlot == Slot.HasChest && secondSlot == Slot.HasChest && thirdSlot == Slot.HasChest)
        {
            _full = true;
        }
    }

    #region ChestOpenFuncs
    public void OpenFirstChest()
    {
        selectedChestRarity = firstChestRarity;
        firstSlot = Slot.Empty;
        OpenChest();
    }
    
    public void OpenSecondChest()
    {
        selectedChestRarity = secondChestRarity;
        secondSlot = Slot.Empty;
        OpenChest();
    }
    
    public void OpenThirdChest()
    {
        selectedChestRarity = thirdChestRarity;
        thirdSlot = Slot.Empty;
        OpenChest();
    }

    public void ShowChestUI()
    {
        
    }
    
    private void OpenChest()
    {
        _full = false;
        Debug.Log(selectedChestRarity);
        // slot checks
        if (firstSlot == Slot.Empty)
        {
            firstChestTransform.GetComponent<Image>().enabled = false;
        }
        else if (secondSlot == Slot.Empty)
        {
            secondChestTransform.GetComponent<Image>().enabled = false;
        }
        else if (thirdSlot == Slot.Empty)
        {
            thirdChestTransform.GetComponent<Image>().enabled = false;
        }
        
        // rarity checks
        if (selectedChestRarity == ChestRarity.Bronze)
        {
            Debug.Log("5k coins");
        }
        else if (selectedChestRarity == ChestRarity.Silver)
        {
            Debug.Log("9k coins");
        }
        else if (selectedChestRarity == ChestRarity.Gold)
        {
            Debug.Log("13k coins");
        }
        else if (selectedChestRarity == ChestRarity.Epic)
        {
            Debug.Log("22k coins");
        }
        else if (selectedChestRarity == ChestRarity.Legend)
        {
            Debug.Log("29k coins");
        }
        else if (selectedChestRarity == ChestRarity.Champion)
        {
            Debug.Log("50k coins");
        }
        else if (selectedChestRarity == ChestRarity.Godly)
        {
            Debug.Log("65k coins");
        }
    }
    
    #endregion
}

public enum Slot
{
    Empty,
    HasChest,
}

public enum ChestRarity
{
    Bronze, // 0
    Silver, // 1
    Gold, // 2
    Epic, // 3
    Legend, // 4
    Champion, // 5
    Godly // 6
}