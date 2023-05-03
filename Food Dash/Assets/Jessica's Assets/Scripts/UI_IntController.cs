using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class UI_InteractionController : MonoBehaviour
{
    [SerializeField] private GameObject uiController;
    [SerializeField] private GameObject baseController;
    [SerializeField] private InputActionReference uiSwitcherInputAction;

    [SerializeField] private GameObject uiCanvasGameObject;
    private bool isUICanvasActive;

    private void OnEnable()
    {
        uiSwitcherInputAction.action.performed += ActivateUIMode;
    }

    private void OnDisable()
    {
        uiSwitcherInputAction.action.performed -= ActivateUIMode;
    }

    private void Start()
    {
        // Deactivate UI Canvas GameObject by default
        uiCanvasGameObject.SetActive(false);

        // Deactivate UI Controller by default
        var xrRayInteractor = uiController.GetComponent<XRRayInteractor>();
        xrRayInteractor.enabled = false;
        var xrInteractorLineVisual = uiController.GetComponent<XRInteractorLineVisual>();
        xrInteractorLineVisual.enabled = false;
    }

    /// <summary>
    /// This method is called when the player presses the UI Switcher Button which is the input action defined in Default Input Actions.
    /// When it is called, UI interaction mode is switched on and off according to the previous state of the UI Canvas.
    /// </summary>
    private void ActivateUIMode(InputAction.CallbackContext context)
    {
        isUICanvasActive = !isUICanvasActive;

        var xrRayInteractor = uiController.GetComponent<XRRayInteractor>();
        var xrInteractorLineVisual = uiController.GetComponent<XRInteractorLineVisual>();
        var xrDirectInteractor = baseController.GetComponent<XRDirectInteractor>();

        if (isUICanvasActive)
        {
            // Activate UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            xrRayInteractor.enabled = true;
            xrInteractorLineVisual.enabled = true;

            // Deactivate Base Controller by disabling its XR Direct Interactor
            xrDirectInteractor.enabled = false;

            // Activate the UI Canvas GameObject
            uiCanvasGameObject.SetActive(true);
        }
        else
        {
            // Deactivate UI Controller by disabling its XR Ray Interactor and XR Interactor Line Visual
            xrRayInteractor.enabled = false;
            xrInteractorLineVisual.enabled = false;

            // Activate Base Controller by enabling its XR Direct Interactor
            xrDirectInteractor.enabled = true;

            // Deactivate the UI Canvas GameObject
            uiCanvasGameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class UI_InteractionController : MonoBehaviour
{
    [SerializeField] private GameObject uiController;
    [SerializeField] private GameObject baseController;
    [SerializeField] private InputActionReference uiSwitcherInputAction;

    [SerializeField] private GameObject uiCanvasGameObject;
    private bool isUICanvasActive;

    private void OnEnable()
    {
        uiSwitcherInputAction.action.performed += ActivateUIMode;
    }

    private void OnDisable()
    {
        uiSwitcherInputAction.action.performed -= ActivateUIMode;
    }

    private void Start()
    {
        // Deactivate UI Canvas GameObject by default
        uiCanvasGameObject.SetActive(false);

        // Deactivate UI Controller by default
        var xrRayInteractor = uiController.GetComponent<XRRayInteractor>();
        xrRayInteractor.enabled = false;
        var xrInteractorLineVisual = uiController.GetComponent<XRInteractorLineVisual>();
        xrInteractorLineVisual.enabled = false;
    }

    /// <summary>
    /// This method is called when the player presses the UI Switcher Button which is the input action defined in Default Input Actions.
    /// When it is called, UI interaction mode is switched on and off according to the previous state of the UI Canvas.
    /// </summary>
    private void ActivateUIMode(InputAction.CallbackContext context)
    {
        isUICanvasActive = !isUICanvasActive;

        var xrRayInteractor = uiController.GetComponent<XRRayInteractor>();
        var xrInteractorLineVisual = uiController.GetComponent<XRInteractorLineVisual>();
        var xrDirectInteractor = baseController.GetComponent<XRDirectInteractor>();

        if (isUICanvasActive)
        {
            // Activate UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            xrRayInteractor.enabled = true;
            xrInteractorLineVisual.enabled = true;

            // Deactivate Base Controller by disabling its XR Direct Interactor
            xrDirectInteractor.enabled = false;

            // Activate the UI Canvas GameObject
            uiCanvasGameObject.SetActive(true);
        }
        else
        {
            // Deactivate UI Controller by disabling its XR Ray Interactor and XR Interactor Line Visual
            xrRayInteractor.enabled = false;
            xrInteractorLineVisual.enabled = false;

            // Activate Base Controller by enabling its XR Direct Interactor
            xrDirectInteractor.enabled = true;

            // Deactivate the UI Canvas GameObject
            uiCanvasGameObject.SetActive(false);
        }
    }
}
