using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusesCheck : MonoBehaviour
{
    // defining car and wheels
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
    public bool isRotatedFlipText = false;

    public int textRotSpeed = 150;
    // Start is called before the first frame update
    void Start()
    {
        // invoking functions for bonuses and flips
        InvokeRepeating("CheckForBonuses", 1, 1);
        InvokeRepeating("FlipCheck", 0.1f, 0.1f);
        
        // setting active bonus splashes
        airTimeText.gameObject.SetActive(true);
        wheelieText.gameObject.SetActive(true);
        flipText.gameObject.SetActive(true);
        
        // setting bonus splashes's alpha to zero
        airTimeText.GetComponent<TMP_Text>().alpha = 0;
        wheelieText.GetComponent<TMP_Text>().alpha = 0;
        flipText.GetComponent<TMP_Text>().alpha = 0;
    }

    #region BonusCheck
    void CheckForBonuses()
    {
        // checking if both of wheels are in air and car is also in air
        if (frontTireDef.isInAir == true && backTireDef.isInAir == true && carDef.isCarInAir == true)
        {
            // check "is air time splash text rotated?"
            if (!isRotatedAirTimeText)
            {
                // if not, rotating it and scaling it
                airTimeText.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 55));
                airTimeText.transform.localScale = new Vector3(1, 1, 1);
                isRotatedAirTimeText = true;
            }
            // calling air time bonus func
            AirTimeBonus();
            // exitting from function
            return;
        }
        else
        {
            // if car not in air, we fade out air time splash text
            InvokeRepeating("FadeOutAirTimeText", 0.4f, Time.deltaTime);
            currentAirTime = 0;
            isRotatedAirTimeText = false;
        }
        // checking if car's back wheel isn't in air, but front wheel and car is
        if (frontTireDef.isInAir == true && backTireDef.isInAir == false && carDef.isCarInAir == true)
        {
            // check "is wheelie splash text rotated?"
            if (!isRotatedWheelieText)
            {
                // if not, rotating it and scaling it
                wheelieText.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 15));
                wheelieText.transform.localScale = new Vector3(1, 1, 1);
                isRotatedWheelieText = true;
            }
            // calling wheelie bonus func
            WheelieBonus();
            // exiting from function
            return;
        }
        else
        {
            // if front wheel or car isn't in air, fading out wheelie splash text
            InvokeRepeating("FadeOutWheelieText", 0.4f, Time.deltaTime);
            currentWheelie = 0;
            isRotatedWheelieText = false;
        }
    }
#endregion

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
    
    void FlipCheck()
    {
        // checking if car in air
        if (frontTireDef.isInAir == true && backTireDef.isInAir == true && carDef.isCarInAir == true)
        {
            // checking if flip start angle is null
            if (float.IsNaN(startFlipAngleZ))
            {
                // if yes, setting it to car's z pos
                startFlipAngleZ = carDef.transform.rotation.z;
            }
            
            // checking if car's z pos bigger than flip start pos by 330 degrees (front flip)
            if (carDef.transform.rotation.z >= startFlipAngleZ + 0.35f)
            {
                // checking "is flip splash text rotated?"
                if (!isRotatedFlipText)
                {
                    // rotating and scaling it
                    flipText.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 55));
                    flipText.transform.localScale = new Vector3(1, 1, 1);
                    isRotatedFlipText = true;
                }
                // calling flip bonus func
                FlipBonus();
                // setting flip start angle to null
                startFlipAngleZ = float.NaN;
            }
            // checking if z pos less than flip start pos by 330 degrees (back flip)
            else if (carDef.transform.rotation.z <= startFlipAngleZ - 0.35f)
            {
                // doing same thing as front flip
                if (!isRotatedFlipText)
                {
                    flipText.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 55));
                    flipText.transform.localScale = new Vector3(1, 1, 1);
                    isRotatedFlipText = true;
                }
                FlipBonus();
                startFlipAngleZ = float.NaN;
            }
        }
        else
        {
            // if car didn't do any flips, fading out text
            isRotatedFlipText = false;
            CancelInvoke("SizeFlipText");
            CancelInvoke("RotateFlipText");
            InvokeRepeating("FadeOutFlipText", 0.4f, Time.deltaTime);
            startFlipAngleZ = float.NaN;
        }
    }
    #endregion
}
