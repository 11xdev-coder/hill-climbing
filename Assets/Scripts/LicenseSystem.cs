using TMPro;
using UnityEngine;

public class LicenseSystem : MonoBehaviour
{
    public byte isFirstTime = 1;
    public new string name;

    [Header("UI")] 
    public GameObject main;
    public GameObject firsttimePanel;
    public TMP_Text nameInputFTScreen;
    public TMP_Text nameInputEditScreen;
    public TMP_Text nameLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        name = PlayerPrefs.GetString("drivername");
        nameLabel.text = name;
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

    public void SaveNameFromFTScreen()
    {
        isFirstTime = 0;
        PlayerPrefs.SetInt("firsttime", isFirstTime);
        name = nameInputFTScreen.text;
        PlayerPrefs.SetString("drivername", name);
        firsttimePanel.SetActive(false);
        main.SetActive(true);
        nameLabel.text = name;
    }

    public void SaveNameFromEditScreen()
    {
        name = nameInputEditScreen.text;
        PlayerPrefs.SetString("drivername", name);
        nameLabel.text = name;
    }
}
