using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelMeter : MonoBehaviour
{
    public float fuel;

    public float maxFuel = 1.0f;

    public float minFuelNeedleAngle;
    public float maxFuelNeedleAngle;

    public RectTransform needle;

    public bool isOutOfFuel = false;
    public Transform outOfFuelTransform;
    public AudioSource vo_OutOfFuel;
    public AudioClip clip;

    public DriverDown driverDown;

    public Rigidbody2D target;

    public bool cantBeDD = false;

    public Transform hud;

    public void Start()
    {
        outOfFuelTransform.gameObject.SetActive(false);
    }

    void Update()
    {
        if (needle != null)
        {
            needle.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minFuelNeedleAngle, maxFuelNeedleAngle, fuel / maxFuel));
        }


        if (!driverDown.isDriverDown)
        {
            if (isOutOfFuel)
            {
                if (target.velocity.sqrMagnitude <= 1)
                {
                    vo_OutOfFuel.Play();
                    outOfFuelTransform.gameObject.SetActive(true);
                    isOutOfFuel = false;
                    cantBeDD = true;
                    Invoke("DisableHUD", 1.4f);
                }
            }
        }
    }

    public void DisableHUD()
    {
        hud.gameObject.SetActive(false);
    }
}
