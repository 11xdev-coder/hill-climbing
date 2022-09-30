using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Tires")]
    public WheelJoint2D frontTire;
    public WheelJoint2D backTire;
    public JointMotor2D frontMotor;
    public JointMotor2D backMotor;

    [Header("Engine")]
    public float speedF;
    public float speedB;
    public float torqueF;
    public float torqueB;
    public float movement;
    public float carRotationSpeed = 1;
    public Rigidbody2D rb;
    
    [Header("Air")]
    public bool isCarInAir = true;

    [Header("Fuel")] 
    public float fuel = 1.0f;
    public float fuelDecrease;
    public FuelMeter fuelMeter;
    public bool isOutofFuel;
    public Transform outOfFuelTransform;
    public AudioSource vo_OutOfFuel;
    public bool isOutofFuelPlayed;

    [Header("Driver")]
    public bool cantBeDDtext = false;
    public DriverDown driverDownScript;

    [Header("HUD")]
    public Transform hud;

    void Awake()
    {
        // assigning car's rigid body 2d
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // decreasing fuel
        InvokeRepeating("FuelDecrease", 0.1f, 0.1f);
        // enabling hud
        hud.gameObject.SetActive(true);
        // disabling out of fuel text
        outOfFuelTransform.gameObject.SetActive(false);
        isOutofFuelPlayed = false;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        // check "is we touched wheels?"
        if (col.transform.CompareTag("Wheel"))
            isCarInAir = false;
        // if not, set car in air to true
        else
            isCarInAir = true;
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        isCarInAir = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // getting horizontal input from user
        movement = Input.GetAxis("Horizontal");
        if (movement > 0)
        {
            // setting wheels' motor speed
            frontMotor.motorSpeed = speedF * -1;
            backMotor.motorSpeed = speedF * -1;
            // setting wheels' motors' torque
            frontMotor.maxMotorTorque = torqueF;
            backMotor.maxMotorTorque = torqueF;
            // setting wheels' motors
            frontTire.motor = frontMotor;
            backTire.motor = backMotor;
            // rotating car
            GetComponent<Rigidbody2D>().AddTorque(carRotationSpeed * movement);
        }
        else if (movement < 0)
        {   
            // doing same thing
            frontMotor.motorSpeed = speedB * -1;
            backMotor.motorSpeed = speedB * -1;

            frontMotor.maxMotorTorque = torqueB;
            backMotor.maxMotorTorque = torqueB;

            frontTire.motor = frontMotor;
            backTire.motor = backMotor;

            GetComponent<Rigidbody2D>().AddTorque(carRotationSpeed * movement);
        }
        else
        {
            frontTire.useMotor = false;
            backTire.useMotor = false;
        }
    }

    void FuelDecrease()
    {
        // decreasing fuel
        fuel -= fuelDecrease;
        // giving fuel to fuel meter so it can show amount of fuel
        fuelMeter.fuel = fuel;
        // checking is fuel less than 0
        if (fuel <= 0)
        {
            // then out of fuel
            isOutofFuel = true;
            OutOfFuel();
        }
    }
    public void OutOfFuel()
    {
        // checking is driver down 
        if (!driverDownScript.isDriverDown)
        {
            // checking is car out of fuel
            if (isOutofFuel)
            {
                if (!isOutofFuelPlayed)
                {
                    // checking is car not moving
                    if (rb.velocity.sqrMagnitude * 3.6f <= 5f)
                    {
                        // playing out of fuel vc
                        vo_OutOfFuel.Play();
                        // setting out of fuel text to true
                        outOfFuelTransform.gameObject.SetActive(true);
                        // setting is out of fuel played to true
                        isOutofFuelPlayed = true;
                        // setting if driver down so dont show text
                        cantBeDDtext = true;
                        // disabling hud
                        DisableHUD();
                    }
                }
            }
        }
        // turning off motors
        GetComponent<CarController>().enabled = false;
        frontTire.useMotor = false;
        backTire.useMotor = false;
        // stopping decreasing fuel
        CancelInvoke("FuelDecrease");
    }
    
    public void DisableHUD()
    {
        hud.gameObject.SetActive(false);
    }
}