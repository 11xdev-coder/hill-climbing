using TMPro;
using UnityEngine;

public class LicenseSystem : MonoBehaviour
{
    public byte isFirstTime = 1;
    public new string name;

    [Header("UI")] 
    public GameObject main;
    public GameObject firsttimePanel;
    public TMP_Text nameInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
        isFirstTime = (byte)PlayerPrefs.GetInt("firsttime");
        if (isFirstTime > 0)
        {
            firsttimePanel.SetActive(true);
        }
        else
        {
            firsttimePanel.SetActive(false);
            main.SetActive(true);
        }
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
        firsttimePanel.SetActive(false);
        main.SetActive(true);
        print(name);
    }
}
