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

        private Transform myCameraTransform;
        private bool isInConversation = false;

        private void Awake()
        {
            myCameraTransform = GetComponentInChildren<Camera>().transform;
        }

        private void Update()
        {
            if (isInConversation) return;
            transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime));
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseXSensitivity, 0));
        }

        private void OnConversationStart(Transform actor)
        {
            isInConversation = true;
        }

        private void OnConversationEnd(Transform actor)
        {
            isInConversation = false;
        }
    }
}