using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ChestSystem : MonoBehaviour
{
    public Nullable<chestRarity> selectedChestRarity;
    public chestRarity firstChestRarity;
    public chestRarity secondChestRarity;
    public chestRarity thirdChestRarity;

    public slot firstSlot;
    public slot secondSlot;
    public slot thirdSlot;

    public GameObject[] chestSlots = new GameObject[3];

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedChestRarity = GetClosestChest();
            OpenChest();
        }
    }
    
    public void AddChestToSlot()
    {
        if (firstSlot == slot.empty && secondSlot == slot.empty && thirdSlot == slot.empty)
        {
            firstSlot = slot.hasChest;
            firstChestRarity = (chestRarity) Random.Range(0, 6);
        }
        
        else if (firstSlot == slot.hasChest && secondSlot == slot.empty && thirdSlot == slot.empty)
        {
            secondSlot = slot.hasChest;
            secondChestRarity = (chestRarity) Random.Range(0, 6);
        }
        
        else if (firstSlot == slot.hasChest && secondSlot == slot.hasChest && thirdSlot == slot.empty)
        {
            thirdSlot = slot.hasChest;
            thirdChestRarity = (chestRarity) Random.Range(0, 6);
        }
        
        else if (firstSlot == slot.hasChest && secondSlot == slot.hasChest && thirdSlot == slot.hasChest)
        {
            Debug.Log("ur full");
        }
    }

    public Nullable<chestRarity> GetClosestChest()
    {
        for (int c = 0; c < chestSlots.Length; c++)
        {
            if (Vector2.Distance(chestSlots[c].gameObject.transform.position, Input.mousePosition) <= 32)
            {
                if (c == 0)
                {
                    firstSlot = slot.empty;
                    return firstChestRarity;
                }
                if (c == 1)
                {
                    secondSlot = slot.empty;
                    return secondChestRarity;
                }
                if (c == 2)
                {
                    thirdSlot = slot.empty;
                    return thirdChestRarity;
                }
            }
        }
        
        return null;
    }
    
    public void OpenChest()
    {
        Debug.Log(selectedChestRarity);
        if (selectedChestRarity == chestRarity.bronze)
        {
            Debug.Log("5k coins");
        }
        else if (selectedChestRarity == chestRarity.silver)
        {
            Debug.Log("9k coins");
        }
        else if (selectedChestRarity == chestRarity.gold)
        {
            Debug.Log("13k coins");
        }
        else if (selectedChestRarity == chestRarity.epic)
        {
            Debug.Log("22k coins");
        }
        else if (selectedChestRarity == chestRarity.legend)
        {
            Debug.Log("29k coins");
        }
        else if (selectedChestRarity == chestRarity.champion)
        {
            Debug.Log("50k coins");
        }
        else if (selectedChestRarity == chestRarity.godly)
        {
            Debug.Log("65k coins");
        }
    }
}

public enum slot
{
    empty,
    hasChest,
}

public enum chestRarity
{
    bronze, // 0
    silver, // 1
    gold, // 2
    epic, // 3
    legend, // 4
    champion, // 5
    godly // 6
}
