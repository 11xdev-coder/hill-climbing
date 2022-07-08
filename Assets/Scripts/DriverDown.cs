using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverDown : MonoBehaviour
{
    public Transform[] allPartsToDisconnect;

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

    public int layerIndex = -1;
    public int disconnectIndex = -1;

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
                    foreach (Transform t in allPartsToDisconnect)
                    {
                        disconnectIndex++;
                        allPartsToDisconnect[disconnectIndex].GetComponent<DisconnectHingeJoint>().Disconnect();
                    }

                    GetComponent<DisconnectHingeJoint>().Disconnect();
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
                        layerIndex++;
                        partsToChangeLayer[layerIndex].gameObject.layer = LayerMask.NameToLayer(afterDDmaskName);
                    }
                }
            }
        }
    }
}
