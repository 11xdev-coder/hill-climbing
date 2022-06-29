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
    public float capacity = 0;
    public void Start()
    {

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (!isDetached)
        {
            if (col.transform.CompareTag(canDestroyLayer))
            {
                capacity -= 0.25f;
                if (capacity <= 0)
                {
                    connectedObjectRB.connectedBody = null;
                    isDetached = true;
                    AS.Play();
                    GetComponent<PolygonCollider2D>().enabled = false;
                }
            }
        }
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        if (!isDetached)
        {
            if (col.transform.CompareTag(canDestroyLayer))
            {
                capacity -= 0.1f;
                if (capacity <= 0)
                {
                    connectedObjectRB.connectedBody = null;
                    isDetached = true;
                    AS.Play();
                    GetComponent<PolygonCollider2D>().enabled = false;
                }
            }
        }
    }
}
