/*
Dialogue System VR Example
(Requires Unity 2017.4.4+)

This example scene demonstrates how to show world space dialogue UIs in VR 
using Unity's SelectionSlider interaction method, and how to configure an
NPC to start a conversation when interacted with using the VR Samples
VREyeRaycaster method.

To play it, you *must* import Unity's VR Samples:
https://www.assetstore.unity3d.com/en/#!/content/51519

Other notes in the scene:

- If playing in the editor, make sure to click in the Game View to give it
  focus so the VR Samples components will work.
- VRManager: Must be present for Unity's VR scripts to work.
- MainCamera: Has several Unity VR scripts. Make sure your scene's camera
  has the same VR scripts.
- Dialogue Manager canvas: Has a Reposition Moving VR Canvas component that
  positions in the canvas in front of the player when a conversation starts.
- Private Hart: Has a VR Interactive Usable which invokes Dialogue System
  triggers that are configured to listen to OnUse, in this case to start
  the conversation.
*/