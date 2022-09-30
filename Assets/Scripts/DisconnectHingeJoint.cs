using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectHingeJoint : MonoBehaviour
{
    public HingeJoint2D origPart;
    public Rigidbody2D partToConnect;

    public void Disconnect()
    {
        // connecting hinge joint part to another one
        origPart.connectedBody = partToConnect;
    }
}
