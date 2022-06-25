using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriverDown : MonoBehaviour
{
    public HingeJoint2D connectedToCarPart;
    public Rigidbody2D partToConnect;

    public WheelJoint2D frontTire;
    public WheelJoint2D backTire;

    public CarController carController;

    public AudioSource ASboneCrack;
    public AudioSource ASvo;
    public bool isDriverDown = false;

    public Transform driverDownImage;

    public FuelMeter fuelMeter;

    void Start()
    {
        driverDownImage.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!fuelMeter.isOutOfFuel)
        {
            if (!isDriverDown)
            {
                if (col.transform.CompareTag("Terrain"))
                {
                    connectedToCarPart.connectedBody = partToConnect;
                    frontTire.useMotor = false;
                    backTire.useMotor = false;

                    carController.enabled = false;

                    driverDownImage.gameObject.SetActive(true);

                    ASboneCrack.Play();
                    ASvo.Play();
                    isDriverDown = true;
                }
            }
        }
    }
}
