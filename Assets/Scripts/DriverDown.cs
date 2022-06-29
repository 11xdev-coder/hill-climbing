using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Transform hud;

    public Transform[] partsToChangeLayer;
    public string afterDDmaskName;

    public int index = -1;

    void Start()
    {
        driverDownImage.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!fuelMeter.cantBeDD)
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
                    hud.gameObject.SetActive(false);
                    foreach (Transform t in partsToChangeLayer)
                    {
                        index++;
                        partsToChangeLayer[index].gameObject.layer = LayerMask.NameToLayer(afterDDmaskName);
                    }
                }
            }
        }
    }
}
