using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LicenseSystem : MonoBehaviour
{
    public byte isFirstTime;
    public string name;

    [Header("UI")]
    public TMP_Text nameInput;
    
    // Start is called before the first frame update
    void Start()
    {
        isFirstTime = (byte)PlayerPrefs.GetInt("firsttime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveName()
    {
        isFirstTime = 0;
        PlayerPrefs.SetInt("firsttime", isFirstTime);
        name = nameInput.text;
        PlayerPrefs.SetString("drivername", name);
        print(name);
    }
}
