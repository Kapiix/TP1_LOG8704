using UnityEngine;
using UnityEngine.XR;

public class ToggleUIPanel : MonoBehaviour
{
    public GameObject uiPanel;

    public Transform playerCamera;

    public float distanceFromPlayer = 2f;

    public float minWallDistance = 1.3f;

    private bool isVisible = false;
    private bool wasPressedLastFrame = false;

    void Start()
    {
        uiPanel.SetActive(true);
    }

    void Update()
    {
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isPressed))
        {
            if (isPressed && !wasPressedLastFrame)
            {
                TryToggle();
            }
            wasPressedLastFrame = isPressed;
        }
    }

    void TryToggle()
    {
        // Si le panneau est actuellement affiché, on le masque 
        if (isVisible)
        {
            isVisible = false;
            uiPanel.SetActive(false);
            return;
        }

        Vector3 flatForward = new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z).normalized;
        Vector3 rayOrigin = playerCamera.position;

        // Vérifions s'il y a un mur trop proche devant
        if (Physics.Raycast(rayOrigin, flatForward, out RaycastHit hit, minWallDistance))
        {
            // Mur trop proche : on n'affiche rien
            return;
        }

        // Pas de mur proche : on affiche normalement
        Vector3 targetPosition = rayOrigin + flatForward * distanceFromPlayer;
        targetPosition.y = playerCamera.position.y;

        uiPanel.transform.position = targetPosition;
        uiPanel.transform.rotation = Quaternion.LookRotation(flatForward);

        isVisible = true;
        uiPanel.SetActive(true);
    }
}