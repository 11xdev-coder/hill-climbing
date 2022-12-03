using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LicenseSystem : MonoBehaviour
{
    public string name;

    [Header("UI")]
    public TMP_Text nameInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveName()
    {
        name = nameInput.text;
        PlayerPrefs.SetString("drivername", name);
        print(name);
    }
}
