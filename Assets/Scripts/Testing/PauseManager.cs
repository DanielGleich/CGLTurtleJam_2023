using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    private InputActionControls inputActions;
    private InputActionControls.GameplayActions gameplayControls;
    private InputActionControls.PauseMenuActions pauseMenuControls;
    private InputAction pause;
    private bool gamePaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button defaultSelected;

    private void Awake() {
        inputActions = new InputActionControls();
        gameplayControls = inputActions.Gameplay;
        pauseMenuControls = inputActions.PauseMenu;
    }

    private void OnEnable() {
        pause = inputActions.Gameplay.TogglePause;
        pause.Enable();
        pause.started += TogglePause;
    }

    private void OnDisable() {
        pause.Disable();
    }

    private void PauseGame() {
        pauseMenu.SetActive(true);
        defaultSelected.Select();
        gameplayControls.Disable();
        Time.timeScale = 0f;
    }

    private void UnPauseGame() {
        pauseMenu.SetActive(false);
        gameplayControls.Enable();
        Time.timeScale = 1f;
    }

    public void OnResumePressed() {
        UnPauseGame();
        gamePaused = false;
    }

    public void OnQuitPressed() {
        Application.Quit();
    }

    private void TogglePause(InputAction.CallbackContext context) {
        gamePaused = !gamePaused;

        if (gamePaused)
            PauseGame();
        else
            UnPauseGame();
    }
}
