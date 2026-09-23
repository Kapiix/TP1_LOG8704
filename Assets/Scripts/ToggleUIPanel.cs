using UnityEngine;
using UnityEngine.XR;

public class ToggleUIPanel : MonoBehaviour
{
    [Tooltip("Le panneau UI à afficher/masquer")]
    public GameObject uiPanel;

    [Tooltip("La caméra du joueur")]
    public Transform playerCamera;

    [Tooltip("Distance devant le joueur où apparaît le panneau")]
    public float distanceFromPlayer = 2f;

    private bool isVisible = false;
    private bool wasPressedLastFrame = false;

    void Start()
    {
        uiPanel.SetActive(false); 
    }

    void Update()
    {
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isPressed))
        {
            if (isPressed && !wasPressedLastFrame)
            {
                isVisible = !isVisible;

                if (isVisible)
                {
                    // Repositionne le panneau devant le joueur, à hauteur d'yeux
                    Vector3 flatForward = new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z).normalized;
                    Vector3 targetPosition = playerCamera.position + flatForward * distanceFromPlayer;
                    targetPosition.y = playerCamera.position.y;

                    uiPanel.transform.position = targetPosition;
                    uiPanel.transform.rotation = Quaternion.LookRotation(flatForward);
                }

                uiPanel.SetActive(isVisible);
            }
            wasPressedLastFrame = isPressed;
        }
    }
}