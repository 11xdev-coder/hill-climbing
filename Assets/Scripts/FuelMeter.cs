using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

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

    public DriverDown driverDown;

    public Rigidbody2D target;

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
                    outOfFuelTransform.gameObject.SetActive(true);
                    vo_OutOfFuel.Play();
                    isOutOfFuel = false;
                }
            }
        }
    }
}
