using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportationActivator : MonoBehaviour
{
    [SerializeField] XRRayInteractor teleportInteractor;
    [SerializeField] InputActionProperty teleportActivatorAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        teleportInteractor.gameObject.SetActive(false);
        teleportActivatorAction.action.performed += Action_performed;
    }

    private void OnDestroy()
    {
        teleportActivatorAction.action.performed -= Action_performed;
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        teleportInteractor.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (teleportActivatorAction.action.WasReleasedThisFrame())
        {
            teleportInteractor.gameObject.SetActive(false);
        }
    }
}
