using UnityEngine;

namespace PixelCrushers.DialogueSystem.Demo
{

    /// <summary>
    /// This is a very basic movement script for the VR example.
    /// </summary>
    public class BasicVRMovement : MonoBehaviour
    {
        public float speed = 2;
        public float mouseXSensitivity = 5;
        public float mouseYSensitivity = -5;
        public Transform myCameraTransform;
        private bool isInConversation = false;
        

        private void Awake()
        {
            myCameraTransform = GetComponentInChildren<Camera>().transform;
        }

        private void Update()
        {

            if (Input.GetKey(KeyCode.JoystickButton8) || Input.GetKey(KeyCode.JoystickButton9))
            {
                return;
            }
                //if (isInConversation) return;
                transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime));
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }

            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseXSensitivity, 0));
            myCameraTransform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * mouseYSensitivity, 0, 0));
        }

        private void OnConversationStart(Transform actor)
        {
            isInConversation = true;
        }

        private void OnConversationEnd(Transform actor)
        {
            isInConversation = false;
        }
        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
}

