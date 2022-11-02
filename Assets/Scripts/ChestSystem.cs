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
    public byte selectedSlotIndex;
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

    [Header("Chest Preview UI")]
    public GameObject previewPanel;
    public TMP_Text previewChestRarity;
    public Image previewChestImage;
    public TMP_Text previewChestnuts;
    public TMP_Text previewShinyCoins;
    
    [Header("SFX")] 
    public AudioClip unlockedSfx;

    [Header("Chest Arrays")]
    public Sprite[] chestSprites = new Sprite[7];
    public int[] chestnutsAmounts = new int[7];
    public int[] shinyCoinsAmounts = new int[7];

    

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
        previewPanel.SetActive(false);
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
    
    #region ChestPreviewFuncs
    public void PreviewFirstChest()
    {
        previewPanel.SetActive(true);
        previewChestRarity.text = Convert.ToString(firstChestRarity);
        previewChestImage.sprite = chestSprites[(int) firstChestRarity];
        selectedChestRarity = firstChestRarity;
        selectedSlotIndex = 1;
        previewShinyCoins.text = Convert.ToString(shinyCoinsAmounts[(int)firstChestRarity]);
        previewChestnuts.text = Convert.ToString(chestnutsAmounts[(int)firstChestRarity]);
    }
    
    public void PreviewSecondChest()
    {
        previewPanel.SetActive(true);
        previewChestRarity.text = Convert.ToString(secondChestRarity);
        previewChestImage.sprite = chestSprites[(int) secondChestRarity];
        selectedChestRarity = secondChestRarity;
        selectedSlotIndex = 2;
        previewChestnuts.text = Convert.ToString(chestnutsAmounts[(int)secondChestRarity]);
        previewShinyCoins.text = Convert.ToString(shinyCoinsAmounts[(int)secondChestRarity]);
    }
    
    public void PreviewThirdChest()
    {
        previewPanel.SetActive(true);
        previewChestRarity.text = Convert.ToString(thirdChestRarity);
        previewChestImage.sprite = chestSprites[(int) thirdChestRarity];
        selectedChestRarity = thirdChestRarity;
        selectedSlotIndex = 3;
        previewChestnuts.text = Convert.ToString(chestnutsAmounts[(int)thirdChestRarity]);
        previewShinyCoins.text = Convert.ToString(shinyCoinsAmounts[(int)thirdChestRarity]);
    }
    #endregion
    
    public void OpenChest()
    {
        _full = false;
        if (selectedSlotIndex == 1) {
            firstSlot = Slot.Empty;
            firstChestTransform.GetComponent<Image>().enabled = false;
        }
        else if (selectedSlotIndex == 2) {
            secondSlot = Slot.Empty;
            secondChestTransform.GetComponent<Image>().enabled = false;
        }
        else if (selectedSlotIndex == 3) {
            thirdSlot = Slot.Empty;
            thirdChestTransform.GetComponent<Image>().enabled = false;
        }
        previewPanel.SetActive(false);
    }
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