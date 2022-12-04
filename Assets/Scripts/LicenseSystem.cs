using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LicenseSystem : MonoBehaviour
{
    public bool isFirstTime= true;
    public string name;

    [Header("UI")]
    public TMP_Text nameInput;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("firsttime") > 0) isFirstTime = true;
        else isFirstTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveName()
    {
        isFirstTime = false;
        if (isFirstTime) PlayerPrefs.SetInt("firsttime", 0);
        else PlayerPrefs.SetInt("firsttime", 1);
        name = nameInput.text;
        PlayerPrefs.SetString("drivername", name);
        print(name);
    }
}
