using UnityEngine;
using VRStandardAssets.Utils;

namespace PixelCrushers.DialogueSystem.Demo
{

    /// <summary>
    /// Add this script to a UI button that also has a VR SelectionSlider.
    /// When the SelectionSlider's bar is full, it invokes the button's
    /// OnClick event.
    /// </summary>
    public class SelectionSliderButton : MonoBehaviour
    {

        private SelectionSlider m_SelectionSlider;

        void Start()
        {
            m_SelectionSlider = GetComponentInChildren<SelectionSlider>();
            if (m_SelectionSlider != null) m_SelectionSlider.OnBarFilled += OnBarFilled;
        }

        void OnDestroy()
        {
            if (m_SelectionSlider != null) m_SelectionSlider.OnBarFilled -= OnBarFilled;
        }

        private void OnBarFilled()
        {
            var button = GetComponent<UnityEngine.UI.Button>();
            if (button != null) button.onClick.Invoke();
        }

    }
}