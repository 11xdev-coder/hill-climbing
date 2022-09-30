using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAirTime : MonoBehaviour
{
    public bool isInAir = false;
    // script must be on wheels
    public void OnCollisionExit2D(Collision2D col)
    {
        isInAir = true;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        isInAir = false;
    }
}
