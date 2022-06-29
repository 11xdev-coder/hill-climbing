using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusesCheck : MonoBehaviour
{
    public WheelAirTime frontTireDef;
    public WheelAirTime backTireDef;
    public CarController carDef;
    public int totalAirTime;
    public int currentAirTime;


    public GameObject airTimeText;
    public CanvasGroup bonusesCanvas;

    public int textRotSpeed = 150;
    public bool isRotatedAirTimeText = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckForBonuses", 1, 1);
        bonusesCanvas.alpha = 0;
        airTimeText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckForBonuses()
    {
        if (frontTireDef.isInAir == true && backTireDef.isInAir == true && carDef.isCarInAir == true)
        {
            if (!isRotatedAirTimeText)
            {
                airTimeText.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 55));
                airTimeText.transform.localScale = new Vector3(1, 1, 1);
                isRotatedAirTimeText = true;
            }
            AirTimeBonus();
        }
        else
        {
            InvokeRepeating("FadeOutBC", 0.4f, Time.deltaTime);
            currentAirTime = 0;
            isRotatedAirTimeText = false;
        }
    }

    void AirTimeBonus()
    {
        totalAirTime++;
        currentAirTime++;

        bonusesCanvas.alpha = 1;

        InvokeRepeating("RotateAirTimeText", Time.deltaTime,Time.deltaTime);
        InvokeRepeating("IncreaseSizeAirTimeText", Time.deltaTime, Time.deltaTime);

        airTimeText.GetComponent<TMP_Text>().text = "AIR TIME \n  +" + currentAirTime;


        //print(totalAirTime);
    }

    void RotateAirTimeText()
    {
        // 55 = 0.46
        // 25 = 0.21
        // print(airTimeText.transform.rotation.z);
        if (airTimeText.transform.rotation.z >= 0.21)
        {
            airTimeText.transform.Rotate(0, 0, -1 * textRotSpeed * Time.deltaTime);
        }
        else
        {
            CancelInvoke("RotateAirTimeText");
        }
    }

    void IncreaseSizeAirTimeText()
    {
        //print(airTimeText.transform.localScale.x);
        if (airTimeText.transform.localScale.x < 2)
        {
            airTimeText.transform.localScale = new Vector3(airTimeText.transform.localScale.x + 0.0008f,
                airTimeText.transform.localScale.y + 0.0008f);
        }
        else
        {
            CancelInvoke("IncreaseSizeAirTimeText");
        }
    }

    void FadeOutBC()
    {
        if (bonusesCanvas.alpha > 0)
        {
            bonusesCanvas.alpha -= Time.deltaTime;
        }
        else
        {
            CancelInvoke("FadeOutBC");
        }
    }
}
