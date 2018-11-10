using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Test : MonoBehaviour {

    public bool triggerPressed;





    // Use this for initialization
    void Start () {

        		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("HTC_VIU_LeftTrigger") > 0.1f && triggerPressed == false)
        {
            triggerPressed = true;
            Debug.Log("pressed down");
        }
        else if (Input.GetAxis("HTC_VIU_LeftTrigger") < 0.1f && triggerPressed == true)
        {
            triggerPressed = false;
            Debug.Log("pressed up");
        }





        //Debug.Log(Input.GetAxis("HTC_VIU_LeftTrigger"));
       
    }
}
