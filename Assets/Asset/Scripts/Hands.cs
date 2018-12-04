using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour {

    private GameObject colldingObject;
    private GameObject heldObject;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            colldingObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colldingObject = null;
    }




    public bool isLeftHand;
    private string grip;
    private bool gripHeld;


    // Use this for initialization
    void Start ()
    {
        if (isLeftHand)
        {
            grip = "HTC_VIU_LeftGrip";
        }
        else
        {
            grip = "HTC_VIU_RightGrip";
        }
	}

    private Vector3 oldPos;
    private Vector3 handVelcity;
    public float handVelocityfudgefactor = 25f;

    private Vector3 oldRot;
    private Vector3 handSpin;
    public float handSpinfudgefactor = 25f;
    // Update is called once per frame
    void Update ()
    {
        handVelcity = (this.transform.position - oldPos) * handVelocityfudgefactor;
        oldPos = this.transform.position;

        handSpin = (this.transform.eulerAngles - oldRot) * handSpinfudgefactor;
        oldRot = this.transform.eulerAngles;

        if (gripHeld == false && Input.GetAxis(grip) > 0.5f)
        {
            if (colldingObject)
            {
                Grab();
            }

            gripHeld = true;
        }
		else if(gripHeld == true && Input.GetAxis(grip) < 0.5f)
        {
            Release();
            gripHeld = false;
        }
	}

    void Grab()
    {
        heldObject = colldingObject;
        FixedJoint fx = this.gameObject.AddComponent<FixedJoint>();
        fx.connectedBody = heldObject.GetComponent<Rigidbody>();
        fx.breakTorque = 1500f;
        fx.breakForce = 1500f;
    }

    void Release()
    {
        if (GetComponent<FixedJoint>())
        {
            //Debug.Log("Destroying FixedJoint: "+ Time.time);

            Destroy(GetComponent<FixedJoint>());
            heldObject.GetComponent<Rigidbody>().velocity = handVelcity;
            heldObject.GetComponent<Rigidbody>().angularVelocity = handSpin;

            //Debug.Log("Destroyed FixedJoint: " + Time.time);
        }

        heldObject = null;
    }

}
