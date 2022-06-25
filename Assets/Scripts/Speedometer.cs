using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public Rigidbody2D target;

    public float maxSpeed = 0.0f;

    public float minSpeedNeedleAngle;
    public float maxSpeedNeedleAngle;

    public RectTransform needle;

    private float speed;

    // Update is called once per frame
    void Update()
    {
        speed = target.velocity.magnitude * 3.6f;

        if (needle != null)
        {
            needle.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedNeedleAngle, maxSpeedNeedleAngle, speed / maxSpeed));
        }
    }
}
