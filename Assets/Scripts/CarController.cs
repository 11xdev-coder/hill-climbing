using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Tire vars")]
    public WheelJoint2D frontTire;
    public WheelJoint2D backTire;
    public JointMotor2D frontMotor;
    public JointMotor2D backMotor;

    [Header("Movement")]
    public float speedF;
    public float speedB;
    public float torqueF;
    public float torqueB;
    public float movement;
    public float carRotationSpeed = 1;

    [Header("Air")]
    public bool isCarInAir = true;

    [Header("Fuel")] 
    public float fuel = 1.0f;
    public float fuelDecrease;
    public FuelMeter fuelMeter;

    [Header("HUD")]
    public Transform hud;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FuelDecrease", 0.1f, 0.1f);
        hud.gameObject.SetActive(true);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Wheel"))
            isCarInAir = false;
        else
            isCarInAir = true;
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Wheel"))
            isCarInAir = true;
        else
            isCarInAir = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement = Input.GetAxis("Horizontal");
        if (movement > 0)
        {
            frontMotor.motorSpeed = speedF * -1;
            backMotor.motorSpeed = speedF * -1;

            frontMotor.maxMotorTorque = torqueF;
            backMotor.maxMotorTorque = torqueF;

            frontTire.motor = frontMotor;
            backTire.motor = backMotor;

            GetComponent<Rigidbody2D>().AddTorque(carRotationSpeed * movement);
        }
        else if (movement < 0)
        {
            frontMotor.motorSpeed = speedB * -1;
            backMotor.motorSpeed = speedB * -1;

            frontMotor.maxMotorTorque = torqueB;
            backMotor.maxMotorTorque = torqueB;

            frontTire.motor = frontMotor;
            backTire.motor = backMotor;

            GetComponent<Rigidbody2D>().AddTorque(carRotationSpeed * movement * 1);
        }
        else
        {
            frontTire.useMotor = false;
            backTire.useMotor = false;
        }
    }

    void FuelDecrease()
    {
        fuel -= fuelDecrease;
        fuelMeter.fuel = fuel;
        if (fuel <= 0)
        {
            GetComponent<CarController>().enabled = false;
            frontTire.useMotor = false;
            backTire.useMotor = false;
            fuelMeter.isOutOfFuel = true;
            CancelInvoke("FuelDecrease");
        }
    }
}