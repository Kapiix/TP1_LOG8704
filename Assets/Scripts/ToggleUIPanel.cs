using UnityEngine;
using UnityEngine.XR;

public class ToggleUIPanel : MonoBehaviour
{
    [Tooltip("Le panneau UI à afficher/masquer (glisse UI_Instructions ici)")]
    public GameObject uiPanel;

    private bool isVisible = true;
    private bool wasPressedLastFrame = false;

    void Update()
    {
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isPressed))
        {
            // On ne déclenche qu'au moment où le bouton vient d'être appuyé,
            // pas à chaque frame où il reste enfoncé
            if (isPressed && !wasPressedLastFrame)
            {
                isVisible = !isVisible;
                uiPanel.SetActive(isVisible);
            }
            wasPressedLastFrame = isPressed;
        }
    }
}