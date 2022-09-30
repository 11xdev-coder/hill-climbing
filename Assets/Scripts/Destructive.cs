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

    public void OnCollisionEnter2D(Collision2D col)
    {
        // checking is part gone
        if (!isDetached)
        {
            // if not, checking is part collided with layer that can destroy it
            if (col.transform.CompareTag(canDestroyLayer))
            {
                // if yes, decreasing capacity
                capacity -= 0.25f;
                // if capacity less than 0
                if (capacity <= 0)
                {
                    // detaching part
                    connectedObjectRB.connectedBody = null;
                    isDetached = true;
                    AS.Play();
                    StartCoroutine("destroyPart");
                }
            }
        }
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        // same thing
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
                    StartCoroutine("destroyPart");
                }
            }
        }
    }


    IEnumerator destroyPart()
    {
        // making this part so he can collide anything
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<HingeJoint2D>().enabled = false;
        // waiting 3 secs
        yield return new WaitForSeconds(3f);
        // destroying game object
        Destroy(gameObject);
    }
}
