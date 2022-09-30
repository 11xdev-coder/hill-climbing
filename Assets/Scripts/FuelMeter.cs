using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelMeter : MonoBehaviour
{
    public float fuel = 1;

    public float maxFuel = 1.0f;

    public float minFuelNeedleAngle;
    public float maxFuelNeedleAngle;

    public RectTransform needle;

    void Update()
    {
        if (needle != null)
        {
            needle.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minFuelNeedleAngle, maxFuelNeedleAngle, fuel / maxFuel));
        }
    }
}
