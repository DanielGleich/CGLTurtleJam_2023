using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlsRotate : MonoBehaviour {
    private InputActionControls inputActions;
    [SerializeField] private float tiltSpeed;
    private bool tiltPressed = false;
    private InputAction inputTilt;

    [SerializeField] private bool tiltAllowed;

    private void Awake() {
        inputActions = new InputActionControls();
    }

    private void OnEnable() {
        inputTilt = inputActions.Gameplay.Tilt;
        inputTilt.performed += StartTilt;
        inputTilt.canceled += StopTilt;
    }

    private void OnDisable() {
        DisableControls();
    }

    private void Update() {
        if (tiltAllowed) {
            float tilt = inputTilt.ReadValue<float>();

            if (tiltPressed)
                gameObject.transform.Rotate(0f, 0f, -tiltSpeed * tilt * Time.deltaTime);
        }
    }

    private void StartTilt(InputAction.CallbackContext context) {
        tiltPressed = true;
    }

    private void StopTilt(InputAction.CallbackContext context) {
        tiltPressed = false;
    }

    public void EnableControls()
    {
        inputTilt.Enable();
    }

    public void DisableControls()
    {
        inputTilt.Disable();    
    }
}
