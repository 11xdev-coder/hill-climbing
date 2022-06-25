using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Destructive : MonoBehaviour
{
    public string canDestroyLayer;

    public HingeJoint2D connectedObjectRB;

    public AudioSource AS;

    public bool isDetached = false;
    public void Start()
    {

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (!isDetached)
        {
            if (col.transform.CompareTag(canDestroyLayer))
            {
                connectedObjectRB.connectedBody = null;
                isDetached = true;
                AS.Play();
            }
        }
    }
}
