using UnityEngine;
using VRStandardAssets.Utils;

namespace PixelCrushers.DialogueSystem.Demo
{

    /// <summary>
    /// Add this to a VRInteractiveItem. When interacted, it will send OnUse to itself,
    /// allowing triggers such as DialogueSystemTrigger that are configured to fire OnUse
    /// to fire.
    /// </summary>
    [RequireComponent(typeof(VRInteractiveItem))]
    public class VRInteractiveUsable : MonoBehaviour
    {
        [SerializeField] private SelectionRadial m_SelectionRadial;         // This controls when the selection is complete.
        [SerializeField] private VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.

        private bool m_GazeOver;                                            // Whether the user is looking at the VRInteractiveItem currently.

        private void Awake()
        {
            if (m_SelectionRadial == null) m_SelectionRadial = FindObjectOfType<SelectionRadial>();
            if (m_InteractiveItem == null) m_InteractiveItem = GetComponent<VRInteractiveItem>();
        }

        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
        }

        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
        }

        private void HandleOver()
        {
            m_SelectionRadial.Show();
            m_GazeOver = true;
        }


        private void HandleOut()
        {
            m_SelectionRadial.Hide();
            m_GazeOver = false;
        }


        private void HandleSelectionComplete()
        {
            if (m_GazeOver) SendMessage("OnUse", m_SelectionRadial.transform, SendMessageOptions.DontRequireReceiver);
        }

    }
}