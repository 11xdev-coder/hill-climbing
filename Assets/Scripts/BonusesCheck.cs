using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusesCheck : MonoBehaviour
{
    public WheelAirTime frontTireDef;
    public WheelAirTime backTireDef;
    public CarController carDef;

    [Header("Air Time")]
    public int totalAirTime;
    public int currentAirTime;
    public GameObject airTimeText;
    public bool isRotatedAirTimeText = false;

    [Header("Wheelie")] 
    public int totalWheelie;
    public int currentWheelie;
    public GameObject wheelieText;
    public bool isRotatedWheelieText = false;

    [Header("Flips")] 
    public float startFlipAngleZ = float.NaN;
    public int totalFlips;
    public GameObject flipText;

    public int textRotSpeed = 150;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckForBonuses", 1, 1);
        InvokeRepeating("FlipCheck", 0.1f, 0.1f);

        airTimeText.gameObject.SetActive(true);
        wheelieText.gameObject.SetActive(true);
        flipText.gameObject.SetActive(true);

        airTimeText.GetComponent<TMP_Text>().alpha = 0;
        wheelieText.GetComponent<TMP_Text>().alpha = 0;
        flipText.GetComponent<TMP_Text>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region BonusCheck
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
            InvokeRepeating("FadeOutAirTimeText", 0.4f, Time.deltaTime);
            currentAirTime = 0;
            isRotatedAirTimeText = false;
        }

        if (frontTireDef.isInAir == true && backTireDef.isInAir == false && carDef.isCarInAir == true)
        {
            if (!isRotatedWheelieText)
            {
                wheelieText.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 15));
                wheelieText.transform.localScale = new Vector3(1, 1, 1);
                isRotatedWheelieText = true;
            }
            WheelieBonus();
        }
        else
        {
            InvokeRepeating("FadeOutWheelieText", 0.4f, Time.deltaTime);
            currentWheelie = 0;
            isRotatedWheelieText = false;
        }
    }
#endregion

    void FlipCheck()
    {
        if (frontTireDef.isInAir == true && backTireDef.isInAir == true && carDef.isCarInAir == true)
        {
            if (float.IsNaN(startFlipAngleZ))
            {
                startFlipAngleZ = carDef.transform.rotation.z;
            }

            if (carDef.transform.rotation.z >= startFlipAngleZ + 0.25f)
            {
                FlipBonus();
                startFlipAngleZ = float.NaN;
            }
            else if (carDef.transform.rotation.z <= startFlipAngleZ - 0.25f)
            {
                FlipBonus();
                startFlipAngleZ = float.NaN;
            }
        }
        else
        {
            InvokeRepeating("FadeOutFlipText", 0.4f, Time.deltaTime);
            startFlipAngleZ = float.NaN;
        }
    }

    #region AirTime
    void AirTimeBonus()
    {
        totalAirTime++;
        currentAirTime++;

        airTimeText.GetComponent<TMP_Text>().alpha = 1;

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

    void FadeOutAirTimeText()
    {
        if (airTimeText.GetComponent<TMP_Text>().alpha > 0)
        {
            airTimeText.GetComponent<TMP_Text>().alpha -= Time.deltaTime;
        }
        else
        {
            CancelInvoke("FadeOutAirTimeText");
        }
    }
    #endregion

    #region Wheelie
    void WheelieBonus()
    {
        totalWheelie++;
        currentWheelie++;

        wheelieText.GetComponent<TMP_Text>().alpha = 1;

        wheelieText.GetComponent<TMP_Text>().text = "WHEELIE \n  +" + currentWheelie;
        InvokeRepeating("SizeWheelieText", Time.deltaTime, Time.deltaTime);
        InvokeRepeating("RotateWheelieText", Time.deltaTime, Time.deltaTime);

        //print(totalWheelie);
    }

    void SizeWheelieText()
    {
        //if (wheelieText.transform.localScale.y < 1)
        //{
        //    wheelieText.transform.localScale = new Vector3(wheelieText.transform.localScale.x,
        //        wheelieText.transform.localScale.y + 0.1f);
        //}
        //else
        //{
        //    CancelInvoke("ScaleWheelieText");
        //}
        if (wheelieText.transform.localScale.x < 2)
        {
            wheelieText.transform.localScale = new Vector3(wheelieText.transform.localScale.x + 0.0008f,
                wheelieText.transform.localScale.y + 0.0008f);
        }
        else
        {
            CancelInvoke("SizeWheelieText");
        }
    }
    void RotateWheelieText()
    {
        //print(wheelieText.transform.rotation.z);
        if (wheelieText.transform.rotation.z < 0.30)
        {
            wheelieText.transform.Rotate(0, 0, 1 * textRotSpeed * Time.deltaTime);
        }
        else
        {
            CancelInvoke("RotateWheelieText");
        }
    }

    void FadeOutWheelieText()
    {
        if (wheelieText.GetComponent<TMP_Text>().alpha > 0)
        {
            wheelieText.GetComponent<TMP_Text>().alpha -= Time.deltaTime;
        }
        else
        {
            CancelInvoke("FadeOutWheelieText");
        }
    }
    #endregion

    #region Flip
    public void FlipBonus()
    {
        totalFlips++;

        flipText.GetComponent<TMP_Text>().alpha = 1;

        InvokeRepeating("SizeFlipText", Time.deltaTime, Time.deltaTime);
        InvokeRepeating("RotateFlipText", Time.deltaTime, Time.deltaTime);
    }

    public void SizeFlipText()
    {
        if (flipText.transform.localScale.x < 2)
        {
            flipText.transform.localScale = new Vector3(flipText.transform.localScale.x + 0.0008f,
                flipText.transform.localScale.y + 0.0008f);
        }
        else
        {
            CancelInvoke("SizeFlipText");
        }
    }

    void RotateFlipText()
    {
        if (flipText.transform.rotation.z >= 0.21)
        {
            flipText.transform.Rotate(0, 0, -1 * textRotSpeed * Time.deltaTime);
        }
        else
        {
            CancelInvoke("RotateFlipText");
        }
    }

    void FadeOutFlipText()
    {
        if (flipText.GetComponent<TMP_Text>().alpha > 0)
        {
            flipText.GetComponent<TMP_Text>().alpha -= Time.deltaTime;
        }
        else
        {
            CancelInvoke("FadeOutFlipText");
        }
    }
    #endregion
}
