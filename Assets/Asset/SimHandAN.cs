using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimHandAN : MonoBehaviour
{

    public float moveSpeed, turnSpeed, rollSpeed;

    public GameObject openHand, closedHand;

    public GameObject prefabPaintball;
    public float shootForce;

    private GameObject collidingObject, heldObject;

    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            collidingObject = other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            collidingObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == collidingObject)
        {
            collidingObject = null;
        }
    }
    #endregion

    public float velocityFudgeFactor;
    private Vector3 oldPosition;
    private Vector3 handVelocity;

    private Vector3 oldRotation;
    private Vector3 handAngularVelocity;
    // Update is called once per frame
    void Update()
    {
        handVelocity = transform.position - oldPosition;
        oldPosition = transform.position;

        handAngularVelocity = transform.eulerAngles - oldRotation;
        oldRotation = transform.eulerAngles;

        #region Sim Hand Movement

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        #endregion
        #region Sim Hand Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * turnSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(Vector3.forward * rollSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(Vector3.back * rollSpeed * Time.deltaTime);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            closedHand.SetActive(true);
            openHand.SetActive(false);
            if (collidingObject)
            {
                heldObject = collidingObject;
                AdvGrab();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            closedHand.SetActive(false);
            openHand.SetActive(true);
            if (heldObject)
            {
                AdvRelease();
                heldObject = null;
            }
        }

        //Interactions with held objects
        if (Input.GetKeyDown(KeyCode.Mouse0) && heldObject)
        {
            if (heldObject.tag == "Flashlight")
            {
                heldObject.GetComponent<Light>().enabled = !heldObject.GetComponent<Light>().enabled;
            }
            else if (heldObject.tag == "Gun")
            {
                GameObject paintball = Instantiate(prefabPaintball, heldObject.transform.position, heldObject.transform.rotation);
                paintball.GetComponent<Rigidbody>().AddForce(paintball.transform.forward * shootForce);
                Destroy(paintball, 2);
            }
        }

    }


    void Grab()
    {
        heldObject.transform.SetParent(this.transform);
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
        //heldObject.transform.localPosition = new Vector3(0,0,0);
        heldObject.transform.localEulerAngles = Vector3.zero;
    }

    void Release()
    {
        heldObject.transform.SetParent(null);
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    void AdvGrab()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 1500;// heldObject.GetComponent<Rigidbody>().mass;
        fx.breakTorque = 1500;
        fx.connectedBody = heldObject.GetComponent<Rigidbody>();
    }

    void AdvRelease()
    {
        if (GetComponent<FixedJoint>())
        {
            Destroy(GetComponent<FixedJoint>());
            heldObject.GetComponent<Rigidbody>().velocity = handVelocity * velocityFudgeFactor / heldObject.GetComponent<Rigidbody>().mass;
            heldObject.GetComponent<Rigidbody>().angularVelocity = handAngularVelocity;
        }
    }
}
