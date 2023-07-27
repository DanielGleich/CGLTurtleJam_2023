using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    private InputActionControls inputActions;
    [SerializeField] private Button defaultSelected;

    private InputAction inputNavigateUp;
    private InputAction inputNavigateDown;
    private InputAction inputSelect;
    private InputAction inputBack;

    private void Awake() {
        inputActions = new InputActionControls();
        defaultSelected.Select();
    }

    private void OnEnable() {
        SetupMenuControls();
    }

    private void OnDisable() {
        DisableMenuControls();
    }

    private void SetupMenuControls() {
        inputNavigateUp = inputActions.MainMenu.NavigateUp;
        inputNavigateUp.Enable();

        inputNavigateDown = inputActions.MainMenu.NavigateDown;
        inputNavigateDown.Enable();

        inputSelect = inputActions.MainMenu.Select;
        inputSelect.Enable();

        inputBack = inputActions.MainMenu.Back;
        inputBack.Enable();
    }

    private void DisableMenuControls() {
        inputNavigateUp.Disable();
        inputNavigateDown.Disable();
        inputSelect.Disable();
        inputBack.Disable();
    }

    public void OnStartPressed() {
        SceneManager.LoadScene(1);
    }

    public void OnQuitPressed() {
        Application.Quit();
    }
}
